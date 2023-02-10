using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module.Talk
{
	[CreateAssetMenu(fileName = "TalkDataSO", menuName = "SO/TalkDataSO")]
	public class TalkDataSO : ScriptableObject
	{
		public List<TalkData> talkDataList = new List<TalkData>();

		public List<string> defaultTalkCodeList = new List<string>();
		public List<string> defaultAutherCodeList = new List<string>();
	}
}
