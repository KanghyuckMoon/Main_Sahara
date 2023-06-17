
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public class BoxRaycaster
    {
        private BoxCollider triggerCollider;
        private Transform transform;

        public BoxCollider TriggerCollider => triggerCollider; 
        public BoxRaycaster(Transform _trm)
        {
            transform = _trm; 
            triggerCollider = transform.Find("TriggerCol").GetComponent<BoxCollider>();

        }
        public Collider[] MyCollisions () {
            Vector3 size =  triggerCollider.size;
            var lossyScale = triggerCollider.transform.lossyScale;
            Collider [] hitColliders = Physics.OverlapBox (
                triggerCollider.center + transform.position + TriggerCollider.transform.localPosition,
                new Vector3(size.x * lossyScale.x,
                    size.y * lossyScale.y,
                    size.z * lossyScale.z) /2, 
                Quaternion.identity
            );

            int i = 0;
            while (i < hitColliders.Length) {
                Debug.Log ("Hit : " + hitColliders [i].name);
                i++;
            }

            return hitColliders; 
        }
        public Collider[] MyCollisions (LayerMask mask) {
            Collider [] hitColliders = Physics.OverlapBox (
                triggerCollider.center + transform.position,
                triggerCollider.size / 2, 
                Quaternion.identity,mask
            );

            int i = 0;
            while (i < hitColliders.Length) {
                Debug.Log ("Hit : " + hitColliders [i].name);
                i++;
            }

            return hitColliders; 
        }
        public void OnDrawGizmos () {
            Vector3 _size = new Vector3(triggerCollider.transform.lossyScale.x * triggerCollider.size.x,
                triggerCollider.transform.lossyScale.y * triggerCollider.size.y,
                triggerCollider.transform.lossyScale.z * triggerCollider.size.z)
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube (
                triggerCollider.center + transform.position + TriggerCollider.transform.localPosition, 
                _size);
        }
    }
    
}
