using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace EventTrigger
{
	public class EventTriggerManager : MonoSingleton<EventTriggerManager>
	{
		private Dictionary<string, System.Action<string>> eventActionDic = new Dictionary<string, System.Action<string>>();

		public void EventCall(string _message)
		{
			if (eventActionDic.TryGetValue(_message, out var _action))
			{
				_action?.Invoke(_message);
			}
		}

		public void AddAction(string _message, System.Action<string> _addAction)
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

		public void RemoveAction(string _message, System.Action<string> _removeAction)
		{
			if (eventActionDic.TryGetValue(_message, out var _action))
			{
				_action -= _removeAction;
			}
		}


	}

}