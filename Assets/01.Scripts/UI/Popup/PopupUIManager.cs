using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Pattern;
using UI.ConstructorManager;
using System;
using UI.Base;
using UI.Popup;
using UI.Production; 
using System.Linq;
using Inventory;
using UI.EventAlarm;
using UnityEditor;
using Quest;

namespace UI.Popup
{

    public delegate void PopupEventTransmit(string _sender, string _recipient, object _obj);

    public class PopupUIManager : MonoSingleton<PopupUIManager>
    {
        private PopupEventTransmit popupEventTransmit;
        
        private Transform popupParent;

        private PopupHudPr popupHudPr;
        private EventAlarmScreenPresenter eventAlarmScreenPr;
        private InteractionScreenPr interactionScreenPr;
        private ShopPopupScreenPr shopPopupScreenPr;
        private GetNewitemScreenPr getNewitemScreenPr;
        
        private List<IPopupPr> popupPrList = new List<IPopupPr>(); 

        // 프로퍼티 
        public PopupEventTransmit PopupEventTransmit
        {
            get => popupEventTransmit;
            set => popupEventTransmit = value; 
        }
        
        public Transform PopupParent
        {
            get
            {
                if (popupParent == null)
                {
                    GameObject _uiParent = GameObject.FindWithTag("UIParent");
                    if (_uiParent != null)
                    {
                        popupParent = _uiParent.transform.Find("PopupScreens");
                    }
                    SetPresenters(); 
                }

                return popupParent;
            }
        }

        private bool isInit = false; 
        public override void Awake()
        {
            base.Awake();
            Init(); 
        }

        public T CreatePopup<T>(PopupType _popupType, object _data = null,float _time = 2f) where  T : IPopup,new()
        {
            if (isInit == false)    
            {
                isInit = true; 
                Init();
            }
            T _popupGetItemPr = new T();

            try
            {
                popupPrList.First(x => x.PopupType == _popupType).SetParent(_popupGetItemPr.Parent);
            }
            catch (Exception e)
            {
                SetPresenters();
                Debug.LogError("Sequence contains no matching element : " + e.Message);
            }
             if (_time > 0f)
            {
                StartCoroutine(_popupGetItemPr.TimerCo(_time));
            }
            else
            {
                _popupGetItemPr.ActiveTween();   
            }
            _popupGetItemPr.SetData(_data);

            return _popupGetItemPr; 
        }

        public void ReceiveEvent(string _sender, object _obj)
        {
            if (_sender is "InventoryManager")
            {
                ItemData _itemData = _obj as ItemData;;
                CreatePopup<PopupGetItemPr>(PopupType.GetItem, _itemData);
            }

            if (_sender is "QuestManager")
            {
                QuestData _questData = _obj as QuestData;
                CreatePopup<EventAlarmPr>(PopupType.EventAlarm, _questData,5f);
            }
        }

        private void CreateGetItemPopup(ItemData _itemData)
        {
            CreatePopup<PopupGetItemPr>(PopupType.GetItem, _itemData);
        }
        private void Init()
        {
            GameObject _parent = GameObject.FindWithTag("UIParent");
            if (_parent == null)
            {
                isInit = false;
                return;
            } 
            popupParent = _parent.transform.Find("PopupScreens");
            SetPresenters();

        }

        private void SetPresenters()
        {
            popupPrList.Clear();
            popupHudPr = PopupParent.GetComponentInChildren<PopupHudPr>();
            eventAlarmScreenPr = PopupParent.GetComponentInChildren<EventAlarmScreenPresenter>();
            interactionScreenPr = PopupParent.GetComponentInChildren<InteractionScreenPr>(); 
            shopPopupScreenPr = PopupParent.GetComponentInChildren<ShopPopupScreenPr>();
            getNewitemScreenPr = PopupParent.GetComponentInChildren<GetNewitemScreenPr>(); 
            
            popupPrList.Add(popupHudPr);
            popupPrList.Add(eventAlarmScreenPr);
            popupPrList.Add(interactionScreenPr);
            popupPrList.Add(shopPopupScreenPr);
            popupPrList.Add(getNewitemScreenPr); 
        }

    }

}
