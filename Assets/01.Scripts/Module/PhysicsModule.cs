using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

namespace Module
{
    public class PhysicsModule : AbBaseModule
    {
        private float rayDistance = 0.25f;
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
        private JumpModule jumpModule;
        private HitModule hitModule;
        private BuffModule buffModule;
        private StateModule stateModule;

        private Vector3 _spherePosition;

        private string buffIconString = "_Icon";
        private string buffEffectString = "_Effect";

        public PhysicsModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }
        public PhysicsModule() : base()
        {

        }

        /*public override void OnTriggerEnter(Collider other)
        {
            foreach (string _tagName in mainModule.HitCollider)
            {
                if (other.CompareTag(_tagName) && !mainModule.IsDead && !mainModule.IsCanHit)
                {
                    InGameHitBox _inGameHitBox = other.GetComponent<InGameHitBox>();
                    if (_inGameHitBox is null) return;
                    if (_inGameHitBox.GetIndex() == praviousHitBoxIndex) return;
                    praviousHitBoxIndex = _inGameHitBox.GetIndex();
                    AttackFeedBack _attackFeedBack = other.GetComponent<AttackFeedBack>();
                    StatData _statData = _inGameHitBox.Owner.GetComponent<StatData>();

                    _inGameHitBox.Owner.GetComponent<SettingTime>().SetTime(_inGameHitBox.HitBoxData.hitStunDelay, 0.1f);
                    mainModule.SettingTime.SetTime(_inGameHitBox.HitBoxData.attackStunDelay, 0.1f);

                    mainModule.StartCoroutine(HitKnockBack(_inGameHitBox, other.ClosestPoint(_locationHitBox.transform.position)));
                    _attackFeedBack.InvokeEvent(other.ClosestPoint(mainModule.transform.position), _inGameHitBox.HitBoxData.hitEffect);
                    if (_statData != null)
                    {
                        HitModule.GetHit(Mathf.RoundToInt(_statData.CalculateDamage(mainModule.StatData.PhysicalResistance, mainModule.StatData.MagicResistance));
                        _statData.ChargeMana(mainModule.StatData.ManaRegen);
                    }
                    else
                    {
                        HitModule.GetHit(other.GetComponent<IndividualObject>().damage);
                    }
                }
            }
        }*/

        public void OnTriggerEnter(Collider other, LocationHitBox _locationHitBox)
        {
            foreach (string _tagName in mainModule.HitCollider)
            {
                if (other.CompareTag(_tagName) && !mainModule.IsDead && !mainModule.IsCanHit)
                {
                    InGameHitBox _inGameHitBox = other.GetComponent<InGameHitBox>();
                    if (_inGameHitBox is null) return;
                    if (_inGameHitBox.GetIndex() == praviousHitBoxIndex) return;
                    praviousHitBoxIndex = _inGameHitBox.GetIndex();
                    AttackFeedBack _attackFeedBack = other.GetComponent<AttackFeedBack>();
                    StatData _statData = _inGameHitBox.Owner.GetComponent<StatData>();

                    _inGameHitBox.Owner.GetComponent<SettingTime>().SetTime(_inGameHitBox.HitBoxData.hitStunDelay, 0.1f);
                    mainModule.SettingTime.SetTime(_inGameHitBox.HitBoxData.attackStunDelay, 0.1f);

                    mainModule.StartCoroutine(HitKnockBack(_inGameHitBox, other.ClosestPoint(_locationHitBox.transform.position)));
                    _attackFeedBack.InvokeEvent(other.ClosestPoint(mainModule.transform.position), _inGameHitBox.HitBoxData.hitEffect);

                    
                    SetDeBuff(_inGameHitBox.HitBoxData.buffList);
                    
                    mainModule.SettingTime.SetTime(0.15f, 0.1f);
                    _inGameHitBox.Owner.GetComponent<SettingTime>().SetTime(0.2f, 0.1f);
                    if (_statData != null)
                    {
                        HitModule.GetHit(Mathf.RoundToInt(_statData.CalculateDamage(mainModule.StatData.PhysicalResistance, mainModule.StatData.MagicResistance) * _locationHitBox.AttackMulti));
                        _statData.ChargeMana(_statData.ManaRegen + _statData.ChangeMana(_statData.ManaRegen));
                    }
                    else
                    {
                        HitModule.GetHit(other.GetComponent<IndividualObject>().damage);
                    }
                }
            }
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

            mainModule.Model.DOKill();
            mainModule.Model.localPosition = Vector3.zero;
            mainModule.Model.DOShakePosition(0.22f, _shakeDir * 0.5f, 20);
            yield return new WaitForSeconds(0.22f);
            mainModule.Model.DOKill();
            mainModule.Model.localPosition = Vector3.zero;
           
            mainModule.attackedTime = 0f;
            mainModule.knockBackPower =  _inGameHitBox.KnockbackPower();
            mainModule.KnockBackVector = _dir;
        }
        public override void FixedUpdate()
        {
            GroundCheack();
            Slope();
        }
        private void Slope()
        {
            Vector3 rayPos = new Vector3(mainModule.transform.position.x, mainModule.transform.position.y - mainModule.groundOffset,
                mainModule.transform.position.z);

            Ray ray = new Ray(rayPos, Vector3.down);
            RaycastHit _raycastHit;
            if (Physics.Raycast(ray, out _raycastHit, rayDistance, mainModule.groundLayer))
            {
                //mainModule.SlopeHit = _raycastHit;
                var angle = Vector3.Angle(Vector3.up, _raycastHit.normal);
                //Debug.LogError(angle + " : : : : " + Vector3.Angle(Vector3.up, _raycastHit.normal));
                mainModule.IsSlope = (angle < mainModule.CharacterController.slopeLimit) && (angle > -mainModule.CharacterController.slopeLimit); // 90 - mainModule.MaxSlope = �ö� �� �ִ� ����
                //return;
                if(mainModule.IsSlope is false)
                {
                    mainModule.SlopeVector = Vector3.ProjectOnPlane(new Vector3(0, mainModule.Gravity, 0), _raycastHit.normal);
                }
            }
            else
            {
                //Debug.LogError("�ȴ�ƴ�ƴ��");
                mainModule.IsSlope = true;
            }

            //Debug.DrawLine(rayPos, rayPos + new Vector3(0, 100, 0), Color.red);
            Debug.DrawRay(rayPos, Vector3.down, Color.red);
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_spherePosition, 0.25f);
        }

        private void GroundCheack()
        {
            _spherePosition = new Vector3(mainModule.transform.position.x, mainModule.transform.position.y - mainModule.groundOffset,
                mainModule.transform.position.z);
            bool _isLand = Physics.CheckSphere(_spherePosition, 0.2f, mainModule.groundLayer,
                QueryTriggerInteraction.Ignore);

            
            
            if (!mainModule.isGround && _isLand)
            {
                FallDamage();

                StateModule.RemoveState(State.JUMP);

                mainModule.KnockBackVector = Vector3.zero;
                //StatModule.
                
                mainModule.StartCoroutine(LandingDelay());
            }

            mainModule.isGround = _isLand && mainModule.IsSlope;
        }
        IEnumerator LandingDelay()
        {
            yield return new WaitForSeconds(0.3f);
            mainModule.StopOrNot = 1;
        }
        private void FallDamage()
        {
            if (JumpModule.gravityWeight <= -100)
            {
                //Debug.LogError("���� ����");
                HitModule.GetHit(20);
            }

            JumpModule.gravityWeight = 0;
        }
        //private AbBuffEffect 
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
            base.OnDisable();
            Pool.ClassPoolManager.Instance.RegisterObject<PhysicsModule>("PhysicsModule", this);
        }
        public override void OnDestroy()
        {
            hitModule = null;
            stateModule = null;
            mainModule = null;
            base.OnDestroy();
            Pool.ClassPoolManager.Instance.RegisterObject<PhysicsModule>("PhysicsModule", this);
        }
    }
}