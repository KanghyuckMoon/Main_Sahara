using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace TimeManager
{
	public class StaticTime : Singleton<StaticTime>, Obserble 
	{
		public static float PlayerDeltaTime
		{
			get
			{
				return Time.unscaledDeltaTime * playerTime * entierTime * uiTime;
			}
		}
		public static float EnemyDeltaTime
		{
			get
			{
				return Time.unscaledDeltaTime * enemyTime * entierTime * uiTime;
			}
		}
		public static float PhysicsDeltaTime
		{
			get
			{
				return Time.unscaledDeltaTime * physicsTime * entierTime * uiTime;
			}
		}
		public static float PhysicsFixedDeltaTime
		{
			get
			{
				return Time.fixedUnscaledDeltaTime * physicsTime * entierTime * uiTime;
			}
		}

		public static float PlayerTime
		{
			get
			{
				return playerTime;
			}
			set
			{
				playerTime = value;
				StaticTime.Instance.Send();
			}
		}
		public static float EnemyTime
		{
			get
			{
				return enemyTime;
			}
			set
			{
				enemyTime = value;
				StaticTime.Instance.Send();
			}
		}
		public static float PhysicsTime
		{
			get
			{
				return physicsTime;
			}
			set
			{
				physicsTime = value;
				StaticTime.Instance.Send();
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
				StaticTime.Instance.Send();
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
				StaticTime.Instance.Send();
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

		public void AddObserver(Observer _observer)
		{
			if (!Observers.Contains(_observer))
			{
				Observers.Add(_observer);
			}
		}

		public void RemoveObserver(Observer _observer)
		{
			if (Observers.Contains(_observer))
			{
				Observers.Remove(_observer);
			}
		}

		public void Send()
		{
			foreach (Observer observer in Observers)
			{
				observer.Receive();
			}
		}
	}

}