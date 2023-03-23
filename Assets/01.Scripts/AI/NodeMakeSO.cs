using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		[Header("stringNode")] 
		public string str;

		[Header("FloatNode")]
		public float value;

		[Header("ConditionCheck")] 
		public float delay;
		public bool isIgnore;
		public bool isInvert;
		public bool isUseTimer;


		public List<NodeModel> nodeModelList = new List<NodeModel>();
	}
}