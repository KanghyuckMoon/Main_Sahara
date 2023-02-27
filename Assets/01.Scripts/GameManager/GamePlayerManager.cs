using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace GameManager
{
	public class GamePlayerManager : MonoSingleton<GamePlayerManager>, Obserble
	{
		public bool IsPlaying
		{
			get
			{
				return isPlaying;
			}
			set
			{
				isPlaying = value;
				Send();
			}
		}

		public List<Observer> Observers => observers;
		public List<Observer> observers = new List<Observer>();

		private bool isPlaying;

		public void AddObserver(Observer _observer)
		{
			Observers.Add(_observer);
		}

		public void Send()
		{
			foreach (Observer _observer in Observers)
			{
				_observer.Receive();
			}
		}
	}
}
