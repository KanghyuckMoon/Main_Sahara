using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Arena
{
    public class ArenaCtrlTouchTrigger : AbArenaCtrlTrigger
    {
        [SerializeField, Header("움직일 물체")] private Transform targetTrm; 
        [SerializeField] private string targetTag = "Player";
        private BoxRaycaster boxRaycaster; 
        [SerializeField]
        private float pushSpeed = 1f; 

        private Collider collider;
        [SerializeField]
        private Vector3 originPos;
        [SerializeField]
        private Vector3 targetPos;
         [SerializeField] private float height; 

        private Vector3 movePosY => originPos + Vector3.down * Height;
        // 충돌 여부 
        [SerializeField]
        private bool isCollision = false;
        [SerializeField]
        private bool isTrigger = false;

        private Rigidbody rigid; 
        // 프로퍼티 
        private float Height => height;

        [ContextMenu("리셋")]
        public void TestReset()
        {
            isCollision = false;
            isTrigger = false;
            targetPos = originPos; 
        }
        protected override void Awake()
        {
            base.Awake();
            if (targetTrm == null) targetTrm = transform;  
            collider = targetTrm.GetComponent<Collider>();
            boxRaycaster = new BoxRaycaster(transform);
            rigid = GetComponent<Rigidbody>(); 
            height = collider.bounds.size.y /2 ;
        }

        private void Start()
        {
            StartCoroutine(InitOriginPos()); 
            //originPos = targetTrm.localPosition;
            //targetPos = originPos; 
        }

        private IEnumerator InitOriginPos()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f); 
                Debug.Log("RIgid Velocity" + rigid.velocity);
                if (rigid.velocity.sqrMagnitude > 0) yield return null;
                
                originPos = targetTrm.localPosition;
                targetPos = originPos; 
                yield break; 
            }
        }
        private void Update()
        {
            if(isTrigger) return;
            Move(); 
            float _height = Height / 2; 
            Debug.DrawRay(targetTrm.position, Vector3.up,Color.red);
            //var _hitInfos = boxRaycaster.MyCollisions();
            if (isCollision)
            {   
                // 목표 도달시 
                if (Mathf.Abs(targetTrm.localPosition.y - movePosY.y) < 0.1f)
                {
                    isCollision = false;
                    isTrigger = true; 
                    Debug.Log("OnArena");
                    Interact();
                }
            }
        }
        
        
        private void OnDrawGizmos () {
            boxRaycaster?.OnDrawGizmos();
        }
        

        private void Move()
        {
            if ( Mathf.Abs(targetTrm.localPosition.y - targetPos.y) < 0.1f ) return; 
            targetTrm.localPosition = Vector3.Lerp(targetTrm.localPosition, targetPos, Time.deltaTime * pushSpeed);
        }

        public void InitTrigger()
        {
            targetPos = originPos;
            isCollision = false;
            isTrigger = false; 
            // targetPos를 원점으로 설정 
            // Collision false 
            // isTrigger false 
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("충돌1");
            if (other.transform.CompareTag(targetTag) )
            {
                Debug.Log("충돌2");
                if (!isCollision)
                {
                    Debug.Log("충돌3");
                    isCollision = true;
                    targetPos = movePosY; 

                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // CollisionExit
            // 충돌 했다가 떨어졌으면 
            if(isCollision)
            {
                isCollision = false;
                targetPos = originPos; 
            }
        }
    }
    
}
