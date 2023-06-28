using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using DG.Tweening;
using Effect;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace Detect
{
    public class SlideDetectItem : BaseDetectItem
    {
        [SerializeField] private GameObject rewardObj;
        [SerializeField] private int changeLayer;
        private int originMask;
        protected override void Awake()
        {
            originMask = targetModel.gameObject.layer;
        }
        public override void GetOut()
        {
            if (isGetOut)
            {
                return;
            }
            targetModel.gameObject.layer = changeLayer;
            isGetOut = true;
            Vector3 _movePos = targetHeightTransform.position;
            var _effectObj = EffectManager.Instance.SetAndGetEffectDefault( effectAddress, targetEffectTrm.position, Quaternion.identity);
            targetModel.DOMove(_movePos,  heightUpTime);
            rewardObj.gameObject.SetActive(true);
            targetTransform.DOShakePosition(heightUpTime, new Vector3(1,0,1) * shakeStrength).OnComplete(() =>
            {
                _effectObj.Pool();
                gameObject.SetActive(false);
                isGetOut = true;
                targetModel.gameObject.layer = originMask;
                getoutEventAfter?.Invoke();
            });
        }
        
        #if UNITY_EDITOR
        [ContextMenu("SetHeightRewardObj")]
        public void SetHeightRewardObj()
        {
            RaycastHit _hit;
            if (Physics.Raycast(rewardObj.transform.position, Vector3.down, out _hit,50,  debug_LayerMask))
            {
                rewardObj.transform.position = _hit.point;
            }
        }
        
        [ContextMenu("LogNotRewardObject")]
        public void LogNotRewardObject()
        {
            if (rewardObj == null)
            {
                Debug.Log("null rewardObject", gameObject);
            }
            else if (rewardObj.name != "Reward")
            {
                Debug.Log("not rewardObject", gameObject);
            }
        }

        [ContextMenu("AddFunction")]
        public void AddFunction()
        {
            if (rewardObj == null)
            {
                return;
            }
            var _function = rewardObj.GetComponent<IReturnFunction>();
            if (_function is not null)
            {
                getoutEventAfter.AddListener(_function.ReturnFunction());
                EditorSceneManager.MarkSceneDirty(gameObject.scene);
            }
            
        }
        
        #endif
    }   
}
