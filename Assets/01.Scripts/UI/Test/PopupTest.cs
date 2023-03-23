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
            PopupUIManager.Instance.CreatePopup<EventAlarmPr>(PopupType.EventAlarm, "temData");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {  
            PopupUIManager.Instance.CreatePopup<InteractionPresenter>(PopupType.Interaction, 
                new InteractionUIData{targetVec =  trm.position, textKey =  "ADSAFASFSAFSA"},-1f);
        }
    }
}