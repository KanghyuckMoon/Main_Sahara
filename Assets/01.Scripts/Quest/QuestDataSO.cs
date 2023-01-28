using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Streaming;

namespace Quest
{
	[CreateAssetMenu(fileName = "QuestDataSO", menuName = "SO/QuestDataSO")]
	public class QuestDataSO : ScriptableObject
	{
		public string questKey;
		public string nameKey;
		public string explanationKey;
		public QuestState earlyQuestState;
		public QuestConditionType questConditionType;
		public List<string> linkQuestKeyList;
		public List<QuestCreateObjectSO> questCreateObjectSOList;
	}

}