using System;
using System.Collections;
using System.Collections.Generic;
using Arena;
using DG.Tweening;
using UnityEngine;
using UpdateManager;
using Utill.Addressable;

namespace Arena
{
    public class CollisionEvent
    {
        private BoxRaycaster boxRaycaster;
        private string targetTag; 
        private Action enterCallback;
        private Action stayStayCallback;
        private Action exitCallback; // 활성화 중이었다가 취소됐을 떄 액션 
        private bool isTrigger = false; 
        
        public CollisionEvent(Transform _trm, string _targetTag,  Action _enterCallback,Action _stayCallback, Action _exitCallback)
        {
            boxRaycaster = new BoxRaycaster(_trm);
            targetTag = _targetTag;
            enterCallback = _enterCallback; 
            stayStayCallback = _stayCallback;
            exitCallback = _exitCallback; 
        }

        public void DrawGizmo()
        {
            boxRaycaster.OnDrawGizmos();
        }

        public bool CheckCol()
        {
            bool _isCurTrigger = false;  // 현재 체크 트리거
            var _cols = boxRaycaster.MyCollisions();
            for (int i = 0; i < _cols.Length; i++)
            {
                if (_cols[i].transform.CompareTag(targetTag))
                {
                    if (isTrigger == false)
                    {
                        isTrigger = true;
                        enterCallback?.Invoke();
                    }
                    else
                    {
                        stayStayCallback?.Invoke();
                    }
                    return true; 
                }
            }
            CheckCancel(_isCurTrigger); 
            return false; 
        }

        /// <summary>
        /// 기존에 활성화중이었다가 취소되면 호출 
        /// </summary>
        /// <param name="_isCurTrigger"></param>
        public void CheckCancel(bool _isCurTrigger)
        {
            if (isTrigger == true && _isCurTrigger == false)
            {
                exitCallback?.Invoke();
                isTrigger = false; 
            }
        }
    }

    [System.Serializable]
    public class LandPlatform
    {
        private Transform targetTrm; 
        
        [SerializeField]
        private float moveDist; // 이동 거리 
        [SerializeField]
        private float moveSpeed;
        private Vector3 originPos;
        private bool isObjectOn = false; // 오브젝트가 위에 있는가 
        private bool isTargetPos; // 목표위치까지 움직였는가 

        private Vector3 targetPos; // 최종 이동 거리 

        public void Init(Transform _trm)
        {
            targetTrm = _trm; 
            originPos = targetTrm.position; 
            targetPos = new Vector3(originPos.x, originPos.y + moveDist, originPos.z);
        }
        public void IsObjectOn(bool isTrue)
        {
            isObjectOn = isTrue; 
        }
        
        /// <summary>
        /// 기본 위치로 되돌아가기 
        /// </summary>
        public void ResetPos()
        {
            targetTrm.DOMoveY(originPos.y, 0.5f);
            isObjectOn = false;
            isTargetPos = false; 
        }
        
        /// <summary>
        /// 이동 ㄱ (FixedUpdate에서)
        /// </summary>
        public void MovePower()
        {
            if (isTargetPos == true) return; 
            targetTrm.position = Vector3.MoveTowards(targetTrm.position, targetPos, moveSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator CheckPos()
        {
            WaitForSeconds w = new WaitForSeconds(0.1f); 
            while (isObjectOn == true)
            {
                // 도착 
                if ( (targetTrm.position - targetTrm.position).sqrMagnitude < 0.01f)
                {
                    isTargetPos = true;
                    targetTrm.DOMoveY(originPos.y +moveDist, 0.1f); 
                    yield break; 
                }

                yield return w; 
            }
        }
    }

    public  class PlatformBase : MonoBehaviour, IArenaPlatform, IUpdateObj
    {
        [SerializeField] private string SOAddress; 
        [SerializeField] private PlatformDataSO platformDataSO;
        [SerializeField]
        private LandPlatform landPlatform;

        [SerializeField, Header("착지 효과")] private bool isLandEffect = true; 
        private CollisionEvent collisionEvent;
        private List<Action> actionList = new List<Action>(); 

        // 프로퍼티
        public List<Action> ActionList => actionList;
        public float startDelay => platformDataSO.startDelay; 
        public float actionDelay => platformDataSO.actionDelay;
        public float tweenDuration => platformDataSO.tweenDuration;

        protected virtual void Awake()
        {
            platformDataSO ??= AddressablesManager.Instance.GetResource<PlatformDataSO>(SOAddress);
            collisionEvent = new CollisionEvent(transform, "Player", LandEffect,landPlatform.MovePower, LandExit);
        }

        private void Start()
        {
            landPlatform.Init(transform);
        }

        protected virtual void OnDisable()
        {
            DOTween.KillAll(); 
            UpdateManager.UpdateManager.Remove(this);
        }

        protected virtual void OnEnable()
        {
           // StartAction();
            UpdateManager.UpdateManager.Add(this);
        }

        private void OnDrawGizmos()
        {
            collisionEvent?.DrawGizmo();
        }

        [ContextMenu("ㄱㄱ")]
        public virtual void StartAction()
        {
          //  float random
            transform.DOShakePosition(2f, Vector3.up, 1, 1f).SetLoops(-1); 
        }
        
        /// <summary>
        /// 착지했을 때 효과 
        /// </summary>
        private  void LandEffect()
        {
            // 플랫폼 주변에 파티클 
            // 밑으로 내려가기 
            DOTween.KillAll(); 
            landPlatform.IsObjectOn(true); 
            StartCoroutine(landPlatform.CheckPos());
        }

        private void LandExit()
        {
            //transform.DOShakePosition(2f, Vector3.up, 1, 1f).SetLoops(-1);
            landPlatform.ResetPos(); 
        }

        public void UpdateManager_Update()
        {
        }

        public void UpdateManager_FixedUpdate()
        {
            if (isLandEffect == true)
            {
                collisionEvent.CheckCol();
            }
        }

        public void UpdateManager_LateUpdate()
        {
        }
    }
}
