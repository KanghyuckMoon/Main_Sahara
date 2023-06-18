using System.Collections;
using System.Collections.Generic;
using CutScene;
using UnityEngine;
using Quest;
using Utill.Pattern;

namespace Map
{
    public class TornadoQuest : MonoBehaviour
    {
        [SerializeField] private TornadoQuestSO tornadoQuestSO;
        [SerializeField] private string questKey;
        
        public void AddTornadoCount()
        {
            if (tornadoQuestSO.tornadoCount >= 1)
            {
                QuestAdd();
            }
            else
            {
                tornadoQuestSO.tornadoCount++;
            }
        }

        private void QuestAdd()
        {
            QuestManager.Instance.ChangeQuestActive(questKey);
        }
    }
}
