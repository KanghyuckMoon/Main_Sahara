using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FischlWorks;
using UpdateManager;
using Utill.Pattern;
using Data;

namespace Module
{
    public abstract class AbMainModule : MonoBehaviour, IUpdateObj, Obserble
    {
        //메인 모듈에서 지금 오브젝트가 가지고 있는 모든 모듈을 가지고 와야해
        //그건 풀링 할때 각각의 모듈을 밑에 복합데이터에 넣어주는 형식으로 한다. 메인모듈에 넣어준다.

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
        public StatData StatData
        {
            get
            {
                statData ??= GetComponent<StatData>();
                return statData;
            }
        }

        public string[] HitCollider
        {
            get
            {
                return hitCollider;

            }
        }
        public float MoveSpeed
        {
            get
            {
                return moveSpeed;

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

        public bool Attacking
        {
            get
            {
                return attacking;
            }
            set
			{
                attacking = value;
                animator.SetBool("Attack", attacking);
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
                animator.SetBool("StrongAttack", strongAttacking);
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
        public float GravityScale
		{
            get
			{
                return gravityScale;
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

        public bool LockOn
        {
            get
            {
                return lockOn;
            }
            set
            {
                lockOn = value;
                animator.SetBool("LockOn", lockOn);
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
                animatorOverrideController ??= new AnimatorOverrideController(animator.runtimeAnimatorController);
                return animatorOverrideController;
            }
        }

		[SerializeField, Header("멈출까말까")] 
        private float stopOrNot;

        [SerializeField, Header("(록온)타겟")]
        private Transform lockOnTarget = null;

        [SerializeField, Header("캐릭터 컨트롤러")] 
        private CharacterController characterController;
        [SerializeField, Header("캐릭터가 갈 방향")] 
        private Vector2 objDir;
        [SerializeField, Header("카메라의 회전")] 
        private Quaternion objRotation;

        [Space]
        [SerializeField, Header("데이터 가져올 데")]
        private StatData statData;

        [Space]
        [SerializeField, Header("피격 판정이름")] 
        private string[] hitCollider;
        [SerializeField, Header("현재 캐릭터 속도")] 
        private float moveSpeed;
        [SerializeField, Header("달리기중?")] 
        private bool isSprint;

        //물리 관련은 최적화를 위해 Public을 사용
        [Space]
        [SerializeField, Header("공중인가?")] 
        public bool isGround; 
        [SerializeField, Header("점프했나?")] 
        private bool isJump;
        [SerializeField, Header("떨어졌나?")] 
        private bool isFreeFall;
        [SerializeField, Header("경사정도")] 
        private bool isSlope;
        [SerializeField, Header("점프버퍼링타임")] 
        private bool isJumpBuf;
        [SerializeField, Header("움직일수 있나?")] 
        private bool canMove;
        [SerializeField, Header("죽었나?")] 
        private bool isDead;
        [SerializeField, Header("공격중인가?")] 
        private bool isAttack;
        [SerializeField, Header("무기를 가지고 있나?")] 
        private bool isWeaponExist;
        [SerializeField, Header("맞았냐?")] 
        private bool isHit;

        [Space]
        [SerializeField, Header("공격하나?")] 
        private bool attacking;
        [SerializeField, Header("강공격하나?")] 
        private bool strongAttacking;

        [Space]
        [SerializeField, Header("중력")] 
        private float gravity;
        [SerializeField, Header("중력크기")] 
        private float gravityScale = -9.8f;
        [SerializeField, Header("땅체크 사거리")] 
        public float groundOffset;
        [SerializeField, Header("넉백")]
        private Vector3 knockBackVector;

        [Space]
        public float hitDelay;

        [Space]
        [SerializeField, Header("락온")] 
        private bool lockOn;

        [Space]
        [SerializeField, Header("서있을 수 있는 물체")] 
        public LayerMask groundLayer;

        [SerializeField, Header("최대경사면")] 
        private float maxSlope;
        
        [SerializeField, Header("레이캐스트 쏠위치")]
        private Transform raycastTarget;
        private RaycastHit slopeHit;

        [SerializeField, Header("비주얼")]
        public GameObject visualObject;

        [SerializeField]
        private Transform model;

        [SerializeField, Header("애니메이터")]
        public Animator animator;

        private AnimatorOverrideController animatorOverrideController;

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

        private void OnDestroy()
        {
            foreach (AbBaseModule baseModule in moduleComponentsDic.Values)
            {
                baseModule?.OnDestroy();
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