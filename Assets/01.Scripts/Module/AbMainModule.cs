using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FischlWorks;

namespace Module
{
    public abstract class AbMainModule : MonoBehaviour
    {
        //메인 모듈에서 지금 오브젝트가 가지고 있는 모든 모듈을 가지고 와야해
        //그건 풀링 할때 각각의 모듈을 밑에 복합데이터에 넣어주는 형식으로 한다. 메인모듈에 넣어준다.

        //Dictionary<ModuleType, BaseModule> modules = new Dictionary<ModuleType, BaseModule>();

        [Header("멈출까말까")] public int StopOrNot;

        [Header("캐릭터 컨트롤러")] public CharacterController characterController;
        [Header("캐릭터가 갈 방향")] public Vector2 objDir;
        [Header("카메라의 회전")] public Quaternion objRotation;

        [Space]
        [Header("데이터 가져올 데")] public string dataSOPath;

        [Space]
        [Header("피격 판정이름")] public string[] hitCollider;
        [Header("현재 캐릭터 속도")] public float moveSpeed;
        [Header("달리기중?")] public bool isSprint;

        [Space]
        [Header("공중인가?")] public bool isGround;
        [Header("점프했나?")] public bool isJump;
        [Header("떨어졌나?")] public bool isFreeFall;
        [Header("경사정도")] public bool isSlope;
        [Header("점프버퍼링타임")] public bool isJumpBuf;
        [Header("움직일수 있나?")] public bool canMove;
        [Header("죽었나?")] public bool isDead;
        [Header("공격중인가?")] public bool isAttack;
        [Header("무기를 가지고 있나?")] public bool isWeaponExist;
        [Header("맞았냐?")] public bool isHit;

        [Space]
        [Header("공격하나?")] public bool attacking;
        [Header("강공격하나?")] public bool strongAttacking;

        [Space]
        [Header("중력")] public float gravity;
        [Header("중력크기")] public float gravityScale = -9.8f;
        [Header("땅체크 사거리")] public float groundOffset;

        [Space]
        [Header("락온")] public bool LockOn;
        //[Header("발 각도 조절")] public csHomebrewIK footRotate;

        [Space]
        [Header("서있을 수 있는 물체")] public LayerMask groundLayer;

        [Header("최대경사면")] public float maxSlope;
        
        [Header("레이캐스트 쏠위치")]
        public Transform raycastTarget;
        public RaycastHit slopeHit;

        protected Dictionary<ModuleType, AbBaseModule> moduleComponentsDic = null;

        private void Start()
        {
            foreach (AbBaseModule baseModule in moduleComponentsDic.Values)
            {
                baseModule?.Start();
            }
        }

        private void Update()
        {
            foreach (AbBaseModule baseModule in moduleComponentsDic.Values)
            {
                baseModule?.Update();
            }
        }

        private void FixedUpdate()
        {
            foreach (AbBaseModule baseModule in moduleComponentsDic.Values)
            {
                baseModule?.FixedUpdate();
            }
        }

        private void LateUpdate()
        {
            foreach (AbBaseModule baseModule in moduleComponentsDic.Values)
            {
                baseModule?.LateUpdate();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            foreach (AbBaseModule baseModule in moduleComponentsDic.Values)
            {
                baseModule?.OnCollisionEnter(other);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            foreach(AbBaseModule baseModule in moduleComponentsDic.Values)
            {
                baseModule?.OnTriggerEnter(other);
            }
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                foreach (AbBaseModule baseModule in moduleComponentsDic.Values)
                {
                    baseModule?.OnDrawGizmos();
                }
            }
        }

        public T GetModuleComponent<T>(ModuleType moduleType) where T : AbBaseModule
        {
            if (moduleComponentsDic.TryGetValue(moduleType, out var module))
            {
                return module as T;
            }

            return null;
        }

        protected void AddModule(ModuleType moduleType, AbBaseModule baseModule)
        {
            moduleComponentsDic.Add(moduleType, baseModule);
        }
    }
}