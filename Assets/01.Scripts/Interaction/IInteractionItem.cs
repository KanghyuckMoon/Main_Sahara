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

		public string Name
		{
			get;
		}

		public Vector3 PopUpPos
		{
			get;
		}

		public string ActionName
		{
			get;
		}
		
		void Interaction();
	}
}

