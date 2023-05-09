using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Quest;
using UnityEngine;

public class DetectCountCheck : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> detectItemList = new List<GameObject>();

    [SerializeField] 
    private string questKey;
    
    public void RemoveDetectItem(GameObject _item)
    {
        detectItemList.Remove(_item);

        if (detectItemList.Count == 0)
        {
            QuestManager.Instance.ChangeQuestClear(questKey);
        }
    }
}
