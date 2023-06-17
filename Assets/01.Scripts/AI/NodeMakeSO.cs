using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.Animations;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AI
{
	public enum NodeType
	{
		Action = 0,
		Selector,
		PercentRandomChoice,
		PercentAction,
		IfSelector,
		StringAction,
		FloatAction,
		Count,
	}

	public enum NodeAction
	{
		None,
		CloserMove,
		RunMove,
		Jump,
		Attack,
		Reset,
		Ignore,
		Rotate,
		JumpAndRunMove,
		TargetFind,
		SuspicionGaugeSet,
		MoveReset,
		WalkAwayMove,
		RunAwayMove,
		SetMoveDir,
		RotateXYZ,
		ModelRotateXYZ,
		TrackMove,
		AroundOriginPos,
		AroundLastFindPlayerPos,
		SkillWeapon = 21,
		SkillE,
		SkillR,
		RageOn,
		Nothing,
		FixiedMove,
		LockOnPlayer,
		StrongAttack,
		RageOff,
		DirectRotate,
		SetAroundOrigin,
		SetAroundPlayer,
		AroundMove,
		InstantDiscovery,
		HitReset,
		//StringAction
		EquipWeapon = 20,
		//FloatAction
		AddRageGauge = 100,
	}

	public enum NodeCondition
	{
		None,
		FerCloserMoveCondition,
		DiscorverCondition,
		AttackCondition,
		JumpMoveCondition,
		AIHostileStateNotDiscovery,
		TargetFindCondition,
		AttackRangeCondition,
		AIHostileStateUnknow,
		AIHostileStateNotUnknow,
		AIHostileStateInvestigate,
		AIHostileStateSuspicion,
		AIHostileStateDiscovery,
		JumpCheck,
		HitCheck,
		HostileCheck,
		JumpAndTime1fCondition,
		GroundCondition,
		NotGroundCondition,
		CheckHPPercent50Condition,
		CheckHPPercent30Condition,
		CheckHPPercent20Condition,
		Time1FCondition,
		InitCheck,
		RageCheck,
		NotRageCheck,
		AroundRangeCondition,
		SuspicionRangeCondition,
		ViewRangeCondition,
		OutSuspicionRangeCondition,
		OutViewRangeCondition,
		LockOnCheck,
		RageGaugeOverCheck,
		RageGaugeUnderCheck,
		CheckAttacking,
		CheckStrongAttacking,
		CheckAttackState,
		AttackRangeCondition2,
	}

	[CreateAssetMenu(fileName = "NodeMakeSO", menuName = "SO/NodeMakeSO")]
	public class NodeMakeSO : ScriptableObject
	{
		public NodeModel nodeModel = new NodeModel();
		public List<NodeModel> nodes = new List<NodeModel>();

		[ContextMenu("NodeListToNodeSO")]
		public void NodeListToNodeSO()
		{
			NodeModel _rootModel = nodes.Where(x => x.isRoot).First();
			if(_rootModel != null)
			{
				nodeModel = _rootModel;
			}
		}

		[ContextMenu("NodeSOToNodeList")]
		public void NodeSOToNodeList()
		{
			nodes.Clear();
			AddNodeModelToList(nodeModel, 0, 0, 0, 0, 0);
		}

		private void AddNodeModelToList(NodeModel _nodeModel, int childGrade, int parentsThisChildCount, int parentsChildCount, float parentsPosY, float additionHeight)
		{
			CreateNodeModel(_nodeModel);
			_nodeModel.position = new Vector2(120 * childGrade, additionHeight + parentsPosY + - 60 * parentsChildCount + 120 * parentsThisChildCount);
			for (int i = 0; i < _nodeModel.nodeModelList.Count; ++i)
			{
				if(i > 0 && _nodeModel.nodeModelList[i - 1].nodeModelList.Count > 1)
				{
					float _additionHeight = 120 * (_nodeModel.nodeModelList[i - 1].nodeModelList.Count - 1);
					AddNodeModelToList(_nodeModel.nodeModelList[i], childGrade + 1, i, _nodeModel.nodeModelList.Count - 1, _nodeModel.position.y, _additionHeight);
				}
				else
				{
					AddNodeModelToList(_nodeModel.nodeModelList[i], childGrade + 1, i, _nodeModel.nodeModelList.Count - 1, _nodeModel.position.y, 0);
				}
			}
		}

		public NodeModel CreateNodeModel(NodeType _type)
		{
			NodeModel _node = new NodeModel();
			_node.nodeType = _type;
			_node.guid = GUID.Generate().ToString();
			nodes.Add(_node);


#if UNITY_EDITOR

			AssetDatabase.SaveAssets();

#endif

			return _node;
		}
		public NodeModel CreateNodeModel(NodeModel _nodeModel)
		{
			_nodeModel.guid = GUID.Generate().ToString();
			nodes.Add(_nodeModel);


#if UNITY_EDITOR

			AssetDatabase.SaveAssets();

#endif

			return _nodeModel;
		}

		public void DeleteNode(NodeModel _node)
		{
			nodes.Remove(_node);
#if UNITY_EDITOR

			AssetDatabase.SaveAssets();

#endif
		}

		public void AddChild(NodeModel _parent, NodeModel _child)
		{
			_parent.AddChild(_child);
		}
		public void RemoveChild(NodeModel _parent, NodeModel _child)
		{
			_parent.RemoveChild(_child);
		}
		public List<NodeModel> GetChild(NodeModel _node)
		{
			return _node.nodeModelList;
		}

	}

	public class InspectorNodeModel : ScriptableObject
	{
		public NodeModel nodeModel;
	}

	[System.Serializable]
	public class NodeModel
	{
		public bool isRoot;
		public NodeType nodeType;
		public NodeCondition nodeCondition;
		public NodeAction nodeAction;

		[Header("PercentRandomChoiceNode")]
		public float changeDelay;

		[Header("PercentActionNode")]
		public float percent;

		[Header("stringNode")] 
		public string str;

		[Header("FloatNode")]
		public float value;

		[Header("ConditionCheck")] 
		public float delay;
		public bool isIgnore;
		public bool isInvert;
		public bool isUseTimer;
		public bool isInvertTime;

		[Header("InEditor")]
		public string guid;

		[Header("InEditor")]
		public Vector2 position;

		public List<NodeModel> nodeModelList = new List<NodeModel>();

		public void AddChild(NodeModel _node)
		{
			nodeModelList.Add(_node);
		}

		public void RemoveChild(NodeModel _node)
		{
			nodeModelList.Remove(_node);
		}

	}
}