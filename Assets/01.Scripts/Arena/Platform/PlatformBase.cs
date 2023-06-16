using System;
using System.Collections;
using System.Collections.Generic;
using Arena;
using DG.Tweening;
using UnityEngine;
using Utill.Addressable;

namespace Arena
{
    public class CollisionEvent
    {
        private BoxRaycaster boxRaycaster;
        private string targetTag; 
        private Action triggerAction;
        public CollisionEvent(Transform _trm, string _targetTag, Action _callback)
        {
            boxRaycaster = new BoxRaycaster(_trm);
            targetTag = _targetTag;
            triggerAction = _callback; 
        }

        public void CheckCol()
        {
            var _cols = boxRaycaster.MyCollisions();
            for (int i = 0; i < _cols.Length; i++)
            {
                if (_cols[i].transform.CompareTag(targetTag))
                {
                    triggerAction?.Invoke();
                    return; 
                }
            }
        }
    }

    public  class PlatformBase : MonoBehaviour, IArenaPlatform
    {
        [SerializeField] private string SOAddress; 
        [SerializeField] private PlatformDataSO platformDataSO;

        private List<Action> actionList = new List<Action>(); 

        // 프로퍼티
        public List<Action> ActionList => actionList;
        public float startDelay => platformDataSO.startDelay; 
        public float actionDelay => platformDataSO.actionDelay;
        public float tweenDuration => platformDataSO.tweenDuration;

        protected virtual void Awake()
        {
            platformDataSO ??= AddressablesManager.Instance.GetResource<PlatformDataSO>(SOAddress);
        }
        protected virtual void OnDisable()
        {
            DOTween.KillAll(); 
        }

        protected virtual void OnEnable()
        {
            StartAction();
        }

        public virtual void StartAction()
        {
          //  float random
            transform.DOShakePosition(2f, Vector3.up, 3, 90f, false, true, ShakeRandomnessMode.Harmonic).SetLoops(-1); 
        }
        
        
    }
}
