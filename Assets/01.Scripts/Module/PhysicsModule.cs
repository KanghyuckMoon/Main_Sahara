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
        private HitModule hitModule;
        private PlayerLandEffectSO effect;

        private float effectSpownDelay = 1.2f;
        private float currenteffectSpownDelay;

        public PhysicsModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            hitModule = mainModule.GetModuleComponent<HitModule>(ModuleType.Hit);
            effect = AddressablesManager.Instance.GetResource<PlayerLandEffectSO>("PlayerLandEffectSO");
        }

        public override void OnTriggerEnter(Collider other)
        {
            foreach (string _tagName in mainModule.hitCollider)
            {
                if (other.CompareTag(_tagName) && !mainModule.isDead)
                {
                    StateModule otherStateModule = other.GetComponentInParent<AbMainModule>().GetModuleComponent<StateModule>(ModuleType.State);
                    hitModule.GetHit(otherStateModule.AdAttack);
                    otherStateModule.ChargeMana(10);
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
            Vector3 rayPos = new Vector3(mainModule.transform.position.x, mainModule.transform.position.y + mainModule.groundOffset,
                mainModule.transform.position.z);

            Ray ray = new Ray(rayPos, Vector3.down);
            if (Physics.Raycast(ray, out mainModule.slopeHit, rayDistance, mainModule.groundLayer))
            {
                var angle = Vector3.Angle(Vector3.up, mainModule.slopeHit.normal);
                mainModule.isSlope = (angle != 0f) && angle < mainModule.maxSlope;
            }
            mainModule.isSlope = false;
        }

        private void GroundCheack()
        {
            Vector3 _spherePosition = new Vector3(mainModule.transform.position.x, mainModule.transform.position.y + mainModule.groundOffset,
                mainModule.transform.position.z);
            bool _isLand = Physics.CheckSphere(_spherePosition, 0.42f, mainModule.groundLayer,
                QueryTriggerInteraction.Ignore);

            if(!mainModule.isGround && _isLand)
            {
                EffectManager.Instance.SetEffectDefault(effect.landEffectName, mainModule.transform.position, Quaternion.identity);
            }

            mainModule.isGround = _isLand;
        }

        private void SetEffect()
        {
            if(mainModule.isGround && mainModule.objDir != Vector2.zero)
            {
                if(currenteffectSpownDelay > effectSpownDelay)
                {
                    currenteffectSpownDelay = 0;
                    if (mainModule.isSprint)
                        EffectManager.Instance.SetEffectDefault(effect.runEffectName, mainModule.transform.position, Quaternion.identity);
                    else
                        EffectManager.Instance.SetEffectDefault(effect.walkEffectName, mainModule.transform.position, Quaternion.identity);
                }
                currenteffectSpownDelay += Time.deltaTime;
            }
        }
    }
}