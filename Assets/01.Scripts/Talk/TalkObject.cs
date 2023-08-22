using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utill.Measurement;
using Quest;
using Module;
using Utill.Addressable;
using Module.Shop;
using Shop;
using UI.PublicManager;
using Pool;
using System.Diagnostics;
using System;

namespace Module.Talk
{
	public class TalkObject : MonoBehaviour
	{
		public bool IsEndTalk
		{
			get
			{
				return isEndTalk;
			}
			set
			{
				isEndTalk = value;
			}
		}

		[SerializeField]
		private string talkCode;
		[SerializeField]
		private string authorCode;

		[SerializeField]
		private UnityEvent endTalkEvent;

		private int index = 0;

		public bool IsCanTalk
		{
			get
			{
				return isCanTalk;
			}
			set
			{
				isCanTalk = value;
			}
		}

		private bool isFirst = false;
		private bool isEndTalk = false;
		private bool isTalking = false;

		private bool isCanTalk = true;

		public void Talk()
		{
			if (!isCanTalk)
			{
				return;
			}
			if (!isTalking)
			{
				Logging.Log("대화 가능");

				//대화
				GetText();
				isEndTalk = false;
				isTalking = true;
			}
		}

		private void GetText()
		{
			PublicUIManager.Instance.SetTexts(authorCode, talkCode, EndTalk);
			isTalking = true;
		}

		private void EndTalk()
		{
			isEndTalk = true;
			isTalking = false;
			endTalkEvent?.Invoke();
		}
	}
}
