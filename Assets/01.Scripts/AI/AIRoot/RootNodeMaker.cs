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


		public RootNodeMaker(AIModule _aiModule, string _address)
		{
			aiModule = _aiModule;
			Init(_address);
		}

		public void Init(string _address)
		{
			AddressablesManager.Instance.GetResourceAsync<AISO>(_address, SetAISO);
		}

		private void SetAISO(AISO _aiSO)
		{
			aiSO = _aiSO;
			aiModule.IsFirstAttack = aiSO.isFirstAttack;
			aiModule.IsHostilities = aiSO.isFirstAttack;
			aiModule.SetNode(ReturnRootNode());
		}

		public INode ReturnRootNode()
		{
			switch(aiSO.aiAddress)
			{
				default:
					NodeMakeSO nodeMakeSO = AddressablesManager.Instance.GetResource<NodeMakeSO>(aiSO.aiAddress);
					return NodeModelToINode(nodeMakeSO.nodeModel, null);
				case "TestTalkNPC":
					return TestTalkNPC();
				case "TestEnemy":
					return TestEnemy();
				case "TestWorm":
					return TestWorm();
				case "TestTrack":
					return TestTrack();
			}
		}

		private partial INode TestEnemy();
		private partial INode TestWorm();
		private partial INode TestTalkNPC();
		private partial INode TestTrack();

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
			Gizmos.DrawLine(drawPos, drawPos + (rightSusDir * aiSO.SuspicionRadius));
			Gizmos.DrawLine(drawPos, drawPos + (leftSusDir * aiSO.SuspicionRadius));
			Gizmos.DrawWireSphere(Position, aiSO.SuspicionAngle);

			Gizmos.color = aiSO.gizmoColor_attack;
			Vector3 rightAttackDir = AngleToDir(aiModule.MainModule.transform.eulerAngles.y + aiSO.AttackAngle * 0.5f);
			Vector3 leftAttackDir = AngleToDir(aiModule.MainModule.transform.eulerAngles.y - aiSO.AttackAngle * 0.5f);
			Gizmos.DrawLine(drawPos, drawPos + (rightAttackDir * aiSO.AttackRadius));
			Gizmos.DrawLine(drawPos, drawPos + (leftAttackDir * aiSO.AttackRadius));
			Gizmos.DrawWireSphere(Position, aiSO.AttackAngle);

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
			switch (_nodeModel.nodeType)
			{
				case NodeType.Action:
					_node = Action(NodeModelToINodeAction(_nodeModel.nodeAction));
					break;
				case NodeType.IgnoreAction:
					_node = IgnoreAction(NodeModelToINodeAction(_nodeModel.nodeAction));
					break;
				case NodeType.IfAction:
					_node = IfAction(NodeModelToINodeCondition(_nodeModel.nodeCondition), NodeModelToINodeAction(_nodeModel.nodeAction));
					break;
				case NodeType.Selector:
					_node = Selector();
					break;
				case NodeType.PercentRandomChoice:
					_node = PercentRandomChoiceNode(_nodeModel.changeDelay);
					break;
				case NodeType.PercentAction:
					_node = Action(NodeModelToINodeAction(_nodeModel.nodeAction));
					break;
				case NodeType.IfSelector:
					_node = IfSelector(NodeModelToINodeCondition(_nodeModel.nodeCondition));
					break;
			}
			if (_parent is not null)
			{
				if (_parent is CompositeNode)
				{
					(_parent as CompositeNode).Add(_node);
				}
				else if (_parent is PercentRandomChoiceNode && _nodeModel.nodeType is NodeType.PercentAction)
				{
					(_parent as PercentRandomChoiceNode).Add(PercentAction(_nodeModel.percent, _node));
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

		[CanBeNull]
		private System.Action NodeModelToINodeAction(NodeAction _nodeAction)
		{
			return _nodeAction switch
			{
				NodeAction.CloserMove => CloserMove,
				NodeAction.RunMove => RunMove,
				NodeAction.Jump => Jump,
				NodeAction.Attack => Attack,
				NodeAction.Reset => Reset,
				NodeAction.Ignore => Ignore,
				NodeAction.Rotate => Rotate,
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
				_ => null
			};
		}
		private System.Func<bool> NodeModelToINodeCondition(NodeCondition _nodeCondition)
		{
			return _nodeCondition switch
			{
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
				_ => null
			};
		}
	}

}