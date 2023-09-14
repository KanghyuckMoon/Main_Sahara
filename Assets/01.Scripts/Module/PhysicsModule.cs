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
    public class PhysicsModule : AbBaseModule
    {
        protected float rayDistance = 0.3f;
        protected ulong praviousHitBoxIndex = 0;

		protected BuffModule BuffModule
        {
            get
            {
                buffModule ??= mainModule.GetModuleComponent<BuffModule>(ModuleType.Buff);
                return buffModule;
            }
        }
		protected HitModule HitModule
        {
            get
            {
                hitModule ??= mainModule.GetModuleComponent<HitModule>(ModuleType.Hit);
                return hitModule;
            }
        }
		protected StateModule StateModule
        {
            get
            {
                stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
                return stateModule;
            }
        }
		protected JumpModule JumpModule
        {
            get
            {
                jumpModule ??= mainModule.GetModuleComponent<JumpModule>(ModuleType.Jump);
                return jumpModule;
            }
        }
        protected JumpModule jumpModule;
        protected HitModule hitModule;
        protected BuffModule buffModule;
		protected StateModule stateModule;

		protected Vector3 _spherePosition;

		protected string buffIconString = "_Icon";
		protected string buffEffectString = "_Effect";

		protected Coroutine knockBackCoroutine;
        protected System.Action landAction;

        public PhysicsModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }
        public PhysicsModule() : base()
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

			mainModule.SettingTime.SetTime(_inGameHitBox.HitBoxData.hitStunDelay, 0f);

			var _settingTime = _inGameHitBox.Owner.GetComponent<SettingTime>();

			if (_settingTime is not null)
			{
				_settingTime.SetTime(_inGameHitBox.HitBoxData.attackStunDelay, 0f);
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
		protected float previousAngle = 0;
        protected virtual void Slope()
        {
            var _transform = mainModule.transform;
            var _position = _transform.position;
            var _rayPos = new Vector3(_position.x, _position.y - mainModule.groundOffset, _position.z);
            var _ray = new Ray(_rayPos, Vector3.down);
            var _ray1 = new Ray(_rayPos, _transform.forward);

            if (Physics.Raycast(_ray, out var _raycastHit, 10f, mainModule.groundLayer))
            {
	            if (mainModule.isGround)
	            {
		            var _angle = Vector3.Angle(Vector3.up, _raycastHit.normal);

		            previousAngle = Physics.Raycast(_ray1, out var _raycastHit1, rayDistance,
			            mainModule.groundLayer)
			            ? Mathf.Lerp(previousAngle, _angle, 5 * mainModule.PersonalDeltaTime)
			            : Mathf.Lerp(previousAngle, 0, 5 * mainModule.PersonalDeltaTime);
		            mainModule.Animator.SetFloat("GrounDegree", previousAngle * mainModule.CanCrawlTheWall);

		            var _slopeLimit = mainModule.CharacterController.slopeLimit;
		            mainModule.IsSlope = _angle <= _slopeLimit + 3f;

		            mainModule.SlopeVector =
			            new Vector3(_raycastHit.normal.x, 0, _raycastHit.normal.z) * 5;

		            mainModule.isGround = mainModule.IsSlope;
	            }
	            else
	            {
		            mainModule.SlopeVector = Vector3.zero;
		            mainModule.isGround = false;
	            }
            }

            Debug.DrawRay(_rayPos, Vector3.down, Color.red);
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
                FallDamage();
                landAction?.Invoke();
            }

            mainModule.isTouchGround = _isLand;
            mainModule.isGround = _isLand;
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
            if (JumpModule.gravityWeight <= -30)
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
            Pool.ClassPoolManager.Instance.RegisterObject<PhysicsModule>(this);
        }
        public override void OnDestroy()
        {
            hitModule = null;
            stateModule = null;
            mainModule = null;
            landAction = null;
            base.OnDestroy();
            Pool.ClassPoolManager.Instance.RegisterObject<PhysicsModule>(this);
        }

        public void AddLandAction(System.Action action)
        {
            landAction += action;
        }
    }
}