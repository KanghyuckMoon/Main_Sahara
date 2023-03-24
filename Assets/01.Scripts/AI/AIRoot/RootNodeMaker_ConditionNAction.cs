using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utill.Addressable;
using Utill.Pattern;
using Module;
using Data;
using static NodeUtill;

namespace AI
{
	public partial class RootNodeMaker
	{
		//Condition
		private bool FerCloserMoveCondition() //Make
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

		private bool isRage = false;
		private bool RageCheck()
		{
			return isRage;
		}
		private bool NotRageCheck()
		{
			return !isRage;
		}
		private bool InitCheck()
		{
			if (aiModule.IsInit == false)
			{
				aiModule.IsInit = true;
				return true;
			}

			return false;
		}
		private bool DiscorverCondition() //Make
		{
			Vector3 vec = aiModule.MainModule.transform.position - aiModule.Player.position;
			float sqrLen = vec.sqrMagnitude;

			//��Ÿ� �ȿ� ������
			if (sqrLen < aiSO.ViewRadius * aiSO.ViewRadius)
			{
				return false;
			}
			return true;
		}

		private bool AttackCondition() //Make
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

		private bool AttackRangeCondition() //Make
		{
			Vector3 targetPos = aiModule.Player.position;
			
			if ((targetPos - Position).sqrMagnitude < aiSO.AttackRadius * aiSO.AttackRadius)
			{
				return true;
			}
			return false;
		}
		private bool AroundRangeCondition() //Make
		{
			Vector3 targetPos = aiModule.Player.position;
			
			if ((targetPos - Position).sqrMagnitude < aiSO.AroundRadius * aiSO.AroundRadius)
			{
				return true;
			}
			return false;
		}
		private bool SuspicionRangeCondition() //Make
		{
			Vector3 targetPos = aiModule.Player.position;
			
			if ((targetPos - Position).sqrMagnitude < aiSO.SuspicionRadius * aiSO.SuspicionRadius)
			{
				return true;
			}
			return false;
		}
		private bool ViewRangeCondition() //Make
		{
			Vector3 targetPos = aiModule.Player.position;
			
			if ((targetPos - Position).sqrMagnitude < aiSO.ViewRadius * aiSO.ViewRadius)
			{
				return true;
			}
			return false;
		}
		private bool OutSuspicionRangeCondition() //Make
		{
			Vector3 targetPos = aiModule.Player.position;
			
			if ((targetPos - Position).sqrMagnitude < aiSO.SuspicionRadius * aiSO.SuspicionRadius)
			{
				return false;
			}
			return true;
		}
		private bool OutViewRangeCondition() //Make
		{
			Vector3 targetPos = aiModule.Player.position;
			
			if ((targetPos - Position).sqrMagnitude < aiSO.ViewRadius * aiSO.ViewRadius)
			{
				return false;
			}
			return true;
		}

		private bool TargetFindCondition() //Make
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

		private bool AIHostileStateNotUnknow() //Make
		{
			if (aiModule.AIModuleHostileState == AIModule.AIHostileState.Unknow)
			{
				return false;
			}
			return true;
		}
		private bool AIHostileStateNotDiscovery() //Make
		{
			if (aiModule.AIModuleHostileState == AIModule.AIHostileState.Unknow || aiModule.AIModuleHostileState == AIModule.AIHostileState.Investigate || aiModule.AIModuleHostileState == AIModule.AIHostileState.Suspicion)
			{
				return true;
			}
			return false;
		}
		private bool AIHostileStateUnKnow()
		{
			if (aiModule.AIModuleHostileState ==  AIModule.AIHostileState.Unknow)
			{
				return true;
			}
			return false;
		}
		private bool AIHostileStateDiscovery() //Make
		{
			if (aiModule.AIModuleHostileState ==  AIModule.AIHostileState.Discovery)
			{
				return true;
			}
			return false;
		}
		private bool AIHostileStateInvestigate() //Make
		{
			if (aiModule.AIModuleHostileState ==  AIModule.AIHostileState.Investigate)
			{
				return true;
			}
			return false;
		}
		private bool AIHostileStateSuspicion() //Make
		{
			if (aiModule.AIModuleHostileState ==  AIModule.AIHostileState.Suspicion)
			{
				return true;
			}
			return false;
		}
		private bool JumpMoveCondition() //Make
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

		private bool JumpCheck() //Make
		{
			jumpCheckVector = Vector3.zero;
			jumpCheckVector = aiModule.Player.position - Position;
			jumpCheckVector.y = 0;
			jumpCheckVector = jumpCheckVector.normalized;

			float dir = Mathf.Atan2(aiModule.MainModule.StatData.Jump, aiModule.MainModule.StatData.WalkSpeed);
			float force = aiModule.MainModule.StatData.WalkSpeed;
			float width = Caculated_Width(force, dir);
			RaycastHit raycastHit;
			jumpCheckVector *= width;
			jumpCheckVector.y = Position.y + 100;
			jumpCheckVector.x += Position.x;
			jumpCheckVector.z += Position.z;
			if (Physics.Raycast(jumpCheckVector, Vector3.down, out raycastHit, aiModule.MainModule.groundLayer))
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

		private bool HitCheck() //Make
		{
			if (aiModule.MainModule.IsHit)
			{
				return true;
			}
			return false;
		}
		private bool LockOnCheck() //Make
		{
			return aiModule.MainModule.LockOn;
		}


		private bool HostileCheck() //Make
		{
			return aiModule.IsHostilities;
		}

		private float time1f = 1f;
		private bool Time1fCondition()
		{
			if(time1f > 0f)
			{
				time1f -= Time.deltaTime;
				return false;
			}
			time1f = 1f;
			return true;
		}
		private bool JumpAndTime1fCondition() //Make
		{
			return GroundCondition() & Time1fCondition();
		}
		private bool GroundCondition() //Make
		{
			return aiModule.MainModule.isGround;
		}
		private bool NotGroundCondition() //Make
		{
			return !aiModule.MainModule.isGround;
		}
		private bool CheckHPPercent50Condition() //Make
		{
		    if(GetPercent() < 0.5f)
		    { 
			    return true;
		    }
		    return false;
		}
		private bool CheckHPPercent30Condition() //Make
		{
			if(GetPercent() < 0.3f)
			{ 
				return true;
			}
			return false;
		}
		private bool CheckHPPercent20Condition() //Make
		{
			if(GetPercent() < 0.2f)
			{ 
				return true;
			}
			return false;
		}
		private bool NoneCondition() //Make
		{
			return true;
		}
		private bool RageGaugeOverCheck()
        {
			if(rageGauge >= 100f)
            {
				return true;
			}
			return false;
		}
		private bool RageGaugeUnderCheck()
		{
			if (rageGauge <= 0f)
			{
				return true;
			}
			return false;
		}


		//Action
		private void SuspicionGaugeSet() //Make
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
		private void LockOnPlayer() //Make
		{
			aiModule.MainModule.LockOn = true;
			aiModule.MainModule.LockOnTarget = aiModule.Player;
		}

		
		private void TargetFind() //Make
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

			//�߰߰�
			if (targetAngle <= aiSO.ViewAngle * 0.5f && !Physics.Raycast(Position, targetDir, aiSO.ViewRadius, aiSO.ObstacleMask) && (targetPos - Position).sqrMagnitude < aiSO.ViewRadius * aiSO.ViewRadius)
			{
				aiModule.LastFindPlayerPos = targetPos;
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

		private void Reset() //Make
		{
			aiModule.MainModule.Attacking = false;
			aiModule.MainModule.StrongAttacking = false;
			aiModule.MainModule.IsJump = false;
			aiModule.MainModule.IsJumpBuf = false;
		}
		private void MoveReset() //Make
		{
			aiModule.AIModuleState = AIModule.AIState.Idle;
			aiModule.MainModule.IsSprint = false;
			aiModule.MainModule.ObjDir = Vector2.zero;
		}

		private void Attack() //Make
		{
			aiModule.AIModuleState = AIModule.AIState.Attack;
			aiModule.MainModule.Attacking = true;
		}
		private void StrongAttack() //Make
		{
			aiModule.MainModule.ObjDir = Vector2.zero;
			aiModule.AIModuleState = AIModule.AIState.Attack;
			aiModule.MainModule.StrongAttacking = true;
			//aiModule.MainModule.Attacking = true;
		}
		private void SkillWeapon() //Make
		{
			aiModule.MainModule.GetModuleComponent<SkillModule>(ModuleType.Skill).UseWeaponSkill();
		}
		private void SkillE() //Make
		{
			aiModule.MainModule.GetModuleComponent<SkillModule>(ModuleType.Skill).UseSkill("E");
		}
		private void SkillR() //Make
		{
			aiModule.MainModule.GetModuleComponent<SkillModule>(ModuleType.Skill).UseSkill("R");
		}

		private void FixiedMove()
		{
			Vector3 targetPos = aiModule.Player.position;
			if ((targetPos - Position).sqrMagnitude > (aiSO.FixedRadius + 1f) * (aiSO.FixedRadius + 1f))
			{
				RunMove();
			}
			else if((targetPos - Position).sqrMagnitude < (aiSO.FixedRadius - 1f) * (aiSO.FixedRadius - 1f))
			{
				WalkAwayMove();
			}
		}
		

		private void CloserMove() //Make
		{
			if (aiModule.MainModule.CanMove && !aiModule.MainModule.Attacking && !aiModule.MainModule.StrongAttacking)
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
		
		private void WalkAwayMove() //Make
		{
			if (aiModule.MainModule.CanMove && !aiModule.MainModule.Attacking && !aiModule.MainModule.StrongAttacking)
			{
				PathSetting();
				Vector3 vec = Vector3.zero;
				vec =  Position - aiModule.Player.position;
				vec.y = 0;
				vec = vec.normalized;
				Vector2 _inputdir = new Vector2(vec.x, vec.z);
				aiModule.Input = _inputdir;

				aiModule.AIModuleState = AIModule.AIState.Walk;
				aiModule.MainModule.IsSprint = false;
				aiModule.MainModule.ObjDir = aiModule.Input;
			}
		}
		
		private void RunAwayMove() //Make
		{
			if (aiModule.MainModule.CanMove && !aiModule.MainModule.Attacking && !aiModule.MainModule.StrongAttacking)
			{
				PathSetting();
				Vector3 vec = Vector3.zero;
				vec =  Position - aiModule.Player.position;
				vec.y = 0;
				vec = vec.normalized;
				Vector2 _inputdir = new Vector2(vec.x, vec.z);
				aiModule.Input = _inputdir;

				aiModule.AIModuleState = AIModule.AIState.Run;
				aiModule.MainModule.IsSprint = true;
				aiModule.MainModule.ObjDir = aiModule.Input;
			}
		}
		
		private void SetMoveDir() //Make
		{
			if (aiModule.MainModule.CanMove && !aiModule.MainModule.Attacking && !aiModule.MainModule.StrongAttacking)
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

		private bool isGetAroundPos = false;
		private Vector3 aroundPos = Vector3.zero;
		private void AroundOriginPos()
		{
			if (aiModule.MainModule.CanMove && !aiModule.MainModule.Attacking && !aiModule.MainModule.StrongAttacking)
			{
				if (isGetAroundPos)
				{
					Vector2 _aroundPosXZ = new Vector2(aroundPos.x, aroundPos.z);
					Vector2 _transformPosXZ = new Vector2(Position.x, Position.z);
					if (Vector2.SqrMagnitude(_aroundPosXZ - _transformPosXZ) < 0.2f)
					{
						isGetAroundPos = false;
					}
					Vector3 vec = Vector3.zero;
					vec = aiModule.Player.position - Position;
					vec.y = 0;
					vec = vec.normalized;
					Vector2 _inputdir = new Vector2(vec.x, vec.z);
					aiModule.Input = _inputdir;
					aiModule.AIModuleState = AIModule.AIState.Walk;
					aiModule.MainModule.IsSprint = false;
					aiModule.MainModule.ObjDir = aiModule.Input;	
				}
				else
				{
					int _count = 3;
					while (_count-- > 0)
					{
						RaycastHit _raycastHit;
						Vector3 _aroundFindPos = new Vector3(aiModule.OriginPos.x + Random.Range(-aiSO.AroundRadius, aiSO.AroundRadius) , Position.y + 20, aiModule.OriginPos.z + Random.Range(-aiSO.AroundRadius, aiSO.AroundRadius));
						if (Physics.Raycast(_aroundFindPos, Vector3.down, out _raycastHit, aiSO.GroundMask))
						{
							aroundPos = _raycastHit.point;
							break;
						}
					}

					isGetAroundPos = true;
				}
			}
		}
		private void AroundLastFindPlayerPos()
		{
			if (aiModule.MainModule.CanMove && !aiModule.MainModule.Attacking && !aiModule.MainModule.StrongAttacking)
			{
				if (isGetAroundPos)
				{
					Vector2 _aroundPosXZ = new Vector2(aroundPos.x, aroundPos.z);
					Vector2 _transformPosXZ = new Vector2(Position.x, Position.z);
					if (Vector2.SqrMagnitude(_aroundPosXZ - _transformPosXZ) < 0.2f)
					{
						isGetAroundPos = false;
					}
					Vector3 vec = Vector3.zero;
					vec = aiModule.Player.position - Position;
					vec.y = 0;
					vec = vec.normalized;
					Vector2 _inputdir = new Vector2(vec.x, vec.z);
					aiModule.Input = _inputdir;
					aiModule.AIModuleState = AIModule.AIState.Walk;
					aiModule.MainModule.IsSprint = false;
					aiModule.MainModule.ObjDir = aiModule.Input;	
				}
				else
				{
					int _count = 3;
					while (_count-- > 0)
					{
						RaycastHit _raycastHit;
						Vector3 _aroundFindPos = new Vector3(aiModule.LastFindPlayerPos.x + Random.Range(-aiSO.AroundRadius, aiSO.AroundRadius) , Position.y + 20, aiModule.LastFindPlayerPos.z + Random.Range(-aiSO.AroundRadius, aiSO.AroundRadius));
						if (Physics.Raycast(_aroundFindPos, Vector3.down, out _raycastHit, aiSO.GroundMask))
						{
							aroundPos = _raycastHit.point;
							break;
						}
					}

					isGetAroundPos = true;
				}
			}
		}

		private void RunMove()//Make
		{
			if (aiModule.MainModule.CanMove && !aiModule.MainModule.Attacking && !aiModule.MainModule.StrongAttacking)
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
				aiModule.Input = Vector2.Lerp(aiModule.Input, _inputdir, Time.deltaTime * aiSO.runMoveInputSpeed);

				aiModule.AIModuleState = AIModule.AIState.Run;
				aiModule.MainModule.IsSprint = true;
				aiModule.MainModule.ObjDir = aiModule.Input;
			}
		}
		private void JumpAndRunMove() //Make
		{
			if (aiModule.MainModule.CanMove && !aiModule.MainModule.Attacking && !aiModule.MainModule.StrongAttacking)
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

		private void Ignore() //Make
		{
			aiModule.Input = Vector2.Lerp(aiModule.Input, Vector2.zero, Time.deltaTime * 10);
			aiModule.AIModuleState = AIModule.AIState.Idle;
			aiModule.MainModule.IsSprint = false;
			aiModule.MainModule.ObjDir = aiModule.Input;
		}

		private void Jump() //Make 
		{
			aiModule.AIModuleState = AIModule.AIState.Jump;
			aiModule.MainModule.IsJump = true;
			aiModule.MainModule.IsJumpBuf = true;
		}

		private void Rotate() //Make
		{
			Vector3 _vec = (aiModule.Player.position - Position).normalized;
			_vec.y = aiModule.MainModule.transform.forward.y;
			aiModule.MainModule.transform.forward = Vector3.Lerp(aiModule.MainModule.transform.forward, _vec, Time.deltaTime * aiSO.rotateMoveInputSpeed);
		}

		private void RotateXYZ() //Make
		{
			Vector3 _vec = (aiModule.Player.position - Position).normalized;
			aiModule.MainModule.transform.forward = Vector3.Lerp(aiModule.MainModule.transform.forward, _vec, Time.deltaTime * aiSO.rotateMoveInputSpeed);
		}

		private void ModelRotateXYZ() //Make
		{
			Vector3 _vec = (aiModule.Player.position - Position).normalized;
			aiModule.MainModule.Model.forward = Vector3.Lerp(aiModule.MainModule.Model.forward, _vec, Time.deltaTime * aiSO.rotateMoveInputSpeed);
		}
		private void HostileStart()
		{
			aiModule.IsHostilities = true;
		}

		private void TrackMove() //Make
		{
			if (aiModule.MainModule.CanMove && !aiModule.MainModule.Attacking)
			{
				Vector3 vec = aiModule.SmoothPath.EvaluateTangent(aiModule.SmoothPath.FindClosestPoint(aiModule.MainModule.transform.position, 0, -1, 2));

				Vector2 _inputdir = new Vector2(vec.x, vec.z);
				aiModule.Input = _inputdir;

				aiModule.AIModuleState = AIModule.AIState.Walk;
				aiModule.MainModule.IsSprint = false;
				aiModule.MainModule.ObjDir = aiModule.Input;
			}
		}

		private void RageOn()
		{
			isRage = true;
		}
		private void RageOff()
		{
			isRage = false;
		}
		private void Nothing()
		{
		}

		//string Action
		private void EquipWeapon(string _str)
		{
			aiModule.MainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon).ChangeWeapon(_str, null);
		}

		//float Action
		private void AddRageGauge(float _add)
		{
			rageGauge += _add;
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

		private float GetPercent()
		{
			return (float)aiModule.MainModule.StatData.CurrentHp / aiModule.MainModule.StatData.MaxHp;
		}

	}
}
