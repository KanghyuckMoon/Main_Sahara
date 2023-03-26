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
                    popupParent = GameObject.FindWithTag("UIParent").transform.Find("PopupScreens");
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
                Init();
                isInit = true; 
            }
            T _popupGetItemPr = new T();
            
            popupPrList.First(x => x.PopupType == _popupType).SetParent(_popupGetItemPr.Parent);
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
                CreatePopup<EventAlarmPr>(PopupType.EventAlarm, _questData);
            }
        }

        private void CreateGetItemPopup(ItemData _itemData)
        {
            CreatePopup<PopupGetItemPr>(PopupType.GetItem, _itemData);
        }
        private void Init()
        {
            GameObject _parent = GameObject.FindWithTag("UIParent");
            if (_parent == null) return; 
            popupParent = _parent.transform.Find("PopupScreens");
            SetPresenters();

        }

        private void SetPresenters()
        {
            popupPrList.Clear();
            popupHudPr = popupParent.GetComponentInChildren<PopupHudPr>();
            eventAlarmScreenPr = popupParent.GetComponentInChildren<EventAlarmScreenPresenter>();
            interactionScreenPr = popupParent.GetComponentInChildren<InteractionScreenPr>(); 

            popupPrList.Add(popupHudPr);
            popupPrList.Add(eventAlarmScreenPr);
            popupPrList.Add(interactionScreenPr);
        }
        /*private Dictionary<PopupType, Type> popupChangeDic = new Dictionary<PopupType, Type>();
        private Dictionary<PopupType, IPopup> popupDic = new Dictionary<PopupType, IPopup>();


        public void Awake()
        {
            base.Awake();
            InitPopupDic(); 
        }
        public AbUI_Base CreatePopup(PopupType _popupType)
        {
            var _prod = UIConstructorManager.Instance.GetProductionUI(GetPopupType(_popupType));
            return _prod.Item2; 
        }

        public void AddToDic<T>(PopupType _popupType,IPopup _iPopup)
        {
            this.popupDic.Add(_popupType, _iPopup);
        }

        private void InitPopupDic()
        {
            popupChangeDic.Clear();

            popupChangeDic.Add(PopupType.GetItem, typeof(PopupGetItemPr));
        }


        private Type GetPopupType(PopupType _popupType)
        {
            if(popupChangeDic.TryGetValue(_popupType, out Type _type) == true)
            {
                return _type; 
            }
            return null; 
        }*/
    }

}
