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
		private partial INode NPC()
		{
			return Selector
			(
				IgnoreAction(Reset),
				Action(TrackMove)
			);
		}
	}

}