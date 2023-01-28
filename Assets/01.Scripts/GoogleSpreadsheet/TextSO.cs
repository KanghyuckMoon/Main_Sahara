using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.SeralizableDictionary;

namespace GoogleSpreadSheet
{
	
	[CreateAssetMenu(fileName = "TextSO", menuName = "SO/TextSO")]
	public class TextSO : ScriptableObject
	{
		[SerializeField]
		private StringString textDataDic = new StringString();

		[ContextMenu("DebugAllTexts")]
		public void DebugAllTexts()
		{
			foreach (var txt in textDataDic)
			{
				Debug.Log($"KEY : {txt.Key}, VALUE : {txt.Value}");
			}
		}

		public void InitTextDatas()
		{
			textDataDic.Clear();
		}

		public void AddTextData(string key, string value)
		{
			textDataDic.Add(key, value);
		}

		public string GetText(string key)
		{
			if(textDataDic.TryGetValue(key, out string value))
			{
				return value;
			}
			Debug.LogWarning("Null Text Data");
			return null;
		}
	}
}