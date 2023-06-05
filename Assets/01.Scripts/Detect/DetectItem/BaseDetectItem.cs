using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Effect;
using UnityEngine.Serialization;

namespace Detect
{
    public class BaseDetectItem : MonoBehaviour, IDetectItem
    {
        private static Dictionary<string, bool> isSpawnDic = new Dictionary<string, bool>();
        private static int nameKey;
        [SerializeField] 
        private string key;

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
        protected UnityEvent getoutEventBefore;
        
        [FormerlySerializedAs("getoutEvent")] [SerializeField]
        protected UnityEvent getoutEventAfter;

        [SerializeField]
        protected bool isInitFalse = true;
        
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
        
        protected virtual void Awake()
        {
            if (isSpawnDic.TryGetValue(key, out bool _bool))
            {
                if (_bool)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    _bool = true;
                }
            }
            upPos = targetModel.position;
            targetModel.position = new Vector3(targetModel.position.x, targetHeightTransform.position.y, targetModel.position.z);
            if (isInitFalse)
            {
                targetModel.gameObject.SetActive(false);
            }
        }

        public virtual void GetOut()
        {
            if (isGetOut)
            {
                return;
            }
            getoutEventBefore?.Invoke();
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
                getoutEventAfter?.Invoke();
            });
        }
        
        #if UNITY_EDITOR
        public LayerMask debug_LayerMask;
        
        
        [ContextMenu("RandomName")]
        public void RandomName()
        {
            var _prefeb = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(gameObject);
            //gameObject.name = _prefeb.name + nameKey++;
            key = _prefeb.name + nameKey++;

            
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }

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
