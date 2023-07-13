
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Measurement;

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
            try
            {

                Transform[] allChildren = transform.GetComponentsInChildren<Transform>();
                foreach (var child in allChildren)
                {
                    if (child.name == "TriggerCol")
                    {
                        triggerCollider = child.GetComponent<BoxCollider>();
                        break; 
                    }
                }

                if (triggerCollider == null)
                {
                    Debug.LogError(transform.name + "TriggerCol 자식에 추가하세요");
                }
            }
            catch (Exception e)
            {
                Debug.LogError(transform.name + "TriggerCol 자식에 추가하세요");
            }
          
        }
        public Collider[] MyCollisions () {
            Vector3 size =  triggerCollider.size;
            var lossyScale = triggerCollider.transform.lossyScale;
            Vector3 center = new Vector3(
                (transform.position.x) + (triggerCollider.center.x +  TriggerCollider.transform.localPosition.x) * triggerCollider.transform.lossyScale.x,
                (transform.position.y) + (triggerCollider.center.y +  TriggerCollider.transform.localPosition.y) * triggerCollider.transform.lossyScale.y,
                (transform.position.z) + (triggerCollider.center.z +  TriggerCollider.transform.localPosition.z) * triggerCollider.transform.lossyScale.z
                );
            Quaternion rot = triggerCollider.transform.rotation;
            Collider [] hitColliders = Physics.OverlapBox (
                center,
                new Vector3(size.x * lossyScale.x,
                    size.y * lossyScale.y,
                    size.z * lossyScale.z) /2, 
                rot
            );

            int i = 0;
            while (i < hitColliders.Length) {
                Logging.Log ("Hit : " + hitColliders [i].name);
                i++;
            }

            return hitColliders; 
        }
        public Collider[] MyCollisions (LayerMask mask) {
            Collider [] hitColliders = Physics.OverlapBox (
                triggerCollider.center + transform.position+ TriggerCollider.transform.localPosition,
                triggerCollider.size / 2, 
                Quaternion.identity,mask
            );

            int i = 0;
            while (i < hitColliders.Length) {
                Logging.Log ("Hit : " + hitColliders [i].name);
                i++;
            }

            return hitColliders; 
        }
        public void OnDrawGizmos () {
            Vector3 _size = new Vector3(triggerCollider.transform.lossyScale.x * triggerCollider.size.x,
                triggerCollider.transform.lossyScale.y * triggerCollider.size.y,
                triggerCollider.transform.lossyScale.z * triggerCollider.size.z);
            Vector3 center = new Vector3(
                (transform.position.x) + (triggerCollider.center.x +  TriggerCollider.transform.localPosition.x) * triggerCollider.transform.lossyScale.x,
                (transform.position.y) + (triggerCollider.center.y +  TriggerCollider.transform.localPosition.y) * triggerCollider.transform.lossyScale.y,
                (transform.position.z) + (triggerCollider.center.z +  TriggerCollider.transform.localPosition.z) * triggerCollider.transform.lossyScale.z
            );
            Quaternion rot = triggerCollider.transform.rotation;

       //     Vector3 a = center * rot; 
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube (
                center , 
                _size );
        }
    }
    
}
