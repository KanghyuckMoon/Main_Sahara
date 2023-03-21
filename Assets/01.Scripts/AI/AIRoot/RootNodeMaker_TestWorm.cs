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
		private partial INode TestWorm()
		{
			return Selector
			(
				IgnoreAction(Reset), //리셋
				IgnoreAction(TargetFind),
				IfAction(AIHostileStateNotDiscovery, MoveReset),
				IgnoreAction(ModelRotateXYZ),
				IfSelector(JumpAndTime1fCondition, 
				IgnoreAction(Jump),
				Action(SetMoveDir))
				//IfAction(NotJumpCondition, CloserMove)
			);
		}
	}

}