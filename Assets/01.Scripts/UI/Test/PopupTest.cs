using System;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UI.Popup;
using Utill.Pattern;
using Inventory;

public class PopupTest : MonoBehaviour
{
    public ItemDataSO ItemDataSo;
    public Transform trm; 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ItemData itemData = ItemData.CopyItemDataSO(ItemDataSo);
            PopupUIManager.Instance.CreatePopup<PopupGetItemPr>(PopupType.GetItem, itemData);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {  
            PopupUIManager.Instance.CreatePopup<EventAlarmPr>(PopupType.EventAlarm, ("위험에 처한 이를 구하라!","퀘스트 활성화"));
        }
        if (Input.GetKeyDown(KeyCode.N))
        {  
            PopupUIManager.Instance.CreatePopup<InteractionPresenter>(PopupType.Interaction, 
                new InteractionUIData{targetVec =  trm.position, textKey =  "ADSAFASFSAFSA"},-1f);
        }
    }
}