using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Arena
{
    public class ArenaCtrlTouchTrigger : AbArenaCtrlTrigger
    {
        [SerializeField] private string targetTag = "Player";
        private BoxCollider triggerCollider; 
        [SerializeField]
        private float pushSpeed = 1f; 

        private Collider collider;
        private Vector3 originPos;
        private Vector3 targetPos; 
        
        private Vector3 movePosY => originPos + Vector3.down * height;
        // 충돌 여부 
        [SerializeField]
        private bool isCollision = false;
        [SerializeField]
        private bool isTrigger = false; 
        
        // 프로퍼티 
        private float height => collider.bounds.size.y; 
        protected override void Awake()
        {
            base.Awake();
            collider = GetComponent<Collider>();
            triggerCollider = transform.Find("TriggerCol").GetComponent<BoxCollider>(); 
        }

        private void Start()
        {
            originPos = transform.localPosition;
        }

        private void Update()
        {
            if(isTrigger == true) return;
            Move(); 
            float _height = height / 2; 
            Debug.DrawRay(transform.position, Vector3.up,Color.red);
            //var _hitInfos = Physics.RaycastAll( transform.position, Vector3.up, _height * transform.localScale.y  + 0.5f);
            //Physics.OverlapBox(transform.position + Vector3.up * transform.position.y * 0.5f, new Vector3(),Quaternion.identity);
            var _hitInfos = MyCollisions();
            bool isPlayerHit = false;
            for(int i =0;i < _hitInfos.Length; i++)
            {
                if (_hitInfos[i].transform.CompareTag(targetTag) )
                {
                    isPlayerHit = true;
                    if (isCollision == false)
                    {
                        isCollision = true;
                        targetPos = movePosY; 

                    }
                }
                if (_hitInfos[i].transform.CompareTag(targetTag) && isCollision == true)
                {   
                    // 목표 도달시 
                    if ( Mathf.Abs(transform.localPosition.y - movePosY.y) < 0.1f)
                    {
                        isCollision = false;
                         isTrigger = true; 
                        Interact();
                    }
                }

            }
            // CollisionExit
            // 충돌 했다가 떨어졌으면 
            if(isCollision == true && (isPlayerHit == false || _hitInfos.Length == 0))
            {
                isCollision = false;
                targetPos = originPos; 
            }

        }

        private Collider[] MyCollisions () {
            Collider [] hitColliders = Physics.OverlapBox (
                triggerCollider.center + transform.position,
                triggerCollider.size / 2, 
                Quaternion.identity
                );

            int i = 0;
            while (i < hitColliders.Length) {
                Debug.Log ("Hit : " + hitColliders [i].name);
                i++;
            }

            return hitColliders; 
        }

        private void OnDrawGizmos () {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube (
                triggerCollider.center + transform.position, 
                triggerCollider.size);
        }
        
        [ContextMenu("테스트")]
        public void Test()
        {
            Interact();
        }
        private void Move()
        {
            if ( Mathf.Abs(transform.localPosition.y - targetPos.y) < 0.1f ) return; 
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.fixedDeltaTime * pushSpeed);
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

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("@@@@@@@@@@@@Trigger Col");
            if (collision.transform.CompareTag(targetTag))
            {
                Interact(); 
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Debug.Log("@@@@@@@@@@@@Trigger Col");
            // CollisionEnter
            // 충돌 안했던 상태에서 플레이어와 충돌시
            if (hit.transform.CompareTag(targetTag) && isCollision == false)
            {
                isCollision = true; 
                Interact();
            }    
            // CollisionStay
            else if (hit.transform.CompareTag(targetTag) && isCollision == true)
            {
                
            }
            // CollisionExit
            // 충돌 했다가 떨어졌으면 
            else if(isCollision == true && hit.transform.CompareTag(targetTag) == false)
            {
                isCollision = false; 
            }
        }
    }
    
}
