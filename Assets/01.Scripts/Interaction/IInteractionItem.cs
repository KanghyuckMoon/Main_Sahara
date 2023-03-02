using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
	public interface IInteractionItem
	{
		bool Enabled
		{
			get;
			set;
		}
		void Interaction();
	}
}

