using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Networking;
using Utill.Pattern;

namespace GoogleSpreadSheet
{
	public class TextManager : MonoSingleton<TextManager>
	{
		public TextSO TextSO
		{
			get
			{
				if (!isInit)
				{
					textSO.InitTextDatas();

					//추후 언어에 따라 다르게 설정
					StartCoroutine(GetText());
				}
				return textSO;
			}
		}
		public bool IsInit => isInit;

		[SerializeField]
		private TextSO textSO = null;

		[SerializeField]
		private bool isReset = false;

		private bool isInit =	false;

		public void Start()
		{
			if (!isReset)
			{
				isInit = true;
				return;
			}

			if (!isInit)
			{
				Instance.textSO.InitTextDatas();
				//추후 언어에 따라 다르게 설정
				StartCoroutine(GetText());
			}
		}

		[ContextMenu("InitTextData")]
		public void InitTextData()
		{
			textSO.InitTextDatas();
			//추후 언어에 따라 다르게 설정
			StartCoroutine(GetText());
		}

		/// <summary>
		/// 키를 통해 텍스트를 가져옴
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetText(string key)
		{
			return TextSO.GetText(key);
		}

		private IEnumerator GetText()
		{
			UnityWebRequest wwwOTHER = UnityWebRequest.Get(URL.OTHER);
			yield return wwwOTHER.SendWebRequest();
			SetTextOTHER(wwwOTHER.downloadHandler.text);

			UnityWebRequest wwwITEM = UnityWebRequest.Get(URL.ITEM);
			yield return wwwITEM.SendWebRequest();
			SetTextITEM(wwwITEM.downloadHandler.text);

			UnityWebRequest wwwMONSTER = UnityWebRequest.Get(URL.MONSTER);
			yield return wwwMONSTER.SendWebRequest();
			SetTextMONSTER(wwwMONSTER.downloadHandler.text);

			UnityWebRequest wwwTALK = UnityWebRequest.Get(URL.TALK);
			yield return wwwTALK.SendWebRequest();
			SetTextTALK(wwwTALK.downloadHandler.text);

			UnityWebRequest wwwQUEST = UnityWebRequest.Get(URL.QUEST);
			yield return wwwQUEST.SendWebRequest();
			SetTextQUEST(wwwQUEST.downloadHandler.text);
			
			UnityWebRequest wwwINTERACTION = UnityWebRequest.Get(URL.INTERACTION);
			yield return wwwINTERACTION.SendWebRequest();
			SetTextINTERACTION(wwwINTERACTION.downloadHandler.text);
			isInit = true;
			
#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty(textSO);
#endif
		}

		private void SetTextOTHER(string tsv)
		{
			string[] row = tsv.Split('\n');
			int rowSize = row.Length;

			for (int i = 1; i < rowSize; i++)
			{
				string[] column = row[i].Split('\t');
				textSO.AddTextData(column[0], column[1]);
			}
		}
		private void SetTextITEM(string tsv)
		{
			string[] row = tsv.Split('\n');
			int rowSize = row.Length;

			for (int i = 1; i < rowSize; i++)
			{
				string[] column = row[i].Split('\t');
				textSO.AddTextData(column[0], column[1]);
				textSO.AddTextData(column[2], column[3]);
			}
		}
		private void SetTextMONSTER(string tsv)
		{
			string[] row = tsv.Split('\n');
			int rowSize = row.Length;

			for (int i = 1; i < rowSize; i++)
			{
				string[] column = row[i].Split('\t');
				textSO.AddTextData(column[0], column[1]);
			}
		}
		private void SetTextTALK(string tsv)
		{
			string[] row = tsv.Split('\n');
			int rowSize = row.Length;

			for (int i = 1; i < rowSize; i++)
			{
				string[] column = row[i].Split('\t');
				textSO.AddTextData(column[0], column[1]);
				textSO.AddTextData(column[2], column[3]);
			}
		}
		private void SetTextQUEST(string tsv)
		{
			string[] row = tsv.Split('\n');
			int rowSize = row.Length;

			for (int i = 1; i < rowSize; i++)
			{
				string[] column = row[i].Split('\t');
				textSO.AddTextData(column[0], column[1]);
				textSO.AddTextData(column[2], column[3]);
			}
		}
		
		private void SetTextINTERACTION(string tsv)
		{
			string[] row = tsv.Split('\n');
			int rowSize = row.Length;

			for (int i = 1; i < rowSize; i++)
			{
				string[] column = row[i].Split('\t');
				textSO.AddTextData(column[0], column[1]);
			}
		}
	}
}
