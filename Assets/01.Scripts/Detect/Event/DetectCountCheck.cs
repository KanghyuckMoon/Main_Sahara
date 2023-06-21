using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Quest;
using UnityEngine;
using UnityEngine.Events;

public class DetectCountCheck : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> detectItemList = new List<GameObject>();

    [SerializeField] 
    private string questKey;

    [SerializeField] private UnityEvent clearEvnet;
    
    [SerializeField] private bool isNotQuest;
    
    [SerializeField] private QuestState completeQuestState = QuestState.Clear;
    
    public void RemoveDetectItem(GameObject _item)
    {
        detectItemList.Remove(_item);

        if (detectItemList.Count == 0)
        {
            clearEvnet?.Invoke();
            if (!isNotQuest)
            {
                switch (completeQuestState)
                {
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
                }
            }
        }
    }
}
