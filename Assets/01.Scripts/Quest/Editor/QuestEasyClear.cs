using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class QuestEasyClear : MonoBehaviour
    {
        [SerializeField]
        private string questKey;

        [ContextMenu("QuestClear")]
        public void QuestClear()
        {
            QuestManager.Instance.ChangeQuestClear(questKey);
        }
    }
}
