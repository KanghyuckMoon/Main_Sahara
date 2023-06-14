using System.Collections;
using System.Collections.Generic;
using EnemyComponent;
using UnityEngine;
using UnityEngine.Events;

namespace Module
{
    public class VayuEyeLocationHitbox : LocationHitBox
    {

        [SerializeField] private RotateEyes rotateEyes;
        
        protected override void OnTriggerEnter(Collider other)
        {
            PhysicsModule _physicsModule = mainModule.GetModuleComponent<PhysicsModule>(ModuleType.Physics);
            if (!_physicsModule.CheckTagName(other))
            {
                return;
            }
            if (rotateEyes.VayuEyeState == VayuEyeState.Normal)
            {
                rotateEyes.EyeHit();
                _physicsModule.InvokeActionFeedback(other, false, transform.position);
            }
            else if(rotateEyes.VayuEyeState == VayuEyeState.Defenseless)
            {
                //base.OnTriggerEnter(other);
                _physicsModule.OnTriggerEnter(other, this, hitEvent);
                _physicsModule.InvokeActionFeedback(other, false, transform.position);
            }
        }
		
    }
}