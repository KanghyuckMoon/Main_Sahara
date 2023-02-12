using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Effect;
using Utill.Addressable;

namespace Module
{
    public class PhysicsModule : AbBaseModule
    {
        private float rayDistance = 0.5f;

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

        public override void OnTriggerEnter(Collider other)
        {
            foreach (string _tagName in mainModule.HitCollider)
            {
                if (other.CompareTag(_tagName) && !mainModule.IsDead)
                {
                    StateModule otherStateModule = other.GetComponentInParent<AbMainModule>()?.GetModuleComponent<StateModule>(ModuleType.State);
                    if (otherStateModule != null)
                    {
                        HitModule.GetHit(otherStateModule.AdAttack);
                        otherStateModule.ChargeMana(10);
                    }
                    else
                    {
                        HitModule.GetHit(other.GetComponent<IndividualObject>().damage);
                    }
                }
            }
        }

        public override void FixedUpdate()
        {
            GroundCheack();
            SetEffect();
            Slope();
        }

        private void Slope()
        {
            Vector3 rayPos = new Vector3(mainModule.transform.position.x, mainModule.transform.position.y + mainModule.GroundOffset,
                mainModule.transform.position.z);

            Ray ray = new Ray(rayPos, Vector3.down);
            RaycastHit _raycastHit;
            if (Physics.Raycast(ray, out _raycastHit, rayDistance, mainModule.GroundLayer))
            {
                mainModule.SlopeHit = _raycastHit;
                   var angle = Vector3.Angle(Vector3.up, mainModule.SlopeHit.normal);
                mainModule.IsSlope = (angle != 0f) && angle < mainModule.MaxSlope;
            }
            else
            {
                mainModule.SlopeHit = _raycastHit;
            }
            mainModule.IsSlope = false;
        }

        private void GroundCheack()
        {
            Vector3 _spherePosition = new Vector3(mainModule.transform.position.x, mainModule.transform.position.y + mainModule.GroundOffset,
                mainModule.transform.position.z);
            bool _isLand = Physics.CheckSphere(_spherePosition, 0.42f, mainModule.GroundLayer,
                QueryTriggerInteraction.Ignore);

            if(!mainModule.IsGround && _isLand)
            {
                EffectManager.Instance.SetEffectDefault(effect.landEffectName, mainModule.transform.position, Quaternion.identity);
            }

            mainModule.IsGround = _isLand;
        }

        private void SetEffect()
        {
            if(mainModule.IsGround && mainModule.ObjDir != Vector2.zero)
            {
                float delay = 1;
                if(currenteffectSpownDelay > effectSpownDelay)
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