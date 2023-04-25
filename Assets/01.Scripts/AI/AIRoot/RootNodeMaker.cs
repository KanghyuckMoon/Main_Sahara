using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.ResourceManagement;
using Utill.Addressable;
using Utill.Pattern;
using Module;
using static NodeUtill;

namespace AI
{
	public partial class RootNodeMaker
	{
		private Vector3 Position
		{
			get
			{
				return aiModule.MainModule.transform.position;
			}
		}

		private AIModule aiModule;
		private AISO aiSO;

		//컨디션 & 액션용 변수
		Vector3 jumpCheckVector;
		private UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
		private float elapsed = 0.0f;
		private float distance = 0.1f;
		private float jumpDistance = 0.5f;
		private float yDistance = 3f;
		private float rageGauge = 0f;
		public bool isSetAISO = false;

		public RootNodeMaker(AIModule _aiModule, string _address)
		{
			aiModule = _aiModule;
			Init(_address);
		}

		public void Init(string _address)
		{
			AddressablesManager.Instance.GetResourceAsync<AISO>(_address, SetAISO);
		}
		
		public void StartWeaponSet()
		{
			if (!string.IsNullOrEmpty(aiSO.startWeapon))
			{
				aiModule.MainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon).ChangeWeapon(aiSO.startWeapon, null);
			}

			if(!string.IsNullOrEmpty(aiSO.startItemE))
			{
				aiModule.MainModule.GetModuleComponent<SkillModule>(ModuleType.Skill).SetSkill("E", aiSO.startItemE);
			}

			if (!string.IsNullOrEmpty(aiSO.startItemR))
			{
				aiModule.MainModule.GetModuleComponent<SkillModule>(ModuleType.Skill).SetSkill("R", aiSO.startItemR);
			}
		}

		private void SetAISO(AISO _aiSO)
		{
			aiSO = _aiSO;
			aiModule.IsFirstAttack = aiSO.isFirstAttack;
			aiModule.IsHostilities = aiSO.isFirstAttack;
			INode _node = ReturnRootNode();
			if (_node != null)
			{
				aiModule.SetNode(_node);
				isSetAISO = true;
			}
			else
			{
				AddressablesManager.Instance.GetResourceAsync<NodeMakeSO>(aiSO.aiAddress, CallBackNodeMakeSO);
			}
			
		}

		public INode ReturnRootNode()
		{
			switch(aiSO.aiAddress)
			{
				default:
					return null;
				case "TestTalkNPC":
					return TestTalkNPC();
				case "TestEnemy":
					return TestEnemy();
				case "TestWorm":
					return TestWorm();
				case "TestTrack":
					return TestTrack();
				case "NPC":
					return NPC();
				case "Navi":
					return Navi();
			}
		}

		private void CallBackNodeMakeSO(NodeMakeSO _nodeMakeSO)
		{
			aiModule.SetNode( NodeModelToINode(_nodeMakeSO.nodeModel, null));
			isSetAISO = true;
		}

		private partial INode TestEnemy();
		private partial INode TestWorm();
		private partial INode TestTalkNPC();
		private partial INode TestTrack();
		private partial INode NPC();
		private partial INode Navi();

		public void Dead()
		{
			aiModule.AIModuleHostileState = AIModule.AIHostileState.Unknow;
			aiModule.MainModule.ObjDir = Vector3.zero;
			Reset();
		}

		public void OnDrawGizmo()
		{
			if(aiSO is null)
			{
				return;
			}
			Gizmos.color = aiSO.gizmoColor_fer;
			Gizmos.DrawWireSphere(aiModule.MainModule.transform.position, aiSO.ferDistance);

			float lookingAngle = aiModule.MainModule.transform.eulerAngles.y;  //ĳ���Ͱ� �ٶ󺸴� ������ ����
			Vector3 rightDir = AngleToDir(aiModule.MainModule.transform.eulerAngles.y + aiSO.ViewAngle * 0.5f);
			Vector3 leftDir = AngleToDir(aiModule.MainModule.transform.eulerAngles.y - aiSO.ViewAngle * 0.5f);
			Vector3 lookDir = AngleToDir(lookingAngle);

			Gizmos.color = aiSO.gizmoColor_discorver;
			Vector3 drawPos = Position;
			drawPos.y += 1f;
			Gizmos.DrawLine(drawPos, drawPos + (rightDir * aiSO.ViewRadius));
			Gizmos.DrawLine(drawPos, drawPos + (leftDir * aiSO.ViewRadius));
			Gizmos.DrawLine(drawPos, drawPos + (lookDir * aiSO.ViewRadius));
			Gizmos.DrawWireSphere(Position, aiSO.ViewRadius);

			Gizmos.color = aiSO.gizmoColor_fer;
			Vector3 rightSusDir = AngleToDir(aiModule.MainModule.transform.eulerAngles.y + aiSO.SuspicionAngle * 0.5f);
			Vector3 leftSusDir = AngleToDir(aiModule.MainModule.transform.eulerAngles.y - aiSO.SuspicionAngle * 0.5f);
			Gizmos.DrawLine(drawPos, drawPos + (rightSusDir * aiSO.SuspicionAngle));
			Gizmos.DrawLine(drawPos, drawPos + (leftSusDir * aiSO.SuspicionAngle));
			Gizmos.DrawWireSphere(Position, aiSO.SuspicionRadius);

			Gizmos.color = aiSO.gizmoColor_attack;
			Vector3 rightAttackDir = AngleToDir(aiModule.MainModule.transform.eulerAngles.y + aiSO.AttackAngle * 0.5f);
			Vector3 leftAttackDir = AngleToDir(aiModule.MainModule.transform.eulerAngles.y - aiSO.AttackAngle * 0.5f);
			Gizmos.DrawLine(drawPos, drawPos + (rightAttackDir * aiSO.AttackAngle));
			Gizmos.DrawLine(drawPos, drawPos + (leftAttackDir * aiSO.AttackAngle));
			Gizmos.DrawWireSphere(Position, aiSO.AttackRadius);

			Debug.DrawRay(jumpCheckVector, Vector3.down * 1000, Color.yellow);

			for (int i = 0; i < path.corners.Length - 1; i++)
			{
				Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
			}
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(Position, aiSO.AroundRadius);
		}

		private INode NodeModelToINode(NodeModel _nodeModel, INode _parent)
		{
			INode _node = null;
			INode _includeNode = null;
			switch (_nodeModel.nodeType)
			{
				case NodeType.Action:
				case NodeType.PercentAction:
					_includeNode = Action(NodeModelToINodeAction(_nodeModel.nodeAction));
					_node = ConditionCheck(NodeModelToINodeCondition(_nodeModel.nodeCondition), _includeNode, _nodeModel.isIgnore, _nodeModel.isInvert, _nodeModel.isUseTimer, _nodeModel.delay, _nodeModel.isInvertTime);
					break;
				case NodeType.Selector:
					_node = Selector();
					break;
				case NodeType.PercentRandomChoice:
					_node = PercentRandomChoiceNode(_nodeModel.changeDelay);
					break;
				case NodeType.IfSelector:
					_node = IfSelector(NodeModelToINodeCondition(_nodeModel.nodeCondition));
					break;
				case NodeType.StringAction:
					_includeNode = StringAction(NodeModelToINodeStringAction(_nodeModel.nodeAction));
					(_includeNode as StringActionNode).str = _nodeModel.str;
					_node = ConditionCheck(NodeModelToINodeCondition(_nodeModel.nodeCondition), _includeNode, _nodeModel.isIgnore, _nodeModel.isInvert, _nodeModel.isUseTimer, _nodeModel.delay, _nodeModel.isInvertTime);
					break;
				case NodeType.FloatAction:
					_includeNode = FloatAction(NodeModelToINodeFloatAction(_nodeModel.nodeAction));
					(_includeNode as FloatActionNode).value = _nodeModel.value;
					_node = ConditionCheck(NodeModelToINodeCondition(_nodeModel.nodeCondition), _includeNode, _nodeModel.isIgnore, _nodeModel.isInvert, _nodeModel.isUseTimer, _nodeModel.delay, _nodeModel.isInvertTime);
					break;
			}
			if (_parent is not null)
			{
				if (_parent is CompositeNode)
				{
					(_parent as CompositeNode).Add(_node);
				}
				else if (_parent is PercentRandomChoiceNode)
				{
					(_parent as PercentRandomChoiceNode).Add(new Tuple<float, INode>(_nodeModel.percent, _node));
				}
			}
			if (_nodeModel.nodeModelList.Count > 0)
			{
				for (int i = 0; i < _nodeModel.nodeModelList.Count; ++i)
				{
					NodeModelToINode(_nodeModel.nodeModelList[i], _node);	
				}
			}
			return _node;
		}

		private System.Action NodeModelToINodeAction(NodeAction _nodeAction)
		{
			return _nodeAction switch
			{
				NodeAction.CloserMove => CloserMove,
				NodeAction.RunMove => RunMove,
				NodeAction.Jump => Jump,
				NodeAction.Attack => Attack,
				NodeAction.StrongAttack => StrongAttack,
				NodeAction.Reset => Reset,
				NodeAction.Ignore => Ignore,
				NodeAction.Rotate => Rotate,
				NodeAction.DirectRotate => DirectRotate,
				NodeAction.JumpAndRunMove => JumpAndRunMove,
				NodeAction.TargetFind => TargetFind,
				NodeAction.SuspicionGaugeSet => SuspicionGaugeSet,
				NodeAction.MoveReset => MoveReset,
				NodeAction.WalkAwayMove => WalkAwayMove,
				NodeAction.RunAwayMove => RunAwayMove,
				NodeAction.SetMoveDir => SetMoveDir,
				NodeAction.RotateXYZ => RotateXYZ,
				NodeAction.ModelRotateXYZ => ModelRotateXYZ,
				NodeAction.TrackMove => TrackMove,
				NodeAction.AroundOriginPos => AroundOriginPos,
				NodeAction.AroundLastFindPlayerPos => AroundLastFindPlayerPos,
				NodeAction.SkillWeapon => SkillWeapon,
				NodeAction.SkillE => SkillE,
				NodeAction.SkillR => SkillR,
				NodeAction.RageOn => RageOn,
				NodeAction.RageOff => RageOff,
				NodeAction.Nothing => Nothing,
				NodeAction.FixiedMove => FixiedMove,
				NodeAction.LockOnPlayer => LockOnPlayer,
				NodeAction.AroundMove => AroundMove,
				NodeAction.SetAroundOrigin => SetAroundOrigin,
				NodeAction.SetAroundPlayer => SetAroundPlayer,
				NodeAction.InstantDiscovery => InstantDiscovery,
				_ => null
			};
		}
		
		private System.Action<string> NodeModelToINodeStringAction(NodeAction _nodeAction)
		{
			return _nodeAction switch
			{
				NodeAction.EquipWeapon => EquipWeapon,
				_ => null
			};
		}
		private System.Action<float> NodeModelToINodeFloatAction(NodeAction _nodeAction)
		{
			return _nodeAction switch
			{
				NodeAction.AddRageGauge => AddRageGauge,
				_ => null
			};
		}

		private System.Func<bool> NodeModelToINodeCondition(NodeCondition _nodeCondition)
		{
			return _nodeCondition switch
			{
				NodeCondition.None => NoneCondition,
				NodeCondition.FerCloserMoveCondition => FerCloserMoveCondition,
				NodeCondition.DiscorverCondition => DiscorverCondition,
				NodeCondition.AttackCondition => AttackCondition,
				NodeCondition.JumpMoveCondition => JumpMoveCondition,
				NodeCondition.AIHostileStateNotDiscovery => AIHostileStateNotDiscovery,
				NodeCondition.TargetFindCondition => TargetFindCondition,
				NodeCondition.AttackRangeCondition => AttackRangeCondition,
				NodeCondition.AIHostileStateUnknow => AIHostileStateUnKnow,
				NodeCondition.AIHostileStateNotUnknow => AIHostileStateNotUnknow,
				NodeCondition.AIHostileStateInvestigate => AIHostileStateInvestigate,
				NodeCondition.AIHostileStateSuspicion => AIHostileStateSuspicion,
				NodeCondition.AIHostileStateDiscovery => AIHostileStateDiscovery,
				NodeCondition.JumpCheck => JumpCheck,
				NodeCondition.HitCheck => HitCheck,
				NodeCondition.HostileCheck => HostileCheck,
				NodeCondition.JumpAndTime1fCondition => JumpAndTime1fCondition,
				NodeCondition.GroundCondition => GroundCondition,
				NodeCondition.NotGroundCondition => NotGroundCondition,
				NodeCondition.CheckHPPercent50Condition => CheckHPPercent50Condition,
				NodeCondition.CheckHPPercent30Condition => CheckHPPercent30Condition,
				NodeCondition.CheckHPPercent20Condition => CheckHPPercent20Condition,
				NodeCondition.Time1FCondition => Time1fCondition,
				NodeCondition.InitCheck => InitCheck,
				NodeCondition.RageCheck => RageCheck,
				NodeCondition.NotRageCheck => NotRageCheck,
				NodeCondition.AroundRangeCondition => AroundRangeCondition,
				NodeCondition.SuspicionRangeCondition => SuspicionRangeCondition,
				NodeCondition.ViewRangeCondition => ViewRangeCondition,
				NodeCondition.OutSuspicionRangeCondition => OutSuspicionRangeCondition,
				NodeCondition.OutViewRangeCondition => OutViewRangeCondition,
				NodeCondition.LockOnCheck => LockOnCheck,
				NodeCondition.RageGaugeOverCheck => RageGaugeOverCheck,
				NodeCondition.RageGaugeUnderCheck => RageGaugeUnderCheck,
				NodeCondition.CheckAttacking => CheckAttacking,
				NodeCondition.CheckStrongAttacking => CheckStrongAttacking,
				_ => null
			};
		}
	}

}