using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Streaming;

namespace Quest
{
	[CreateAssetMenu(fileName = "QuestCreateObjectSO", menuName = "SO/QuestCreateObjectSO")]
	public class QuestCreateObjectSO : ScriptableObject
	{
		public string targetSceneName;
		public List<ObjectDataSO> objectDataList;
	}

}