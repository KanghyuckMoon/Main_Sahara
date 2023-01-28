using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FischlWorks;

namespace Module
{
    public class MainModule : MonoBehaviour
    {
        //메인 모듈에서 지금 오브젝트가 가지고 있는 모든 모듈을 가지고 와야해
        //그건 풀링 할때 각각의 모듈을 밑에 복합데이터에 넣어주는 형식으로 한다. 메인모듈에 넣어준다.

        //Dictionary<ModuleType, BaseModule> modules = new Dictionary<ModuleType, BaseModule>();

        [Header("캐릭터 컨트롤러")] public Rigidbody rigidBody;
        [Header("캐릭터가 갈 방향")] public Vector2 objDir;
        [Header("카메라의 회전")] public Quaternion objRotation;

        [Header("피격 판정이름")] public string[] hitCollider;
        [Header("현재 캐릭터 속도")] public float moveSpeed;
        [Header("달리기중?")] public bool isSprint;

        [Space]
        [Header("공중인가?")] public bool isGround;
        [Header("점프했나?")] public bool isJump;
        [Header("떨어졌나?")] public bool isFreeFall;
        [Header("경사정도")] public bool isSlope;

        [Space]
        [Header("중력")] public float gravity;
        [Header("중력크기")] public float gravityScale = -9.8f;
        [Header("땅체크 사거리")] public float groundOffset;

        [Space]
        [Header("락온")] public bool LockOn;
        [Header("발 각도 조절")] public csHomebrewIK footRotate;

        [Space]
        [Header("서있을 수 있는 물체")] public LayerMask groundLayer;

        [Header("최대경사면")] public float maxSlope;

        [Header("레이캐스트 쏠위치")]
        public Transform raycastTarget;
        public RaycastHit slopeHit;

        private Dictionary<ModuleType, AbBaseModule> moduleComponentsDic = null;

        private void Awake()
        {
            moduleComponentsDic = new();
            rigidBody = GetComponentInParent<Rigidbody>();
            footRotate = GetComponentInParent<csHomebrewIK>();
            AddModule(ModuleType.Input, new InputModule(this));
            AddModule(ModuleType.Move, new MoveModule(this));
            AddModule(ModuleType.State, new StateModule(this));
            AddModule(ModuleType.Camera, new CameraModule(this));
            AddModule(ModuleType.Jump, new JumpModule(this));
            AddModule(ModuleType.Hp, new HpModule(this));
            AddModule(ModuleType.Animation, new AnimationModule(this));
            AddModule(ModuleType.Pysics, new PysicsModule(this));
            AddModule(ModuleType.UI, new UIModule(this));

            raycastTarget = transform.parent.Find("RayCastPoint");
        }

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