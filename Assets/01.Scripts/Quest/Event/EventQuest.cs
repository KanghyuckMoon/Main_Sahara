using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class EventQuest : MonoBehaviour
    {
        [SerializeField]
        private string questKey;

        [SerializeField]
        private QuestState setQuestState;
        
        public void QuestSet()
        {
            if(string.IsNullOrEmpty(questKey))
			{
                return;
			}

            switch (setQuestState)
            {
                case QuestState.Disable:
                    break;
                case QuestState.Discoverable:
                    QuestManager.Instance.ChangeQuestDiscoverable(questKey);
                    break;
                case QuestState.Active:
                    QuestManager.Instance.ChangeQuestActive(questKey);
                    break;
                case QuestState.Achievable:
                case QuestState.Clear:
                    QuestManager.Instance.ChangeQuestClear(questKey);
                    break;
                default:
                    break;
            }
        }
    }   
}
