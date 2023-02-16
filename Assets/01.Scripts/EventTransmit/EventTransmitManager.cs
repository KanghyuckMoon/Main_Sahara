using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using Inventory;
using Quest;

namespace EventTransmit
{
	public class EventTransmitManager : MonoSingleton<EventTransmitManager>
	{
		//인벤토리 -> 퀘스트

		public void Awake()
		{
			InventoryManager.Instance.InventoryEventTransmit += SendEvent;
			QuestManager.Instance.QuestEventTransmit += SendEvent;
		}

		public void SendEvent(string _sender, string _recipient, object _obj)
		{
			switch(_recipient)
			{
				case "InventoryManager":
					InventoryManager.Instance.ReceiveEvent(_sender, _obj);
					break;
				case "QuestManager":
					QuestManager.Instance.ReceiveEvent(_sender, _obj);
					break;
			}
		}

	}
}
