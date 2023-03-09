using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

public class EventTriggerManager : MonoSingleton<EventTriggerManager>
{
	private Dictionary<string, System.Action> eventActionDic = new Dictionary<string, System.Action>();

	public void EventCall(string _message)
	{
		if (eventActionDic.TryGetValue(_message, out var _action))
		{
			_action?.Invoke();
		}
	}

	public void AddAction(string _message, System.Action _addAction)
	{
		if (eventActionDic.TryGetValue(_message, out var _action))
		{
			_action += _addAction;
		}
		else
		{
			eventActionDic.Add(_message, _addAction);
		}
	}
	
	public void RemoveAction(string _message, System.Action _removeAction)
	{
		if (eventActionDic.TryGetValue(_message, out var _action))
		{
			_action -= _removeAction;
		}
	}


}
