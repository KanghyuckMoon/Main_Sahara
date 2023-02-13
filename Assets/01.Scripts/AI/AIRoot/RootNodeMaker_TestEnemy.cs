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
		private partial INode TestEnemy()
		{
			return Selector
			(
				IgnoreAction(Reset), //리셋
				IgnoreAction(TargetFind),
				IgnoreAction(SuspicionGaugeSet),
				IfAction(NotDiscoveryCondition, Ignore),
				IfAction(FerCloserMoveCondition, RunMove), //너무 멀리 있으면 근접
				IfSelector(AttackRangeCondition, //공격 사거리 안에 들어왔으면
					IfAction(AttackCondition, Attack), //시야각이 되면 공격
					Action(Rotate) //시야각이 안 되면 회전
				),
				IfAction(JumpMoveCondition, JumpAndRunMove),
				Action(CloserMove)
				//PercentRandomChoiceNode(0.1f,
				//	PercentAction(10f, Action(Jump)), //점프
				//	PercentAction(90f, Action(CloserMove)) //근접
				//	)
			);
		}
	}

}