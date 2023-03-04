using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Streaming
{
	[System.Serializable]
	public class SaveRecordDataList
	{
		public List<SaveRecordData> dateList = new List<SaveRecordData>();
	}


	[System.Serializable]
	public class SaveRecordData
	{
		public string date;
		public string imagePath;
	}

}
