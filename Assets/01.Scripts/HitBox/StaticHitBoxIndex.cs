using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitBox
{
	public static class StaticHitBoxIndex
	{
		private static ulong hitBoxIndex;

		public static ulong GetHitBoxIndex()
		{
			hitBoxIndex += 20000;
			return hitBoxIndex;
		}
	}
}
