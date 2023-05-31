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

        [ContextMenu("asdhjflakwefku")]
        public void SetName()
        {
            var str = GetComponentInParent<QuestObject>().name.Replace("Treasure_Chest_Detect", "EVQM");
            questKey = str.Trim();
        }
        
        public void QuestSet()
        {
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
