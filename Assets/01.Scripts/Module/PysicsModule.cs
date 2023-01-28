using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public class PysicsModule : AbBaseModule
    {
        private float rayDistance = 0.5f;

        public PysicsModule(MainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {

        }

        public override void OnCollisionEnter(Collision collision)
        {
            foreach (string _tagName in mainModule.hitCollider)
            {
                if (collision.collider.tag == _tagName)
                {

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
            mainModule.isGround = Physics.CheckSphere(_spherePosition, 0.35f, mainModule.groundLayer,
                QueryTriggerInteraction.Ignore);
        }
    }
}