using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer
{
	void Receive();
}

public interface Obserble
{
	public List<Observer> Observers
	{
		get;
	}

	void AddObserver(Observer _observer);

	void Send();
}