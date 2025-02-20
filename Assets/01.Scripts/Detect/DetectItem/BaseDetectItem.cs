using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
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

        protected Vector3 upPos;
        
        [SerializeField]
        protected UnityEvent getoutEvent;

        public List<Observer> Observers
        {
            get
            {
                return observers;
            }
        }

        private List<Observer> observers = new List<Observer>();
        
        public bool IsGetOut
        {
            get
            {
                return isGetOut;
            }
            set
            {
                isGetOut = value;
            }
        }

        protected bool isGetOut = false;
        
#if  UNITY_EDITOR

        public LayerMask debug_LayerMask;
        
#endif
        
        protected virtual void Start()
        {
            upPos = targetModel.position;
            targetModel.position = new Vector3(targetModel.position.x, targetHeightTransform.position.y, targetModel.position.z);
            targetModel.gameObject.SetActive(false);
        }

        public virtual void GetOut()
        {
            if (isGetOut)
            {
                return;
            }
            targetModel.gameObject.SetActive(true);
            isGetOut = true;
            Vector3 _movePos = upPos;
            var _effectObj = EffectManager.Instance.SetAndGetEffectDefault( effectAddress, targetEffectTrm.position, Quaternion.identity);
            targetModel.DOMove(_movePos,  heightUpTime);
            targetTransform.DOShakePosition(heightUpTime, new Vector3(1,0,1) * shakeStrength).OnComplete(() =>
            {
                _effectObj.Pool();
                gameObject.SetActive(false);
                isGetOut = true;
                getoutEvent?.Invoke();
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

        [ContextMenu("SetPosIsModelPos")]
        public void SetPosIsModelPos()
        {
            transform.position = targetModel.transform.position;
        }
        
        [ContextMenu("SetEffectPosIsThisPos")]
        public void SetEffectPosIsThisPos()
        {
            targetEffectTrm.position = transform.position;
        }
        
        
        [ContextMenu("SetModelPosIsThisPos")]
        public void SetModelPosIsThisPos()
        {
            targetModel.position = transform.position;
        }
        
        #endif
    }   
}
