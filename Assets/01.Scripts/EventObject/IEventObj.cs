using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventObject
{
	public interface IEventObj
	{
		public bool IsAlreadyPlay
		{
			get;
			set;
		}

		public bool IsInfinityPlay
		{
			get;
			set;
		}

		public void PlayEvent();
	}
}
