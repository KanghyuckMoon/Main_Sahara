using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module.Talk
{
	[CreateAssetMenu(fileName = "AllTalkDataSO", menuName = "SO/AllTalkDataSO")]
	public class AllTalkDataSO : ScriptableObject
	{
		public List<TalkDataSO> talkDataSOList = new List<TalkDataSO>();
	}
}
