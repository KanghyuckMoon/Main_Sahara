using System;
using System.Collections;
using System.Collections.Generic;
using Arena;
using DG.Tweening;
using UnityEngine;
using Utill.Addressable;

namespace Arena
{
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
        }
    }
}
