using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quest;

namespace Module.Talk
{
	public enum TalkCondition
	{
		Quest,
		Position,
		HandWork,
		None,
	}

	[System.Serializable]
	public class TalkData
	{
		public string talkText;
		public string authorText;
		public TalkCondition talkCondition;

		//Condition Check
		public string questKey;
		public QuestState questState;

	}
}
