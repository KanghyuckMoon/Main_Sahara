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
    
    public void RemoveDetectItem(GameObject _item)
    {
        detectItemList.Remove(_item);

        if (detectItemList.Count == 0)
        {
            clearEvnet?.Invoke();
            if (!isNotQuest)
            {
                QuestManager.Instance.ChangeQuestClear(questKey);
            }
        }
    }
}
