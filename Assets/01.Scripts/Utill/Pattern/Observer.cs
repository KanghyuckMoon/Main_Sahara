using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer
{
	void Receive();
}

public interface IObserble
{
	public List<Observer> Observers
	{
		get;
	}

	void AddObserver(Observer _observer)
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
		foreach (var _observer in Observers)
		{
			_observer.Receive();
		}
	}
}