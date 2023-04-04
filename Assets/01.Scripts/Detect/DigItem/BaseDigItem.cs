using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detect
{
    public class BaseDigItem : MonoBehaviour
    {
        [SerializeField]
        protected float radius = 5f;
        [SerializeField] 
        private DetectItemType detectItemType;
        [SerializeField] 
        private LayerMask targetLayerMask;
        
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
                    if (detectItemType != component.DetectItemType)
                    {
                        continue;
                    }
                    targetItem = component;
                    minimumDistance = dir.sqrMagnitude;
                }
            }

            minDistance = minimumDistance;

            if (targets.Length == 0)
            {
                targetItem = null;
                minDistance = float.MaxValue;
            }
        }

        public void Dig()
        {
            GetNearObject();
            if(targetItem is not null)
            {
                Debug.Log("GetOut");
                targetItem.GetOut();
            }
        }

        private void OnEnable()
        {
            var _detectAnimationAction = transform.root.GetComponent<DetectAnimationAction>();
            if (_detectAnimationAction is not null)
            {
                Debug.Log("Success");
                _detectAnimationAction.ChangeAction(Dig);
            }
        }

        private void Update()
        {
            //Test Code
            if (Input.GetKeyDown(KeyCode.C))
            {
                GetNearObject();
                if(targetItem is not null)
                {
                    Debug.Log("GetOut");
                    targetItem.GetOut();
                }
            }
        }
    }
    
}

