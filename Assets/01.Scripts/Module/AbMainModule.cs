using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FischlWorks;
using UpdateManager;

namespace Module
{
    public abstract class AbMainModule : MonoBehaviour, IUpdateObj
    {
        //���� ��⿡�� ���� ������Ʈ�� ������ �ִ� ��� ����� ������ �;���
        //�װ� Ǯ�� �Ҷ� ������ ����� �ؿ� ���յ����Ϳ� �־��ִ� �������� �Ѵ�. ���θ�⿡ �־��ش�.

        //Dictionary<ModuleType, BaseModule> modules = new Dictionary<ModuleType, BaseModule>();

        [Header("������")] public int StopOrNot;

        [Header("ĳ���� ��Ʈ�ѷ�")] public CharacterController characterController;
        [Header("ĳ���Ͱ� �� ����")] public Vector2 objDir;
        [Header("ī�޶��� ȸ��")] public Quaternion objRotation;

        [Space]
        [Header("������ ������ ��")] public string dataSOPath;

        [Space]
        [Header("�ǰ� �����̸�")] public string[] hitCollider;
        [Header("���� ĳ���� �ӵ�")] public float moveSpeed;
        [Header("�޸�����?")] public bool isSprint;

        [Space]
        [Header("�����ΰ�?")] public bool isGround;
        [Header("�����߳�?")] public bool isJump;
        [Header("��������?")] public bool isFreeFall;
        [Header("�������")] public bool isSlope;
        [Header("�������۸�Ÿ��")] public bool isJumpBuf;
        [Header("�����ϼ� �ֳ�?")] public bool canMove;
        [Header("�׾���?")] public bool isDead;
        [Header("�������ΰ�?")] public bool isAttack;
        [Header("���⸦ ������ �ֳ�?")] public bool isWeaponExist;
        [Header("�¾ҳ�?")] public bool isHit;

        [Space]
        [Header("�����ϳ�?")] public bool attacking;
        [Header("�������ϳ�?")] public bool strongAttacking;

        [Space]
        [Header("�߷�")] public float gravity;
        [Header("�߷�ũ��")] public float gravityScale = -9.8f;
        [Header("��üũ ��Ÿ�")] public float groundOffset;

        [Space]
        [Header("����")] public bool LockOn;
        //[Header("�� ���� ����")] public csHomebrewIK footRotate;

        [Space]
        [Header("������ �� �ִ� ��ü")] public LayerMask groundLayer;

        [Header("�ִ����")] public float maxSlope;
        
        [Header("����ĳ��Ʈ ����ġ")]
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

		private void OnEnable()
		{
            UpdateManager.UpdateManager.Add(this);
		}

		private void OnDisable()
		{

            UpdateManager.UpdateManager.Remove(this);
        }

		void IUpdateObj.UpdateManager_Update()
        {
            foreach (AbBaseModule baseModule in moduleComponentsDic.Values)
            {
                baseModule?.Update();
            }
        }

        void IUpdateObj.UpdateManager_FixedUpdate()
        {
            foreach (AbBaseModule baseModule in moduleComponentsDic.Values)
            {
                baseModule?.FixedUpdate();
            }
        }

        void IUpdateObj.UpdateManager_LateUpdate()
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