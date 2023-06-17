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