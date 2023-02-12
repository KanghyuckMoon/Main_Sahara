using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utill.Addressable;
using Utill.Pattern;
using Module;
using static NodeUtill;

namespace AI
{
	public partial class RootNodeMaker
	{
		//Condition
		private bool FerCloserMoveCondition()
		{
			Vector3 vec = aiModule.MainModule.transform.position - aiModule.Player.position;
			float sqrLen = vec.sqrMagnitude;
			float distance = aiSO.ferDistance;

			if (aiModule.AIModuleState == AIModule.AIState.Walk)
			{
				distance += 2f;
			}

			if (sqrLen < distance * distance)
			{
				return false;
			}
			return true;
		}
		private bool DiscorverCondition()
		{
			Vector3 vec = aiModule.MainModule.transform.position - aiModule.Player.position;
			float sqrLen = vec.sqrMagnitude;

			//사거리 안에 들어오면
			if (sqrLen < aiSO.ViewRadius * aiSO.ViewRadius)
			{
				return false;
			}
			return true;
		}

		private bool AttackCondition()
		{
			Vector3 targetPos = aiModule.Player.position;
			Vector3 targetDir = (targetPos - Position).normalized;
			float lookingAngle = aiModule.MainModule.transform.eulerAngles.y;
			Vector3 lookDir = AngleToDir(lookingAngle);
			float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;

			if (targetAngle <= aiSO.AttackAngle * 0.5f && (targetPos - Position).sqrMagnitude < aiSO.AttackRadius * aiSO.AttackRadius)
			{
				return true;
			}
			return false;
		}

		private bool AttackRangeCondition()
		{
			Vector3 targetPos = aiModule.Player.position;
			
			if ((targetPos - Position).sqrMagnitude < aiSO.AttackRadius * aiSO.AttackRadius)
			{
				return true;
			}
			return false;
		}

		private bool TargetFindCondition()
		{
			Collider[] Targets = Physics.OverlapSphere(Position, aiSO.ViewRadius, aiSO.TargetMask);
			Vector3 targetPos = aiModule.Player.position;
			Vector3 targetDir = (targetPos - Position).normalized;
			float lookingAngle = aiModule.MainModule.transform.eulerAngles.y;
			Vector3 lookDir = AngleToDir(lookingAngle);
			float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
			if (targetAngle <= aiSO.ViewAngle * 0.5f && !Physics.Raycast(Position, targetDir, aiSO.ViewRadius, aiSO.ObstacleMask))
			{
				return true;
			}

			return false;
		}

		private bool DiscoveryCondition()
		{
			if (aiModule.AIModuleHostileState == AIModule.AIHostileState.Unknow)
			{
				return false;
			}
			return true;
		}
		private bool NotDiscoveryCondition()
		{
			if (aiModule.AIModuleHostileState == AIModule.AIHostileState.Unknow || aiModule.AIModuleHostileState == AIModule.AIHostileState.Investigate || aiModule.AIModuleHostileState == AIModule.AIHostileState.Suspicion)
			{
				return true;
			}
			return false;
		}
		private bool JumpMoveCondition()
		{
			if (path.corners.Length > 1)
			{
				Vector3 distancePos = path.corners[1] - aiModule.Player.position;
				if (path.corners[1].y - Position.y > yDistance || distancePos.sqrMagnitude > distance * distance)
				{
					return JumpCheck();
				}
			}
			return false;
		}

		private bool JumpCheck()
		{
			jumpCheckVector = Vector3.zero;
			jumpCheckVector = aiModule.Player.position - Position;
			jumpCheckVector.y = 0;
			jumpCheckVector = jumpCheckVector.normalized;

			float dir = Mathf.Atan2(aiModule.MainModule.GetModuleComponent<StateModule>(ModuleType.State).JumpPower, aiModule.MainModule.GetModuleComponent<StateModule>(ModuleType.State).Speed);
			float force = aiModule.MainModule.GetModuleComponent<StateModule>(ModuleType.State).Speed;
			float width = Caculated_Width(force, dir);
			RaycastHit raycastHit;
			jumpCheckVector *= width;
			jumpCheckVector.y = Position.y + 100;
			jumpCheckVector.x += Position.x;
			jumpCheckVector.z += Position.z;
			if (Physics.Raycast(jumpCheckVector, Vector3.down, out raycastHit, aiModule.MainModule.GroundLayer))
			{
				NavMeshPath tempPath = new NavMeshPath();
				NavMesh.CalculatePath(raycastHit.point, aiModule.Player.position, NavMesh.AllAreas, tempPath);

				if (tempPath.corners.Length > 0)
				{
					Vector3 _distancePos = tempPath.corners[tempPath.corners.Length - 1] - aiModule.Player.position;
					if (_distancePos.sqrMagnitude < distance * distance)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		private bool HitCheck()
		{
			if (aiModule.MainModule.IsHit)
			{
				return true;
			}
			return false;
		}

		private bool HostileCheck()
		{
			return aiModule.IsHostilities;
		}

		//Action
		private void SuspicionGaugeSet()
		{
			switch (aiModule.AIModuleHostileState)
			{
				case AIModule.AIHostileState.Unknow:
					if (aiModule.SuspicionGauge > 0)
					{
						aiModule.SuspicionGauge -= Time.deltaTime;
					}
					else
					{
						aiModule.SuspicionGauge = 0;
					}
					break;
				case AIModule.AIHostileState.Suspicion:
					if (aiModule.SuspicionGauge < aiSO.maxSuspicionGauge)
					{
						aiModule.SuspicionGauge += Time.deltaTime;
					}
					else
					{
						aiModule.SuspicionGauge = aiSO.maxSuspicionGauge;
						aiModule.AIModuleHostileState = AIModule.AIHostileState.Discovery;
					}
					break;
				case AIModule.AIHostileState.Discovery:
					aiModule.SuspicionGauge = aiSO.maxSuspicionGauge;
					break;
				case AIModule.AIHostileState.Investigate:
					if (aiModule.SuspicionGauge > 0)
					{
						aiModule.SuspicionGauge -= Time.deltaTime;
					}
					else
					{
						aiModule.SuspicionGauge = 0;
						aiModule.AIModuleHostileState = AIModule.AIHostileState.Unknow;
					}
					break;
			}
		}

		private void TargetFind()
		{
			Vector3 targetPos = aiModule.Player.position;
			Vector3 targetDir = (targetPos - Position).normalized;
			float lookingAngle = aiModule.MainModule.transform.eulerAngles.y;
			Vector3 lookDir = AngleToDir(lookingAngle);
			float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;

			if (aiModule.AIModuleHostileState == AIModule.AIHostileState.Discovery && (targetPos - Position).sqrMagnitude < aiSO.SuspicionRadius * aiSO.SuspicionRadius)
			{
				return;
			}

			//발견각
			if (targetAngle <= aiSO.ViewAngle * 0.5f && !Physics.Raycast(Position, targetDir, aiSO.ViewRadius, aiSO.ObstacleMask) && (targetPos - Position).sqrMagnitude < aiSO.ViewRadius * aiSO.ViewRadius)
			{
				aiModule.AIModuleHostileState = AIModule.AIHostileState.Discovery;
			}
			else if (targetAngle <= aiSO.SuspicionAngle * 0.5f && !Physics.Raycast(Position, targetDir, aiSO.SuspicionRadius, aiSO.ObstacleMask) && (targetPos - Position).sqrMagnitude < aiSO.SuspicionRadius * aiSO.SuspicionRadius)
			{
				aiModule.AIModuleHostileState = AIModule.AIHostileState.Suspicion;
			}
			else
			{
				if (aiModule.AIModuleHostileState == AIModule.AIHostileState.Discovery)
				{
					aiModule.AIModuleHostileState = AIModule.AIHostileState.Investigate;
				}
				else
				{
					aiModule.AIModuleHostileState = AIModule.AIHostileState.Unknow;
				}
			}
		}

		private void Reset()
		{
			aiModule.MainModule.Attacking = false;
			aiModule.MainModule.IsJump = false;
			aiModule.MainModule.IsJumpBuf = false;
		}

		private void Attack()
		{
			aiModule.AIModuleState = AIModule.AIState.Attack;
			aiModule.MainModule.Attacking = true;
		}
		private void CloserMove()
		{
			if (aiModule.MainModule.CanMove && !aiModule.MainModule.Attacking)
			{
				PathSetting();
				Vector3 vec = Vector3.zero;
				if (path.corners.Length > 1)
				{
					vec = path.corners[1] - Position;
				}
				else
				{
					vec = aiModule.Player.position - Position;
				}
				vec.y = 0;
				vec = vec.normalized;
				Vector2 _inputdir = new Vector2(vec.x, vec.z);
				aiModule.Input = _inputdir;

				aiModule.AIModuleState = AIModule.AIState.Walk;
				aiModule.MainModule.IsSprint = false;
				aiModule.MainModule.ObjDir = aiModule.Input;
			}
		}

		private void RunMove()
		{
			if (aiModule.MainModule.CanMove && !aiModule.MainModule.Attacking)
			{
				PathSetting();
				Vector3 vec = Vector3.zero;
				if (path.corners.Length > 1)
				{
					vec = path.corners[1] - Position;
				}
				else
				{
					vec = aiModule.Player.position - Position;
				}
				vec.y = 0;
				vec = vec.normalized;
				Vector2 _inputdir = new Vector2(vec.x, vec.z);
				aiModule.Input = Vector2.Lerp(aiModule.Input, _inputdir, Time.deltaTime);

				aiModule.AIModuleState = AIModule.AIState.Run;
				aiModule.MainModule.IsSprint = true;
				aiModule.MainModule.ObjDir = aiModule.Input;
			}
		}
		private void JumpAndRunMove()
		{
			if (aiModule.MainModule.CanMove && !aiModule.MainModule.Attacking)
			{
				Vector3 vecDistance = Vector3.zero;
				vecDistance = path.corners[1] - Position;
				if (vecDistance.sqrMagnitude < jumpDistance * jumpDistance)
				{
					aiModule.AIModuleState = AIModule.AIState.Jump;
					aiModule.MainModule.IsJump = true;
					aiModule.MainModule.IsJumpBuf = true;
				}

				PathSetting();
				Vector3 vec = Vector3.zero;
				vec = aiModule.Player.position - Position;
				vec.y = 0;
				vec = vec.normalized;
				Vector2 _inputdir = new Vector2(vec.x, vec.z);
				aiModule.Input = _inputdir;

				aiModule.AIModuleState = AIModule.AIState.Run;
				aiModule.MainModule.IsSprint = true;
				aiModule.MainModule.ObjDir = aiModule.Input;
			}
		}

		private void Ignore()
		{
			aiModule.Input = Vector2.Lerp(aiModule.Input, Vector2.zero, Time.deltaTime * 10);
			aiModule.AIModuleState = AIModule.AIState.Idle;
			aiModule.MainModule.IsSprint = false;
			aiModule.MainModule.ObjDir = aiModule.Input;
		}

		private void Jump()
		{
			aiModule.AIModuleState = AIModule.AIState.Jump;
			aiModule.MainModule.IsJump = true;
			aiModule.MainModule.IsJumpBuf = true;
		}

		private void Rotate()
		{
			Vector3 _vec = (aiModule.Player.position - Position).normalized;
			_vec.y = aiModule.MainModule.transform.forward.y;
			aiModule.MainModule.transform.forward = Vector3.Lerp(aiModule.MainModule.transform.forward, _vec, Time.deltaTime);
		}

		private void HostileStart()
		{
			aiModule.IsHostilities = true;
		}

		//Utill
		private Vector3 AngleToDir(float angle)
		{
			float radian = angle * Mathf.Deg2Rad;
			return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
		}
		private float GetAngle(Vector3 vStart, Vector3 vEnd)
		{
			Vector3 v = vEnd - vStart;

			return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
		}
		private float CalculateAngle(Vector3 from, Vector3 to)
		{
			return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.y;
		}
		private float Caculated_Width(float v, float sin)
		{
			return (v * v) * (Mathf.Sin(sin * 2)) / Mathf.Abs(Physics2D.gravity.y);
		}

		private void PathSetting()
		{
			elapsed += Time.deltaTime;
			if (elapsed > 0.3f)
			{
				elapsed = 0.3f;
				NavMesh.CalculatePath(Position, aiModule.Player.position, NavMesh.AllAreas, path);
			}
			else if (path.corners.Length > 1 && path.corners[1].sqrMagnitude < distance * distance)
			{
				elapsed = 0f;
				NavMesh.CalculatePath(Position, aiModule.Player.position, NavMesh.AllAreas, path);
			}
		}

	}
}
