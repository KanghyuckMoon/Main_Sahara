using System;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UI.Popup;
using Utill.Pattern;
using Inventory;
using Quest;
using TimeManager;

public class PopupTest : MonoBehaviour
{
    public ItemDataSO ItemDataSo;
    public QuestDataSO QuestDataSo; 
    public QuestDataSO ClearQuestDataSo; 

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
            PopupUIManager.Instance.CreatePopup<EventAlarmPr>(PopupType.EventAlarm,
                new QuestData(QuestDataSo.questKey,QuestDataSo.nameKey, QuestDataSo.explanationKey
                    ,QuestDataSo.earlyQuestState,QuestDataSo.questConditionType, QuestDataSo.questCreateObjectSOList,QuestDataSo.linkQuestKeyList, QuestDataSo.isTalkQuest));
        }   
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            PopupUIManager.Instance.CreatePopup<EventAlarmPr>(PopupType.EventAlarm, new QuestData(ClearQuestDataSo.questKey,ClearQuestDataSo.nameKey, ClearQuestDataSo.explanationKey
                    ,ClearQuestDataSo.earlyQuestState,ClearQuestDataSo.questConditionType, ClearQuestDataSo.questCreateObjectSOList,ClearQuestDataSo.linkQuestKeyList, ClearQuestDataSo.isTalkQuest),4f);
        }   

        if (Input.GetKeyDown(KeyCode.N))
        {
            PopupUIManager.Instance.CreatePopup<InteractionPresenter>(PopupType.Interaction,
                new InteractionUIData { targetVec = trm.position, textKey = "ADSAFASFSAFSA" }, -1f);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ItemData itemData = ItemData.CopyItemDataSO(ItemDataSo);
            PopupUIManager.Instance.CreatePopup<ShopPopupPr>(PopupType.Shop,
                itemData);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ItemData itemData = ItemData.CopyItemDataSO(ItemDataSo);
            PopupUIManager.Instance.CreatePopup<PopupGetNewitemPr>(PopupType.GetNewItem,
                itemData,3f);
        }
       
    }
}