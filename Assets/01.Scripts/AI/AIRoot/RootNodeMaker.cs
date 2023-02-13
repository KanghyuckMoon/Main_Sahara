using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
			aiSO = AddressablesManager.Instance.GetResource<AISO>(_address);
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
			}
		}

		private partial INode TestEnemy();
		private partial INode TestTalkNPC();

		public void Dead()
		{
			aiModule.AIModuleHostileState = AIModule.AIHostileState.Unknow;
			aiModule.MainModule.ObjDir = Vector3.zero;
			Reset();
		}

		public void OnDrawGizmo()
		{
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
				_ => null,
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
				NodeCondition.NotDiscoveryCondition => NotDiscoveryCondition,
				NodeCondition.DiscoveryCondition => DiscorverCondition,
				NodeCondition.TargetFindCondition => TargetFindCondition,
				NodeCondition.AttackRangeCondition => AttackRangeCondition,
				_ => null,
			};
		}
	}

}