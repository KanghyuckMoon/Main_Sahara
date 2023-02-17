using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Streaming;
using UnityEditor;

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
		public bool isUpdate = true; //���� ����ȭ�� ���� ������Ʈ�� �� ������ �Ǵ� ����Ʈ�� ������Ʈ�� �� �������� ó��

		//����Ʈ ���¿� ���� ����

		[Header("Position"), Space(10)]
		public Vector3 goalPosition = new Vector3(0,4000,0);
		public float distance = 3f;


		[Header("TargetMonster"), Space(10)]
		//None

		[Header("TargetObject"), Space(10)]
		//None

		[Header("Inventory"), Space(10)]
		public string itemKey;
		public int needCount = 1;

		[Header("Time"), Space(10)]
		public float afterTime = 5f;
	}

}