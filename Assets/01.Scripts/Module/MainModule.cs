using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FischlWorks;

namespace Module
{
    public class MainModule : MonoBehaviour
    {
        //���� ��⿡�� ���� ������Ʈ�� ������ �ִ� ��� ����� ������ �;���
        //�װ� Ǯ�� �Ҷ� ������ ����� �ؿ� ���յ����Ϳ� �־��ִ� �������� �Ѵ�. ���θ�⿡ �־��ش�.

        //Dictionary<ModuleType, BaseModule> modules = new Dictionary<ModuleType, BaseModule>();

        [Header("ĳ���� ��Ʈ�ѷ�")] public Rigidbody rigidBody;
        [Header("ĳ���Ͱ� �� ����")] public Vector2 objDir;
        [Header("ī�޶��� ȸ��")] public Quaternion objRotation;

        [Header("�ǰ� �����̸�")] public string[] hitCollider;
        [Header("���� ĳ���� �ӵ�")] public float moveSpeed;
        [Header("�޸�����?")] public bool isSprint;

        [Space]
        [Header("�����ΰ�?")] public bool isGround;
        [Header("�����߳�?")] public bool isJump;
        [Header("��������?")] public bool isFreeFall;
        [Header("�������")] public bool isSlope;

        [Space]
        [Header("�߷�")] public float gravity;
        [Header("�߷�ũ��")] public float gravityScale = -9.8f;
        [Header("��üũ ��Ÿ�")] public float groundOffset;

        [Space]
        [Header("����")] public bool LockOn;
        [Header("�� ���� ����")] public csHomebrewIK footRotate;

        [Space]
        [Header("������ �� �ִ� ��ü")] public LayerMask groundLayer;

        [Header("�ִ����")] public float maxSlope;

        [Header("����ĳ��Ʈ ����ġ")]
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