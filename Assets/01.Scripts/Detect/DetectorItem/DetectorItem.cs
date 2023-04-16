using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detect
{
    public class DetectorItem : MonoBehaviour
    {
        
        [SerializeField]
        protected float radius = 5f;

        [SerializeField] 
        protected LayerMask targetLayerMask;

        [SerializeField] 
        protected DetectItemType detectItemType;
        
        protected IDetectItem targetItem;

        protected float minDistance = 0f;
        
        private void GetNearObject()
        {
            GameObject obj = null;
            float minimumDistance = float.MaxValue;
            Collider[] targets = Physics.OverlapSphere(transform.position, radius,targetLayerMask);
            foreach (Collider col in targets)
            {
                Vector3 dir = col.transform.position - transform.position;
                if (dir.sqrMagnitude < minimumDistance)
                {
                    var component = col.gameObject.GetComponent<IDetectItem>();
                    if ((detectItemType & component.DetectItemType) != 0)
                    {
                        targetItem = component;
                        minimumDistance = dir.sqrMagnitude;
                    }
                }
            }

            minDistance = minimumDistance;

            if (targets.Length == 0)
            {
                targetItem = null;
                minDistance = float.MaxValue;
            }
        }

        public virtual void Detect()
        {
            
        }

        private void Update()
        {
            GetNearObject();
            Detect();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}

