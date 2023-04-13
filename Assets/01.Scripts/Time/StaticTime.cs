using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace TimeManager
{
	public class StaticTime : Singleton<StaticTime>, IObserble 
	{
		public static float PlayerDeltaTime
		{
			get
			{
				return Time.deltaTime * playerTime * entierTime * uiTime;
			}
		}
		public static float EnemyDeltaTime
		{
			get
			{
				return Time.deltaTime * enemyTime * entierTime * uiTime;
			}
		}
		public static float PhysicsDeltaTime
		{
			get
			{
				return Time.deltaTime * physicsTime * entierTime * uiTime;
			}
		}
		public static float PhysicsFixedDeltaTime
		{
			get
			{
				return physicsTime * entierTime * uiTime * Time.fixedDeltaTime;
			}
		}
		public static float PhysicsPlayerFixedDeltaTime
		{
			get
			{
				return playerTime * physicsTime * entierTime * uiTime * Time.fixedDeltaTime;
			}
		}

		public static float PhysicsEnemyFixedDeltaTime
		{
			get
			{
				return enemyTime * physicsTime * entierTime * uiTime * Time.fixedDeltaTime;
			}
		}


		public static float PlayerTime
		{
			get
			{
				return playerTime * entierTime * uiTime;
			}
			set
			{
				playerTime = value;
				StaticTime.Instance.GetIObserble().Send();
			}
		}
		public static float EnemyTime
		{
			get
			{
				return enemyTime * entierTime * uiTime;
			}
			set
			{
				enemyTime = value;
				StaticTime.Instance.GetIObserble().Send();
			}
		}
		public static float PhysicsTime
		{
			get
			{
				return physicsTime * entierTime * uiTime;
			}
			set
			{
				physicsTime = value;
				StaticTime.Instance.GetIObserble().Send();
			}
		}

		public static float EntierTime
		{
			get
			{
				return entierTime;
			}
			set
			{
				entierTime = value;
				StaticTime.Instance.GetIObserble().Send();
			}
		}
		public static float UITime
		{
			get
			{
				return uiTime;
			}
			set
			{
				uiTime = value;
				StaticTime.Instance.GetIObserble().Send();
			}
		}

		public List<Observer> Observers
		{
			get
			{
				return observers;
			}
		}

		private static float playerTime = 1f;
		private static float enemyTime = 1f;
		private static float physicsTime = 1f;
		private static float entierTime = 1f;
		private static float uiTime = 1f;
		private static List<Observer> observers = new List<Observer>();
		private IObserble _obserble;

		public float GetEntireTimeWithIndi(float time)
		{
			return entierTime * uiTime * time;
		}
		public float GetEnemyTimeWithIndi(float time)
		{
			return enemyTime * uiTime * time;
		}
		public float GetPlayerTimeWithIndi(float time)
		{
			return playerTime * uiTime * time;
		}

		public IObserble GetIObserble()
		{
			_obserble ??= (StaticTime.Instance as IObserble);
			return _obserble;
		}
	}

}