using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
	public class HitClearObject : MonoBehaviour
	{
		[SerializeField]
		private string questClearKey;
		[SerializeField]
		private string[] hitboxTagArray;

		private void OnTriggerEnter(Collider other)
		{
			foreach (string _tag in hitboxTagArray)
			{
				if (other.CompareTag(_tag))
				{
					QuestManager.Instance.ChangeQuestClear(questClearKey);
				}
			}
		}
	}
}
