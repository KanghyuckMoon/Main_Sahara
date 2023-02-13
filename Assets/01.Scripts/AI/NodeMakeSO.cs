using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
	public enum NodeType
	{
		Action,
		IgnoreAction,
		IfAction,
		Selector,
		PercentRandomChoice,
		PercentAction,

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
	}

	public enum NodeCondition
	{
		None,
		FerCloserMoveCondition,
		DiscorverCondition,
		AttackCondition,
		JumpMoveCondition,
		NotDiscoveryCondition,
		DiscoveryCondition,
		TargetFindCondition,
		AttackRangeCondition,
	}

	[CreateAssetMenu(fileName = "NodeMakeSO", menuName = "SO/NodeMakeSO")]
	public class NodeMakeSO : ScriptableObject
	{
		public NodeModel nodeModel = new NodeModel();
	}

	[System.Serializable]
	public class NodeModel
	{
		public NodeType nodeType;
		public NodeCondition nodeCondition;
		public NodeAction nodeAction;

		[Header("PercentRandomChoiceNode")]
		public float changeDelay;

		[Header("PercentActionNode")]
		public float percent;

		public List<NodeModel> nodeModelList = new List<NodeModel>();
	}
}