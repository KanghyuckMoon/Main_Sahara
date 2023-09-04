using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using Inventory;
using Quest;
using Json;
using Streaming;
using UI.Popup;

namespace EventTransmit
{
	public class EventTransmitManager : MonoSingleton<EventTransmitManager>
	{
		//인벤토리 -> 퀘스트

		public override void Awake()
		{
			base.Awake();
			InventoryManager.Instance.InventoryEventTransmit = SendEvent;
			QuestManager.Instance.QuestEventTransmit = SendEvent;
			SaveManager.Instance.SaveEventTransmit = SendEvent;
			StreamingManager.Instance.StreamingEventTransmit = SendEvent;
			PopupUIManager.Instance.PopupEventTransmit = SendEvent; 
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
				case "SaveManager":
					SaveManager.Instance.ReceiveEvent(_sender, _obj);
					break;
				case "StreamingManager":
					StreamingManager.Instance.ReceiveEvent(_sender, _obj);
					break;
				case "PopupUIManager":
					PopupUIManager.Instance.ReceiveEvent(_sender, _obj);
					break; 
			}
		}

	}
}
