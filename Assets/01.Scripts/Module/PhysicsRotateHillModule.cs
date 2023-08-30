using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Effect;
using Utill.Addressable;
using Utill;
using HitBox;
using Data;
using Attack;
using Buff;
using DG.Tweening;
using Skill;
using Item;
using Pool;
using System.Runtime.InteropServices.ComTypes;

namespace Module
{
	public enum MovementDirection
	{
		Up,
		Right,
		Down,
		Left
	}

	public class PhysicsRotateHillModule : AbBaseModule
    {
        private float rayDistance = 0.3f;
        private ulong praviousHitBoxIndex = 0;

        private BuffModule BuffModule
        {
            get
            {
                buffModule ??= mainModule.GetModuleComponent<BuffModule>(ModuleType.Buff);
                return buffModule;
            }
        }
        private HitModule HitModule
        {
            get
            {
                hitModule ??= mainModule.GetModuleComponent<HitModule>(ModuleType.Hit);
                return hitModule;
            }
        }
        private StateModule StateModule
        {
            get
            {
                stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
                return stateModule;
            }
        }
        private JumpModule JumpModule
        {
            get
            {
                jumpModule ??= mainModule.GetModuleComponent<JumpModule>(ModuleType.Jump);
                return jumpModule;
            }
        }
        private MoveModule MoveModule
        {
            get
			{
				moveModule ??= mainModule.GetModuleComponent<MoveModule>(ModuleType.Move);
				return moveModule;
            }
        }
        private JumpModule jumpModule;
        private HitModule hitModule;
        private BuffModule buffModule;
        private StateModule stateModule;
        private MoveModule moveModule;

        private Vector3 _spherePosition;

        private string buffIconString = "_Icon";
        private string buffEffectString = "_Effect";

        private Coroutine knockBackCoroutine;
        protected System.Action landAction;

        public PhysicsRotateHillModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }
        public PhysicsRotateHillModule() : base()
        {

        }

        public bool CheckTagName(Collider other)
        {
            foreach (string _tagName in mainModule.HitCollider)
            {
                if (other.CompareTag(_tagName) && !mainModule.IsDead)
                {
                    return true;
                }
            }
            return false;
        }

        public void InvokeActionFeedback(Collider other, bool isDefaultPos = true, Vector3 point = default)
        {
            AttackFeedBack _attackFeedBack = other.GetComponent<AttackFeedBack>();
            InGameHitBox _inGameHitBox = other.GetComponent<InGameHitBox>();
            foreach (var _s in _inGameHitBox.HitBoxData.hitEffect)
            {
                if (isDefaultPos)
                {
                    _attackFeedBack.InvokeEvent(other.ClosestPoint(mainModule.transform.position), _s);
                }
                else
                {
                    _attackFeedBack.InvokeEvent(other.ClosestPoint(point), _s);
                }
            }
        }

        public void OnTriggerEnter(Collider other, LocationHitBox _locationHitBox, UnityEvent hitEvent = null)
        {
            foreach (string _tagName in mainModule.HitCollider)
            {
                if (other.CompareTag(_tagName) && !mainModule.IsDead)
                {
                    InGameHitBox _inGameHitBox = other.GetComponent<InGameHitBox>();
                    if(DeadZoneCheck(_tagName))
                    {
                        return;
                    }

                    if (!mainModule.IsCanHit)
                    {
                        if(IsHit(_inGameHitBox))
                        {
                            return;
                        }
                        praviousHitBoxIndex = _inGameHitBox.GetIndex();
                        StatData _statData = _inGameHitBox.Owner.GetComponent<StatData>();
                        Vector3 _closerPoint = other.ClosestPoint(_locationHitBox.transform.position);

						HitStopAndAction(_inGameHitBox);

                        KnockBack(_inGameHitBox, _closerPoint);

                        HitEffectAndFeedBack(other, _inGameHitBox, hitEvent);

						SetDeBuff(_inGameHitBox.HitBoxData.buffList);

                        if (other.CompareTag("Player_Weapon") || other.CompareTag("PlayerSkill"))
                        {
                            PlayersHitAndManaUp(other, _statData, _inGameHitBox, _locationHitBox, _closerPoint);
                        }
                        else
                        {
                            EnemysHitAndManaUp(other, _statData, _inGameHitBox, _locationHitBox);
						}
                    }
                    else
                    {
                        Block(_inGameHitBox);
					}
                }
            }

        }
        
        private void HitEffectAndFeedBack(Collider other, InGameHitBox _inGameHitBox, UnityEvent hitEvent)
		{
			AttackFeedBack _attackFeedBack = other.GetComponent<AttackFeedBack>();
			foreach (var _s in _inGameHitBox.HitBoxData.hitEffect)
			{
				_attackFeedBack.InvokeEvent(other.ClosestPoint(mainModule.transform.position + Vector3.up), _s);
			}
			hitEvent?.Invoke();
		}

        private void KnockBack(InGameHitBox _inGameHitBox, Vector3 _closerPoint)
		{
			if (knockBackCoroutine != null)
			{
				mainModule.StopCoroutine(knockBackCoroutine);
			}

			switch (_inGameHitBox.HitBoxData.hitBoxType)
			{
				case HitBoxType.Default:
					knockBackCoroutine = mainModule.StartCoroutine(HitKnockBack(_inGameHitBox, _closerPoint));
					break;
				case HitBoxType.DamageOnly:
					break;
			}
		}

        private void HitStopAndAction(InGameHitBox _inGameHitBox)
		{
			_inGameHitBox.HitBoxAction?.Invoke(HitBoxActionType.Hit);

			mainModule.SettingTime.SetTime(0.18f, 0f);

			var _settingTime = _inGameHitBox.Owner.GetComponent<SettingTime>();

			if (_settingTime is not null)
			{
				_settingTime.SetTime(0.18f, 0f);
			}
		}

        private void Block(InGameHitBox _inGameHitBox)
		{
			var _ani = _inGameHitBox.Owner.GetComponent<Animator>();
			_ani.SetBool("Hit", true);
			var _effect = ObjectPoolManager.Instance.GetObject("BlockedEffect");

			var _pos = (_inGameHitBox.Owner.transform.position + new Vector3(0, 1, 0)) -
					   (mainModule.transform.position + new Vector3(0, 1, 0));
			var _endpos = (mainModule.transform.position + new Vector3(0, 1, 0)) + _pos.normalized;

			_effect.transform.position = _endpos;
			_effect.SetActive(true);
		}

        private void PlayersHitAndManaUp(Collider other, StatData _statData, InGameHitBox _inGameHitBox, LocationHitBox _locationHitBox, Vector3 _closerPoint) 
		{
			int _totalMana = 0;
			int _manaCount = 0;
			if (_statData != null)
			{
				_inGameHitBox.Owner.GetComponent<BodyRotation>()?.SetChromaticAberration(0.3f);

				HitModule.GetHit(Mathf.RoundToInt(
					_statData.CalculateDamage(mainModule.StatData.PhysicalResistance,
						mainModule.StatData.MagicResistance) * _locationHitBox.AttackMulti), _inGameHitBox.HitBoxData.hitBoxType);
				_totalMana = _statData.ManaRegen + _statData.ChangeMana(_statData.ManaRegen);

				_manaCount = (_totalMana / 10);

				for (int i = 0; i < _manaCount; ++i)
				{
					MPBall mpBall = ObjectPoolManager.Instance.GetObject("MPBall")
						.GetComponent<MPBall>();
					mpBall.SetMPBall(_closerPoint, _statData.ChargeMana, _totalMana / _manaCount,
						_inGameHitBox.Owner);
				}
			}
			else
			{
				StatData _stat = other.GetComponent<InGameHitBox>().Owner.GetComponent<StatData>();
				HitModule.GetHit(other.GetComponent<IndividualObject>().damage, _inGameHitBox.HitBoxData.hitBoxType);
				_totalMana = _stat.ManaRegen + _statData.ChangeMana(_stat.ManaRegen);

				_manaCount = (_totalMana / 10);

				for (int i = 0; i < _manaCount; ++i)
				{
					MPBall mpBall = ObjectPoolManager.Instance.GetObject("MPBall")
						.GetComponent<MPBall>();
					mpBall.SetMPBall(_closerPoint, _stat.ChargeMana, _totalMana / _manaCount,
						_inGameHitBox.Owner);
				}
			}
		}
        private void EnemysHitAndManaUp(Collider other, StatData _statData, InGameHitBox _inGameHitBox, LocationHitBox _locationHitBox)
		{
			int _totalMana = 0;
			if (_statData != null)
			{
				HitModule.GetHit(Mathf.RoundToInt(
					_statData.CalculateDamage(mainModule.StatData.PhysicalResistance,
						mainModule.StatData.MagicResistance) * _locationHitBox.AttackMulti), _inGameHitBox.HitBoxData.hitBoxType);
				_totalMana = _statData.ManaRegen + _statData.ChangeMana(_statData.ManaRegen);
			}
			else
			{
				StatData _stat = other.GetComponent<InGameHitBox>().Owner.GetComponent<StatData>();
				HitModule.GetHit(other.GetComponent<IndividualObject>().damage, _inGameHitBox.HitBoxData.hitBoxType);
				_totalMana = _stat.ManaRegen + _statData.ChangeMana(_stat.ManaRegen);
			}
		}


        private bool IsHit(InGameHitBox _inGameHitBox)
		{
			if (_inGameHitBox is null) return true;
			if (_inGameHitBox.GetIndex() == praviousHitBoxIndex) return true;
			if ((_inGameHitBox.HitBoxData.hitType & mainModule.IgnoreHitType) != 0) return true;
            return false;
		}

        private bool DeadZoneCheck(string _tagName)
        {
			if (_tagName == "DeadZone")
			{
				HitModule.GetHit(1000000);
				return true;
			}
            return false;
		}

        private float CalculateAngle(Vector3 _from, Vector3 _to)
        {
            return Mathf.Atan2(_from.z - _to.z, _from.x - _to.x) * Mathf.Rad2Deg;
        }

        private IEnumerator HitKnockBack(InGameHitBox _inGameHitBox, Vector3 _closetPos)
        {
            Vector3 _dir;
            if (_inGameHitBox.IsContactDir)
			{
                _dir = (_closetPos - _inGameHitBox.transform.position).normalized;
            }
            else
            {
                _dir = (_inGameHitBox.KnockbackDir() * Vector3.forward);
            }
            Vector3 _shakeDir = Quaternion.Euler(0, -45, 0) * _dir;

            float _power = _inGameHitBox.KnockbackPower() / 10f;
            
            mainModule.Model.DOKill();
            mainModule.Model.localPosition = Vector3.zero;
            mainModule.Model.DOShakePosition(0.22f, _shakeDir * _power, 20);
            yield return new WaitForSeconds(0.22f);
            mainModule.Model.DOKill();
            mainModule.Model.localPosition = Vector3.zero;
           
            mainModule.attackedTime = 0f;
            mainModule.knockBackPower =  _inGameHitBox.KnockbackPower();
            mainModule.KnockBackVector = _dir;
            knockBackCoroutine = null;
        }
        public override void FixedUpdate()
        {
            GroundCheack();
            Slope();
		}
        private float previousAngle = 0;
        private void Slope()
        {
            var _transform = mainModule.transform;
            var _position = _transform.position;
            var _rayPos = mainModule.RaycastTarget.position;
			var _ray = new Ray(_rayPos, -_transform.up);
			var _ray2 = new Ray(_rayPos, -Vector3.up);

			if (Physics.Raycast(_ray, out var _raycastHit, 20f, mainModule.groundLayer))
			{
                float _distance = _raycastHit.distance;
				if(mainModule.groundDistance > _distance) 
                {
                    GroundIn(_rayPos, _raycastHit.normal);
				}
                else
				{
					GroundOut();
				}
				//ZRotationSlope();

				//
				// Calculate the desired tilt angle based on the ground normal.
				//float tiltAngle = Mathf.Rad2Deg * Mathf.Atan2(_raycastHit.normal.x, _raycastHit.normal.z);
				//MoveModule.SetZRotation(tiltAngle);
			}
			else if(Physics.Raycast(_ray2, out var _raycastHit2, 20f, mainModule.groundLayer))
			{
				float _distance = _raycastHit2.distance;
				if (mainModule.groundDistance > _distance)
				{
					GroundIn(_rayPos, _raycastHit2.normal);
				}
				else
				{
					GroundOut();
				}
			}
            else
            {
				GroundOut();
			}
            Debug.DrawRay(_rayPos, Vector3.down, Color.red);
        }

        private void GroundIn(Vector3 _rayPos, Vector3 _rayNormal)
		{
			var forward = mainModule.transform.forward;
			forward.y = 0;
			forward = forward.normalized;
			var _ray1 = new Ray(_rayPos, forward);
			var _angle = Vector3.Angle(Vector3.up, _rayNormal);

			previousAngle = Physics.Raycast(_ray1, out var _raycastHit1, rayDistance, mainModule.groundLayer)
				? Mathf.Lerp(previousAngle, _angle, 5 * mainModule.PersonalDeltaTime)
				: Mathf.Lerp(previousAngle, 0, 5 * mainModule.PersonalDeltaTime);
			mainModule.Animator.SetFloat("GrounDegree", previousAngle * mainModule.CanCrawlTheWall);


			var _slopeLimit = mainModule.CharacterController.slopeLimit;
			mainModule.IsSlope = _angle <= _slopeLimit + 5f;


			if (Vector3.Dot(forward, _rayNormal) > 0)
			{
				MoveModule.SetXRotation(_angle);
			}
			else
			{
				MoveModule.SetXRotation(-_angle);
			}

            ZRotationSlope(_rayNormal);

			mainModule.SlopeVector = new Vector3(_rayNormal.x, 0, _rayNormal.z) * 5;
		}

        private void GroundOut()
		{
			mainModule.IsSlope = true;
			MoveModule.SetXRotation(0f);
		}

		float CalculateTiltAngle(Vector3 groundNormal)
		{
			float angle = Vector3.Angle(Vector3.up, groundNormal);

			return angle;
		}

		private Vector3 preNormal;
		private MovementDirection preMovementDirection;

		private void ZRotationSlope(Vector3 curNormal)
        {
			//Vector3 forward = mainModule.transform.forward;
            //forward.y = 0;
			//forward = forward.normalized;
			//
			//var moveMentDirection = ClassifyDirection(forward);


			//if (Vector3.Distance(preNormal, curNormal) < 0.1f && (moveMentDirection == preMovementDirection))
			//{
			//	return;
			//}
			//else
			//{
			//	preNormal = curNormal;
			//}


			if (Physics.Raycast(mainModule.leftFeet.position + Vector3.up * 5, Vector3.down, out var _leftFootHit, 20f, mainModule.groundLayer)
				&& Physics.Raycast(mainModule.rightFeet.position + Vector3.up * 5, Vector3.down, out var _rightFootHit, 20f, mainModule.groundLayer))
			{

				//axisVector = mainModule.transform.right;
				//axisVector.y = 0;
				//axisVector = axisVector.normalized;


				//float _angle = 0f; 
                //Vector3 dir = Vector3.zero;
				//
				//
				//Vector3 axisVector = Vector3.zero;
				//switch (moveMentDirection)
				//{
				//	case MovementDirection.Up:
				//		axisVector = mainModule.transform.forward;
				//		axisVector.y = 0;
				//		axisVector = axisVector.normalized;
				//		dir = (_leftFootHit.point - _rightFootHit.point).normalized;
				//		_angle = Vector3.Angle(axisVector, dir);
				//		break;
				//	case MovementDirection.Right:
				//		axisVector = mainModule.transform.right;
				//		axisVector.y = 0;
				//		axisVector = axisVector.normalized;
				//		dir = (_leftFootHit.point - _rightFootHit.point).normalized;
				//		_angle = Vector3.Angle(axisVector, dir) - 180;
				//		break;
				//	case MovementDirection.Left:
				//		axisVector = -mainModule.transform.right;
				//		axisVector.y = 0;
				//		axisVector = axisVector.normalized;
				//		dir = (_leftFootHit.point - _rightFootHit.point).normalized;
				//		_angle = Vector3.Angle(axisVector, dir);
				//		break;
				//	case MovementDirection.Down:
				//		axisVector = -mainModule.transform.forward;
				//		axisVector.y = 0;
				//		axisVector = axisVector.normalized;
				//		break;
				//}
				//preMovementDirection = moveMentDirection;
				//axisVector = mainModule.transform.right;
				//axisVector.y = 0;
				//axisVector = axisVector.normalized;
				//
				//dir = (_leftFootHit.point - _rightFootHit.point).normalized;
				//_angle = Vector3.Angle(axisVector, dir);
				float distance = _leftFootHit.point.y - _rightFootHit.point.y;
				float _angle = -distance * Mathf.Rad2Deg;
				MoveModule.SetZRotation(_angle);
				Debug.Log($"{mainModule.gameObject.name} Y Distance {_angle}", mainModule.gameObject);
				//Debug.DrawRay(mainModule.transform.position, dir * 5, Color.blue);
			}
            else
			{
				MoveModule.SetZRotation(0f);
			}
		}

		public float AngleBetweenPointsAlongAxis(Vector3 point1, Vector3 point2, Vector3 axis)
		{
			// Calculate the vectors from the origin to the input points
			Vector3 v1 = point1 - Vector3.Dot(point1, axis) * axis;
			Vector3 v2 = point2 - Vector3.Dot(point2, axis) * axis;

			// Calculate the angle between the two vectors
			float angle = Vector3.Angle(v1, v2);

			// Determine the sign of the angle based on the axis and the cross product of the vectors
			Vector3 cross = Vector3.Cross(v1, v2);
			if (Vector3.Dot(cross, axis) < 0)
			{
				angle = -angle;
			}

			return angle;
		}

		public override void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_spherePosition, mainModule.GroundCheckRadius);
        }
        private void GroundCheack()
        {
            _spherePosition = new Vector3(mainModule.transform.position.x, mainModule.transform.position.y - mainModule.groundOffset,
                mainModule.transform.position.z);
            bool _isLand = Physics.CheckSphere(_spherePosition, mainModule.GroundCheckRadius, mainModule.groundLayer,
                QueryTriggerInteraction.Ignore);

            if (!mainModule.isGround && _isLand)
            {
                //FallDamage();
                landAction?.Invoke();
            }

            mainModule.isGround = _isLand && mainModule.IsSlope;
            if (mainModule.isGround)
            {
                mainModule.lastGroundPos = mainModule.transform.position;
            }
        }
        IEnumerator LandingDelay()
        {
            yield return new WaitForSeconds(0.3f);
            mainModule.StopOrNot = 1;
        }
        private void FallDamage()
        {
            if (JumpModule.gravityWeight <= -50)
            {
                HitModule.GetHit(20);
            }

            mainModule.Gravity = 0;
            JumpModule.gravityWeight = 0;
        }

        private void SetDeBuff(List<BuffData> _buffDatas)
        {
            if (_buffDatas.Count == 0) return;
            
            foreach (var _buffs in _buffDatas)
            {
                GetBuff(_buffs, buffModule)
                    .SetDuration(_buffs.duration)
                    .SetPeriod(_buffs.period)
                    .SetValue(_buffs.value)
                    .SetSprite(_buffs.buffs.ToString() + buffIconString)
                    .SetSpownObjectName(_buffs.buffs.ToString() + buffEffectString);
            }
        }
        private AbBuffEffect GetBuff(BuffData _buffs, BuffModule _bufmodule) => _buffs.buffs switch
        {
            Buffs.U_Healing => new Healing_Buf(_bufmodule),
            Buffs.U_ChangeMagicDef => new ChangeMagicResistance_Buf(_bufmodule),
            Buffs.U_ChangePhysicDef => new ChangePhysicResistance_Buf(_bufmodule),
            Buffs.None => null
        };
        public override void OnDisable()
        {
            hitModule = null;
            stateModule = null;
            mainModule = null;
            landAction = null;
            base.OnDisable();
            Pool.ClassPoolManager.Instance.RegisterObject<PhysicsRotateHillModule>(this);
        }
        public override void OnDestroy()
        {
            hitModule = null;
            stateModule = null;
            mainModule = null;
            landAction = null;
            base.OnDestroy();
            Pool.ClassPoolManager.Instance.RegisterObject<PhysicsRotateHillModule>(this);
        }

        public void AddLandAction(System.Action action)
        {
            landAction += action;
        }

		private MovementDirection ClassifyDirection(Vector3 forward)
		
            {// Calculate the angle between the player's local forward direction and the global forward direction (positive Z-axis)
			float angle = Vector3.Angle(Vector3.forward, forward);

			// Determine the direction based on the angle
			if (angle < 45f)
			{
				//Debug.Log($"{angle}, Right");
				return MovementDirection.Right;
			}
			else if (angle < 135f)
			{
				if (forward.x >= 0)
				{
					//Debug.Log($"{angle}, Down");
					return MovementDirection.Down;
				}
				else
				{
					//Debug.Log($"{angle}, Up");
					return MovementDirection.Up;
				}
			}
			else
			{
                //Debug.Log($"{angle}, Left");
				return MovementDirection.Left;
			}
		}
	}
}