using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class QuestEasyClear : MonoBehaviour
    {
        [SerializeField]
        private string questKey;

        [ContextMenu("QuestClearForce")]
        public void QuestClearForce()
        {
            QuestManager.Instance.ChangeQuestClearForce(questKey);
        }

        [ContextMenu("QuestClearOrAchive")]
        public void QuestClearOrAchive()
        {
            QuestManager.Instance.ChangeQuestClear(questKey);
        }
        
        
        [ContextMenu("QuestActive")]
        public void QuestActive()
        {
            QuestManager.Instance.ChangeQuestActive(questKey);
        }
        
        [ContextMenu("QuestDiscorver")]
        public void QuestDiscorver()
        {
            QuestManager.Instance.ChangeQuestDiscoverable(questKey);
        }
    }
}
