using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventTrigger;
using Utill.Pattern;
using Utill.Addressable;

namespace Tutorial
{
	public class TutorialManager : MonoSingleton<TutorialManager>
	{
		private TutorialSO tutorialSO;
		private TutorialSaveData tutorialSaveData = new TutorialSaveData();

		private void Start()
		{
			tutorialSO = AddressablesManager.Instance.GetResource<TutorialSO>("TutorialSO");
			AddEvent();
		}

		private void AddEvent()
		{
			foreach(var _obj in tutorialSO.tutorialKeyDic)
			{
				EventTriggerManager.Instance.AddAction(_obj.Key, CheckMessage);
			}
		}

		private void CheckMessage(string _message)
		{
			if (!CheckAlreadyView(_message))
			{
				CheckAlreadyView(tutorialSO.tutorialKeyDic[_message]);
			}
		}

		private bool CheckAlreadyView(string _popUpKey)
		{
			if (tutorialSaveData.checkPopUpKeyList.Contains(_popUpKey))
			{
				return true;
			}
			else
			{
				tutorialSaveData.checkPopUpKeyList.Add(_popUpKey);
				return false;
			}
		}
	}
}
