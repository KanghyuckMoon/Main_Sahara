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

        public T GetScreen<T>(PopupType popupType) where T : IPopup
        {
            IPopupPr popupPr = popupPrList.Find((x) => x.PopupType == popupType); 
            return (T)popupPr ;
        }
        public override void Awake()
        {
            base.Awake();
            Init(); 
        }

        public T CreatePopup<T>(PopupType _popupType, object _data = null,float _time = 2f) where  T : IPopup,new()
        {
            // 스크린 활성화 여부 체크후 활성화
            
            if (isInit == false)    
            {
                isInit = true; 
                Init();
            }
            T _popupGetItemPr = new T();

            try
            {
                var _screenPr = popupPrList.First(x => x.PopupType == _popupType);
                _screenPr.SetParent(_popupGetItemPr.Parent);
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

        private Stack<PopupGetNewitemPr> getNewItemStack = new Stack<PopupGetNewitemPr>();
        private Stack<ItemData> dataStack = new Stack<ItemData>(); 
        public void ReceiveEvent(string _sender, object _obj)
        {
            if (_sender is "InventoryManager")
            {
                ItemData _itemData = _obj as ItemData;;
                if (InventoryManager.Instance.ItemCheck(_itemData.key, 1) == false)
                {
                    //dataStack.Push(_itemData);
                    // 현재 활성화 중인 팝업있으면, 넘어가기 

                    //if (isCurPopupu == false)
                    //{
                     //   isCurPopupu = true; 
                      //  ItemData _data = dataStack.Pop(); 
                        var _popup  = CreatePopup<PopupGetNewitemPr>(PopupType.GetNewItem, _itemData,3f);
                      //  _popup.OnInactiveEvt += () =>
                       // {
                       //     _popup.OnInactiveEvt = null;
                       //     isCurPopupu = false;
                       // };
                   // }
                    
                   
                    // 트윈끝났을 때 알림 
                    
                }
                CreatePopup<PopupGetItemPr>(PopupType.GetItem, _itemData);
            }

            if (_sender is "QuestManager")
            {
                QuestData _questData = _obj as QuestData;
                CreatePopup<EventAlarmPr>(PopupType.EventAlarm, _questData,5f);
            }
        }

        private bool isCurPopupu = false; 
        private bool isExeCo = false; 
        private IEnumerator PopupWaitCo()
        {
            while (getNewItemStack.Count > 0)
            {
                
                yield return null;
                if (getNewItemStack.Count > 0)
                {
                    
                }
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
