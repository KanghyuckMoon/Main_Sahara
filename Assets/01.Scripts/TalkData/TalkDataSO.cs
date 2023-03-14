using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module.Talk
{
	[CreateAssetMenu(fileName = "TalkDataSO", menuName = "SO/TalkDataSO")]
	public class TalkDataSO : ScriptableObject
	{
		public string key;

		public List<TalkData> talkDataList = new List<TalkData>();

		public List<string> defaultTalkCodeList = new List<string>();
		public List<string> defaultAutherCodeList = new List<string>();

		public float talkRange = 1f; //대화 가능 거리

		public void Copy(TalkDataSO _talkDataSO)
		{
			key = _talkDataSO.key;
			talkDataList = _talkDataSO.talkDataList;
			defaultTalkCodeList = _talkDataSO.defaultTalkCodeList;
			defaultAutherCodeList = _talkDataSO.defaultAutherCodeList;
			talkRange = _talkDataSO.talkRange;

		}

	}
}
