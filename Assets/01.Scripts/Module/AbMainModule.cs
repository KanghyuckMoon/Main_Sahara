using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FischlWorks;
using UpdateManager;
using Utill.Pattern;
using Data;
using Pool;
using TimeManager;

namespace Module
{
    public abstract class AbMainModule : MonoBehaviour, IUpdateObj, Obserble
    {
        //���� ��⿡�� ���� ������Ʈ�� ������ �ִ� ��� ����� ������ �;���
        //�װ� Ǯ�� �Ҷ� ������ ����� �ؿ� ���յ����Ϳ� �־��ִ� �������� �Ѵ�. ���θ�⿡ �־��ش�.

        #region ������Ƽ
        public float StopOrNot
        {
            get
            {
                return stopOrNot;
            }
            set
            {
                stopOrNot = value;
            }
        }
        public Transform LockOnTarget
        {
            get
            {
                return lockOnTarget;
            }
            set
            {
                lockOnTarget = value;
            }
        }
        public CharacterController CharacterController
        {
            get
            {
                return characterController;
            }
            set
            {
                characterController = value;
            }
        }
        public Vector2 ObjDir
        {
            get
            {
                return objDir;
            }
            set
            {
                objDir = value;
            }
        }
        public Quaternion ObjRotation
        {
            get
            {
                return objRotation;
            }
            set
            {
                objRotation = value;
            }
        }
        public Vector3 ObjForword
        {
            get
            {
                return objForword;
            }
            set
            {
                objForword = value;
            }
        }
        public StatData StatData
        {
            get
            {
                statData ??= GetComponent<StatData>();
                return statData;
            }
        }
        public float MoveSpeed
        {
            get
            {
                return moveSpeed;

            }
        }
        public string[] HitCollider
        {
            get
            {
                return hitCollider;

            }
        }
        public bool IsSprint
        {
            get
            {
                return isSprint;

            }
            set
            {
                isSprint = value;
            }
        }
        public bool IsJump
        {
            get
            {
                return isJump;
            }
            set
            {
                isJump = value;
            }
        }
        public bool IsFreeFall
        {
            get
            {
                return isFreeFall;
            }
        }
        public bool IsSlope
        {
            get
            {
                return isSlope;
            }
            set
            {
                isSlope = value;
            }
        }
        public bool IsJumpBuf
        {
            get
            {
                return isJumpBuf;
            }
            set
            {
                isJumpBuf = value;
            }
        }
        public bool CanMove
        {
            get
            {
                return canMove;
            }
            set
            {
                canMove = value;
            }
        }
        public bool IsDead
        {
            get
            {
                return isDead;
            }
            set
            {
                isDead = value;
                Send();
            }
        }
        public bool IsAttack
        {
            get
            {
                return isAttack;
            }
        }
        public bool IsWeaponExist
        {
            get
            {
                return isWeaponExist;
            }
            set
            {
                isWeaponExist = value;
            }
        }
        public bool IsHit
        {
            get
            {
                return isHit;
            }
            set
            {
                isHit = value;
            }
        }
        public bool IsCharging
        {
            get
            {
                return isCharging;
            }
            set
            {
                isCharging = value;
            }
        }
        public bool Attacking
        {
            get
            {
                return attacking;
            }
            set
            {
                attacking = value;
                Animator.SetBool("Attack", attacking);
            }
        }
        public bool StrongAttacking
        {
            get
            {
                return strongAttacking;
            }
            set
            {
                strongAttacking = value;
                Animator.SetBool("StrongAttack", strongAttacking);
            }
        }
        public float Gravity
        {
            get
            {
                return gravity;
            }
            set
            {
                gravity = value;
            }
        }
        public bool IsCanHit
        {
            get
            {
                return isCanHit;
            }
            set
            {
                isCanHit = value;
            }
        }
        public bool IsStaticTime
        {
            get
            {
                return isStaticTime;
            }
            set
            {
                isStaticTime = value;
            }
        }
        public float GravityScale
        {
            get
            {
                return gravityScale;
            }
        }
        public float EntireTime
        {
            get
            {
                return IsStaticTime ? StaticTime.EntierTime : PersonalTime;
            }
        }
        public float PersonalTime
        {
            get
            {
                return player ? StaticTime.PlayerTime : StaticTime.EnemyTime;
            }
            set 
            {
                if(player == true)
                {
                    StaticTime.PlayerTime = value; 
                }
                else
                {
                    StaticTime.EnemyTime = value;
                }

            }
        }
        public Vector3 KnockBackVector
        {
            get
            {
                return knockBackVector;
            }
            set
            {
                knockBackVector = value;
            }
        }
        public Vector3 SlopeVector
        {
            get
            {
                return slopeVector;
            }
            set
            {
                slopeVector = value;
            }
        }
        public bool LockOn
        {
            get
            {
                return lockOn;
            }
            set
            {
                lockOn = value;
                Animator.SetBool("LockOn", lockOn);
            }
        }
        public float MaxSlope
		{
            get
			{
                return maxSlope;
			}
		}
        public Transform RaycastTarget
		{
            get
			{
                return raycastTarget;
			}
            set
			{
                raycastTarget = value;
			}
		}
        public GameObject VisualObject
        {
            get
            {
                visualObject ??= transform.Find("Visual").gameObject;
                return visualObject;
            }
        }
        public RaycastHit SlopeHit
		{
			get
			{
				return slopeHit;
			}
            set
			{
                slopeHit = value;
			}
		}
        public Transform Model
		{
            get
			{
                return model;
			}
		}
        public AnimatorOverrideController AnimatorOverrideController
        {
            get
            {
                animatorOverrideController ??= new AnimatorOverrideController(Animator.runtimeAnimatorController);
                Animator.runtimeAnimatorController = animatorOverrideController;
                return animatorOverrideController;
            }
        }
        public Animator Animator
		{
            get
			{
                animator ??= GetComponent<Animator>();
                return animator;
			}
            set
			{
                animator = value;
			}
		}
        public GameObject Back
        {
            get
            {
                return back;
            }
        }
        #endregion

        #region ����
        public bool player;

        private float stopOrNot;

        [SerializeField, Header("(�Ͽ�)Ÿ��")]
        private Transform lockOnTarget = null;

        [SerializeField, Header("ĳ���� ��Ʈ�ѷ�")] 
        private CharacterController characterController;
        [SerializeField, Header("ĳ���Ͱ� �� ����")] 
        private Vector2 objDir;
        [SerializeField, Header("ī�޶��� ȸ��")] 
        private Quaternion objRotation;
        [SerializeField, Header("ī�޶� �չ���")]
        private Vector3 objForword;

        [Space]
        [SerializeField, Header("������ ������ ��")]
        private StatData statData;

        [Space]
        [SerializeField, Header("�ǰ� �����̸�")] 
        private string[] hitCollider;
        [SerializeField, Header("���� ĳ���� �ӵ�")] 
        private float moveSpeed;
        [SerializeField, Header("�޸�����?")] 
        private bool isSprint;

        //���� ������ ����ȭ�� ���� Public�� ���
        [Space]
        [SerializeField, Header("�����ΰ�?")] 
        public bool isGround; 
        [SerializeField, Header("�����߳�?")] 
        private bool isJump;
        [SerializeField, Header("��������?")] 
        private bool isFreeFall;
        [SerializeField, Header("�������")]
        private bool isSlope;
        [SerializeField, Header("�������۸�Ÿ��")] 
        private bool isJumpBuf;
        [SerializeField, Header("�����ϼ� �ֳ�?")] 
        private bool canMove;
        [SerializeField, Header("�׾���?")] 
        private bool isDead;
        [SerializeField, Header("�������ΰ�?")] 
        private bool isAttack;
        [SerializeField, Header("���⸦ ������ �ֳ�?")] 
        private bool isWeaponExist;
        [SerializeField, Header("�¾ҳ�?")] 
        private bool isHit;
        [SerializeField, Header("��¡���ΰ�?")]
        private bool isCharging;
        [SerializeField, Header("���� �� �ֳ�?")]
        private bool isCanHit;
        [SerializeField, Header("��ü �ð��ΰ�?")]
        private bool isStaticTime;

        [Space]
        [SerializeField, Header("�����ϳ�?")] 
        private bool attacking;
        [SerializeField, Header("�������ϳ�?")] 
        private bool strongAttacking;

        [Space]
        [SerializeField, Header("�߷�")] 
        private float gravity;
        [SerializeField, Header("�߷�ũ��")] 
        private float gravityScale = -9.8f;
        [SerializeField, Header("��üũ ��Ÿ�")] 
        public float groundOffset;
        [SerializeField, Header("�˹�")]
        private Vector3 knockBackVector;
        [SerializeField, Header("�ٴ� ���")]
        private Vector3 slopeVector;

        [Space]
        public float hitDelay;

        [Space]
        [SerializeField, Header("����")] 
        private bool lockOn;

        [Space]
        [SerializeField, Header("������ �� �ִ� ��ü")] 
        public LayerMask groundLayer;

        [SerializeField, Header("�ִ����")] 
        private float maxSlope;
        
        [SerializeField, Header("����ĳ��Ʈ ����ġ")]
        private Transform raycastTarget;
        private RaycastHit slopeHit;

        [SerializeField, Header("���־�")]
        public GameObject visualObject;

        [SerializeField]
        private Transform model;

        [SerializeField, Header("�ִϸ�����")]
        protected Animator animator;

        private float personalTime;

        [Space]
        [SerializeField, Header("���� ��")]
        public Transform leftFeet;
        public Transform rightFeet;

        [Space]
        [SerializeField, Header("�㸮")]
        private GameObject back;

        [Space]
        public AnimatorOverrideController animatorOverrideController;
        #endregion

        protected Dictionary<ModuleType, AbBaseModule> moduleComponentsDic = null;

		public List<Observer> Observers
		{
            get
			{
                return observers;
			}
		}
        private List<Observer> observers = new List<Observer>();

		private void Start()
        {
            foreach (AbBaseModule baseModule in moduleComponentsDic.Values)
            {
                baseModule?.Start();
            }
        }

        public void OnEnable()
        {
            foreach (AbBaseModule baseModule in moduleComponentsDic.Values)
            {
                //baseModule.Init(this);
                baseModule?.Awake();
                baseModule?.OnEnable();
            }
            UpdateManager.UpdateManager.Add(this);
		}

        public void OnDisable()
        {
            foreach (AbBaseModule baseModule in moduleComponentsDic.Values)
            {
                baseModule?.OnDisable();
            }
            UpdateManager.UpdateManager.Remove(this);
            moduleComponentsDic.Clear();
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

        private void OnDestroy()
        {
            foreach (AbBaseModule baseModule in moduleComponentsDic.Values)
            {
                baseModule?.OnDestroy();
            }
            moduleComponentsDic.Clear();
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
        protected void AddModuleWithPool<T>(ModuleType _moduleType, string _moduleAddress, params string[] _parameters) where T : AbBaseModule, new()
        {
            T _module = ClassPoolManager.Instance.GetClass<T>(_moduleAddress);
            if (_module is null)
            {
                _module = new T();
            }
            _module.Init(this, _parameters);
            moduleComponentsDic.Add(_moduleType, _module);
        }

        public void AddObserver(Observer _observer)
		{
            Observers.Add(_observer);
            _observer.Receive();
		}

        public void RemoveObserver(Observer _observer)
        {
            Observers.Remove(_observer);
        }

        public void Send()
        {
            foreach (Observer observer in Observers)
            {
                observer.Receive();
            }
        }
    }
}