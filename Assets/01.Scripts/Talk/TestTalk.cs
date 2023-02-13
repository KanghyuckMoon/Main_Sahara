using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSpreadSheet;
using Utill.Measurement;
using Quest;
public class TestTalk : MonoBehaviour
{
	[SerializeField]
	private string talkCode;
	[SerializeField]
	private string authorCode;

	private int index = 0;

	public IEnumerator Start()
	{
		while(!TextManager.Instance.IsInit)
		{
			yield return null;
		}

		LogText();
	}

	public void LogText()
	{
		string text = TextManager.Instance.GetText($"{talkCode}_{index}");
		string authortext = TextManager.Instance.GetText($"{authorCode}_{index}");
		Logging.Log($"{text}{authortext}");

		if (authortext[0] is '!')
		{
			switch (authortext)
			{
				case "!END\r":
					return;
				case "!TACTIVE\r":
					QuestManager.Instance.ChangeQuestActive(text);
					return;
				case "!TCLEAR\r":
					QuestManager.Instance.ChangeQuestClear(text);
					return;
			}
		}

		index++;
		LogText();
	}

}
