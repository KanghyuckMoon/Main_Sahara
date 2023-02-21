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
using DG.Tweening;

namespace Module
{
    public class PhysicsModule : AbBaseModule
    {
        private float rayDistance = 0.5f;
        private ulong praviousHitBoxIndex = 0;
        private HitModule HitModule
        {
            get
            {
                hitModule ??= mainModule.GetModuleComponent<HitModule>(ModuleType.Hit);
                return hitModule;
            }
        }

        private HitModule hitModule;

        private PlayerLandEffectSO effect;

        private float effectSpownDelay => effect.effectDelayTime;
        private float currenteffectSpownDelay;
        private bool isRight;

        public PhysicsModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            effect = AddressablesManager.Instance.GetResource<PlayerLandEffectSO>("PlayerLandEffectSO");
        }
        public void OnTriggerEnter(Collider other, LocationHitBox _locationHitBox)
        {
            foreach (string _tagName in mainModule.HitCollider)
            {
                if (other.CompareTag(_tagName) && !mainModule.IsDead)
                {
                    
                    InGameHitBox _inGameHitBox = other.GetComponent<InGameHitBox>();
                    if (_inGameHitBox is null) return;
                    if (_inGameHitBox.GetIndex() == praviousHitBoxIndex) return;
                    praviousHitBoxIndex = _inGameHitBox.GetIndex();
                    AttackFeedBack _attackFeedBack = other.GetComponent<AttackFeedBack>();
                    StatData _statData = _inGameHitBox.Owner.GetComponent<StatData>();
                    mainModule.StartCoroutine(HitKnockBack(_inGameHitBox, other.ClosestPoint(_locationHitBox.transform.position)));
                    _attackFeedBack.InvokeEvent(other.ClosestPoint(mainModule.transform.position));
                    if (_statData != null)
                    {
                        HitModule.GetHit(Mathf.RoundToInt(_statData.MeleeAttack * _locationHitBox.AttackMulti));
                        _statData.ChargeMana(10);
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

            mainModule.KnockBackVector = _dir * _inGameHitBox.KnockbackPower();
        }

        public override void FixedUpdate()
        {
            GroundCheack();
            SetEffect();
            Slope();
        }

        private void Slope()
        {
            Vector3 rayPos = new Vector3(mainModule.transform.position.x, mainModule.transform.position.y + mainModule.groundOffset,
                mainModule.transform.position.z);

            Ray ray = new Ray(rayPos, Vector3.down);
            RaycastHit _raycastHit;
            if (Physics.Raycast(ray, out _raycastHit, rayDistance, mainModule.groundLayer))
            {
                mainModule.SlopeHit = _raycastHit;
                var angle = Vector3.Angle(Vector3.up, mainModule.SlopeHit.normal);
                mainModule.IsSlope = ((angle != 0f) && angle < mainModule.MaxSlope);
                //return;
            }
            else
            {
                mainModule.SlopeHit = _raycastHit;
            }
            //mainModule.IsSlope = false;
        }

        private void GroundCheack()
        {
            Vector3 _spherePosition = new Vector3(mainModule.transform.position.x, mainModule.transform.position.y + mainModule.groundOffset,
                mainModule.transform.position.z);
            bool _isLand = Physics.CheckSphere(_spherePosition, 0.36f, mainModule.groundLayer,
                QueryTriggerInteraction.Ignore);

            if (!mainModule.isGround && _isLand)
            {
                mainModule.KnockBackVector = Vector3.zero;
                EffectManager.Instance.SetEffectDefault(effect.landEffectName, mainModule.transform.position, Quaternion.identity);
            }

            mainModule.isGround = _isLand;
        }

        private void SetEffect()
        {
            if (mainModule.isGround && mainModule.ObjDir != Vector2.zero)
            {
                float delay = 1;
                if (currenteffectSpownDelay > effectSpownDelay)
                {
                    currenteffectSpownDelay = 0;

                    if (mainModule.IsSprint)
                    {
                        delay = effect.runEffectDelay;
                        EffectManager.Instance.SetEffectDefault(isRight ? effect.runREffectName : effect.runLEffectName, mainModule.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        EffectManager.Instance.SetEffectDefault(isRight ? effect.walkRffectName : effect.walkLffectName, mainModule.transform.position, Quaternion.identity);
                    }
                    isRight = !isRight;
                }
                currenteffectSpownDelay += Time.deltaTime * delay;
            }
        }
    }
}