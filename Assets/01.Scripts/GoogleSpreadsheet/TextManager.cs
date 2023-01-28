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
		[SerializeField]
		private TextSO textSO = null;

		public void Start()
		{
			textSO.InitTextDatas();

			//���� �� ���� �ٸ��� ����
			StartCoroutine(GetText());
		}

		/// <summary>
		/// Ű�� ���� �ؽ�Ʈ�� ������
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetText(string key)
		{
			return textSO.GetText(key);
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
	}
}
