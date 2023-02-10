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

        public PhysicsModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            hitModule = mainModule.GetModuleComponent<HitModule>(ModuleType.Hit);
        }

        public override void OnTriggerEnter(Collider other)
        {
            foreach (string _tagName in mainModule.hitCollider)
            {
                if (other.CompareTag(_tagName))
                {
                    if (!mainModule.isDead)
                    {
                        hitModule.GetHit(other.GetComponentInParent<AbMainModule>().GetModuleComponent<StateModule>(ModuleType.State).AdAttack, other.ClosestPoint(mainModule.transform.position));
                        //mainModule.transform.DOShakePosition(0.15f, 0.2f, 180, 160);
                    }
                }
            }
        }

        public override void FixedUpdate()
        {
            GroundCheack();

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
                EffectManager.Instance.SetEffectDefault("LandSandHitEffect", mainModule.transform.position, Quaternion.identity);
            }

            mainModule.isGround = _isLand;
        }
    }
}