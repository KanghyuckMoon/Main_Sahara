using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using DG.Tweening;

namespace Detect
{
    public class BaseDetectItem : MonoBehaviour, IDetectItem
    {
        public DetectItemType DetectItemType
        {
            get
            {
                return detectItemType;
            }

            set
            {
                detectItemType = value;
            }
        }

        [SerializeField]
        protected DetectItemType detectItemType;

        [SerializeField] 
        protected Transform targetHeightTransform;
        
        [SerializeField] 
        protected Transform targetTransform;

        [SerializeField] 
        protected float heightUpTime = 2f;

        protected bool isGetOut = false;
        
        public virtual void GetOut()
        {
            if (isGetOut)
            {
                return;
            }
            isGetOut = true;
            Vector3 _movePos = targetHeightTransform.transform.position;
            targetTransform.DOMove(_movePos, heightUpTime);
        }
    }   
}
