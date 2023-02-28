using UnityEngine;
using UnityEngine.AI;
using AI;
using DynamicBGM;
using Utill.Pattern;
using Pool;
using static NodeUtill;

namespace Module
{
	public interface IEnemy
	{
		public string AIAddress
		{
			get;
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
			Investigate //놓친 플레이어를 찾음
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

		public AIModule() : base()
		{

		}

		public AIModule(AbMainModule _mainModule) : base(_mainModule)
		{
			Init(_mainModule);
		}

		public override void Init(AbMainModule _mainModule, params string[] _parameters)
		{
			base.Init(_mainModule, _parameters);
			rootNodeMaker ??= new RootNodeMaker(this, (_mainModule as IEnemy).AIAddress);
			rootNodeMaker.Init((_mainModule as IEnemy).AIAddress);
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

			if (!isInit)
			{
				isInit = true;
				WeaponModule _weaponModule = mainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon);
				_weaponModule.ChangeWeapon("EndSword", "Sword");
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
			base.OnDisable();
			ClassPoolManager.Instance.RegisterObject<AIModule>("AIModule", this);
		}

	}
}