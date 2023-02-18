using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager
{
    public class StaticTimeController : MonoBehaviour
	{
		public float playerTime = 1f;
		public float enemyTime = 1f;
		public float physicsTime = 1f;
		public float entierTime = 1f;

		private void OnValidate()
		{
			StaticTime.PlayerTime = playerTime;
			StaticTime.EnemyTime = enemyTime;
			StaticTime.PhysicsTime = physicsTime;
			StaticTime.EntierTime = entierTime;
		}

	}
}