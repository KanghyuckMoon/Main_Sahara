using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using DG.Tweening;
using Effect;

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
        protected Transform targetModel;
        
        [SerializeField] 
        protected Transform targetEffectTrm;
        
        [SerializeField] 
        protected string effectAddress;

        [SerializeField] 
        protected float heightUpTime = 2f;
            
        [SerializeField] 
        protected float shakeStrength = 0.5f;

        protected bool isGetOut = false;

        private Vector3 upPos;

#if  UNITY_EDITOR

        public LayerMask debug_LayerMask;
        
#endif
        
        private void Start()
        {
            upPos = targetModel.position;
            targetModel.position = new Vector3(targetModel.position.x, targetHeightTransform.position.y, targetModel.position.z);
        }

        public virtual void GetOut()
        {
            if (isGetOut)
            {
                return;
            }
            isGetOut = true;
            Vector3 _movePos = upPos;
            var _effectObj = EffectManager.Instance.SetAndGetEffectDefault( effectAddress, targetEffectTrm.position, Quaternion.identity);
            targetModel.DOMove(_movePos,  heightUpTime);
            targetTransform.DOShakePosition(heightUpTime, new Vector3(1,0,1) * shakeStrength).OnComplete(() =>
            {
                _effectObj.Pool();
                gameObject.SetActive(false);
            });
        }
        
        #if UNITY_EDITOR

        [ContextMenu("SetHeight")]
        public void SetHeight()
        {
            RaycastHit _hit;
            if (Physics.Raycast(transform.position, Vector3.down, out _hit,50,  debug_LayerMask))
            {
                transform.position = _hit.point;
            }
        }
        
        [ContextMenu("SetEffectPosIsThisPos")]
        public void SetEffectPosIsThisPos()
        {
            targetEffectTrm.position = transform.position;
        }
        
        #endif
    }   
}
