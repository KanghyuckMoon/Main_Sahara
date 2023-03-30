using UnityEngine;
using UnityEngine.AI;
using AI;
using DynamicBGM;
using Utill.Pattern;
using Pool;
using static NodeUtill;
using Cinemachine;

namespace Module
{
	public interface IEnemy
	{
		public string AIAddress
		{
			get;
		}

		public CinemachineSmoothPath SmoothPath
		{
			get;
			set;
		}
	}

	public partial class AIModule : AbBaseModule
	{
		public enum AIState
		{
			Idle,
			Run,
			Walk,
			Attack,
			Jump,
		}

		public enum AIHostileState
		{
			Unknow, //발견 안 함
			Suspicion, //의심
			Discovery, //발견
			Investigate //놓친 플레이어를 찾는중
		}

		public Transform Player
		{
			get
			{
				player ??= GameObject.FindGameObjectWithTag("Player")?.transform;
				return player;
			}
		}

		public AIState AIModuleState
		{
			get
			{
				return aiState;
			}
			set
			{
				aiState = value;
			}
		}
		public Vector2 Input
		{
			get
			{
				return input;
			}
			set
			{
				input = value;
			}
		}
		public AIHostileState AIModuleHostileState
		{
			get
			{
				return currentAIHostileState;
			}
			set
			{
				previousAIHostileState = currentAIHostileState;
				currentAIHostileState = value;

				if (previousAIHostileState is AIHostileState.Discovery)
				{
					if (currentAIHostileState is AIHostileState.Unknow || currentAIHostileState is AIHostileState.Suspicion || currentAIHostileState is AIHostileState.Investigate)
					{
						DynamicBGMManager.Instance.RemoveEnemyCount();
						UIModule.IsRender = false;
					}
				}
				else if (previousAIHostileState is not AIHostileState.Discovery)
				{
					if (currentAIHostileState is AIHostileState.Discovery)
					{
						DynamicBGMManager.Instance.AddEnemyCount();
						UIModule.IsRender = true;
					}
				}
			}
		}

		public float SuspicionGauge
		{
			get
			{
				return suspicionGauge;
			}
			set
			{
				suspicionGauge = value;
			}
		}
		public UIModule UIModule
		{
			get
			{
				uiModule ??= mainModule.GetModuleComponent<UIModule>(ModuleType.UI);
				return uiModule;
			}
		}

		public bool IsFirstAttack
		{
			get
			{
				return isFirstAttack;
			}
			set
			{
				isFirstAttack = value;
			}
		}

		public bool IsHostilities
		{
			get
			{
				return isHostilities;
			}
			set
			{
				isHostilities = value;
			}
		}
		public CinemachineSmoothPath SmoothPath
		{
			get
			{
				return smoothPath;
			}
			set
			{
				smoothPath = value;
			}
		}

		public Vector3 OriginPos
		{
			get
			{
				return originPos;
			}
			set
			{
				originPos = value;
			}
		}
		
		public Vector3 LastFindPlayerPos
		{
			get
			{
				return lastFindPlayerPos;
			}
			set
			{
				lastFindPlayerPos = value;
			}
		}

		public bool IsInit
		{
			get
			{
				return isInit;
			}
			set
			{
				isInit = value;
			}
		}
		
		private Transform player;
		protected INode _rootNode;
		private bool isInit = false;
		private Vector2 input = Vector3.zero;
		private AIState aiState = AIState.Idle;
		private RootNodeMaker rootNodeMaker;
		private AIHostileState currentAIHostileState;
		private AIHostileState previousAIHostileState;
		private UIModule uiModule;
		private float suspicionGauge = 0f;
		private bool isFirstAttack = true;
		private bool isHostilities = true;
		private CinemachineSmoothPath smoothPath;
		private Vector3 originPos = Vector3.zero;
		private Vector3 lastFindPlayerPos = Vector3.zero;

		public AIModule() : base()
		{

		}

		public AIModule(AbMainModule _mainModule) : base(_mainModule)
		{
		}

		public override void Init(AbMainModule _mainModule, params string[] _parameters)
		{
			base.Init(_mainModule, _parameters);
			if ((_mainModule is IEnemy))
			{
				if ((_mainModule as IEnemy).SmoothPath is not null)
				{
					smoothPath = (_mainModule as IEnemy).SmoothPath;
				}
				originPos = _mainModule.transform.position;
				rootNodeMaker ??= new RootNodeMaker(this, (_mainModule as IEnemy).AIAddress);
				rootNodeMaker.isSetAISO = false;
				rootNodeMaker.Init((_mainModule as IEnemy).AIAddress);
			}
		}

		public override void Start()
		{
			base.Start();
			Init(mainModule);
		}

		public void SetNode(INode _node)
		{
			_rootNode = _node;
		}

		public override void Update()
		{
			if (Player is null)
			{
				return;
			}

			if (rootNodeMaker == null)
			{
				return;
			}
			
			if (!rootNodeMaker.isSetAISO)
            {
				return;
            }

			if (!isInit)
			{
				rootNodeMaker.StartWeaponSet();
				isInit = true;
			}

			if (mainModule.IsDead)
			{
				rootNodeMaker.Dead();
				return;
			}

			_rootNode.Run();
		}

		public override void OnDrawGizmos()
		{
			rootNodeMaker.OnDrawGizmo();
		}

		public override void OnDisable()
		{
			isInit = false;
			rootNodeMaker = null;
			smoothPath = null;
			uiModule = null;
			player = null;
			_rootNode = null;
			base.OnDisable();
			ClassPoolManager.Instance.RegisterObject<AIModule>("AIModule", this);
		}

		public override void OnDestroy()
		{
			isInit = false;
			rootNodeMaker = null;
			smoothPath = null;
			uiModule = null;
			player = null;
			_rootNode = null;
			base.OnDestroy();
			ClassPoolManager.Instance.RegisterObject<AIModule>("AIModule", this);
		}

	}
}