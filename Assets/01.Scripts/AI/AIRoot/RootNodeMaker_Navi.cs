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
				IfAction(() => ParamCondition(CheckIsUsePath, FollowCondition), TrackMove),
				IfSelector(CheckIsUsePath, 
					IfAction(FollowCondition, TrackMoveRun), 
					IfAction(() => FollowCondition(2f), TrackMoveWalk), 
					Action(MoveReset)),
				//IgnoreAction(MoveReset),
				Action(FollowMove)
			);
		}
	}

}