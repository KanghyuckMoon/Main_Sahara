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
		public bool isTalkQuest = false;

		//퀘스트 상태에 따른 변수

		[Header("Position")]
		public Vector3 goalPosition = new Vector3(0,4000,0);
		public float distance = 3f;

		[Header("TargetMonster")]
		//

		[Header("MonsterType")]
		//

		[Header("DebugData")]
		//

		[Header("TargetObject")]
		//

		[Header("MiniGame")]
		//

		[Header("Stat")]
		//

		[Header("Inventory")]
		public string itemName = null;

		[Header("Mission")]
		//

		[Header("TargetNPC")]
		//

		[Header("TargetInteractionItem")]
		//
		
		[Header("Time")]
		public float afterTime = 5f;
	}

}