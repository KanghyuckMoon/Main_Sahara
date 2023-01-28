using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
	[CreateAssetMenu(fileName = "QuestDataAllSO", menuName = "SO/QuestDataAllSO")]
	public class QuestDataAllSO : ScriptableObject
	{
		public List<QuestDataSO> questDataSOList;
	}
}
