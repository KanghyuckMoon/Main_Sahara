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
		private partial INode Navi()
		{
			return Selector
			(
				IgnoreAction(Reset),
				IfSelector(CheckIsUsePath, 
					IfAction(FollowCondition, TrackMoveRun), 
					IfAction(() => FollowCondition(2f), TrackMoveWalk), 
					Action(MoveReset)),
				//IfAction(CheckIsUsePath, MoveReset),
				IgnoreAction(MoveReset),
				Action(FollowMove)
			);
		}
	}

}