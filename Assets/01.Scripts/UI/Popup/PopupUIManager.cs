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
using TimeManager;
using UI.EventManage;
using UnityEditor.PackageManager;

namespace UI.Popup
{

    /*
     * stack 추가 자동으로 없어짐 or 없애기 이걸 만들면 되긴 해
     * 시간 정지 createPopup도 하나 만들고
     * 근데 stack을 통해 관리하는거 사실 모든 UI에 적용되는걸로 하나 만들긴 해야해
     * 시간되면 그렇게 하고
     * 그러면 Esc로 끈다고 했을 때
     * ScreenUICOntroller ESC 는 무시를 해줘야해 <- 일단 임시 방편으로 ( 전체 UIStack 만들기 전에 ) 
     */
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
        private PopupTutorialScreenPr popupTutorialScreenPr; 
        
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

        private Stack<IPopup> popupStack = new Stack<IPopup>();
        public T CreatePopup<T>(PopupType _popupType, object _data = null,float _time = 2f) where  T : IPopup,new()
        {
            // 스크린 활성화 여부 체크후 활성화
            
            if (isInit == false)    
            {
                isInit = true; 
                Init();
            }
            T _popupGetItemPr = new T();
            //popupStack.Push(_popupGetItemPr);
            //_popupGetItemPr.OnInactiveEvt = () => popupStack.Pop();
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

        private IPopup curStopPopup; 
        public T CreatePopupTimeStop<T>(PopupType _popupType, object _data = null) where  T : IPopup,new()
        {
            // 스크린 활성화 여부 체크후 활성화
            
            if (isInit == false)    
            {
                isInit = true; 
                Init();
            }
            StaticTime.UITime = 0f;
            T _popupGetItemPr = new T();
            curStopPopup = _popupGetItemPr;
            //popupStack.Push(_popupGetItemPr);
            //_popupGetItemPr.OnInactiveEvt = () => popupStack.Pop();
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
            
            _popupGetItemPr.ActiveTween();   
            _popupGetItemPr.SetData(_data);
    
            // UI 입력 멈추고
            //UIMan.
            EventManager.Instance.TriggerEvent(EventsType.SetUIInput, false);
            StartCoroutine(StayTimeStopPopupCo());
            return _popupGetItemPr; 
        }

        private IEnumerator StayTimeStopPopupCo()
        {
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown((KeyCode.Escape)))
                {
                    StaticTime.UITime = 1f;
                    curStopPopup.InActiveTween(); 
                    curStopPopup.Undo(); 
                    EventManager.Instance.TriggerEvent(EventsType.SetUIInput, true);
                    // UI 입력 멈춘거 풀고
                    // UI 닫고 
                }
            }
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
                Debug.Log("@@ 아이템 획득");
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
            popupTutorialScreenPr = PopupParent.GetComponentInChildren<PopupTutorialScreenPr>(); 
            
            popupPrList.Add(popupHudPr);
            popupPrList.Add(eventAlarmScreenPr);
            popupPrList.Add(interactionScreenPr);
            popupPrList.Add(shopPopupScreenPr);
            popupPrList.Add(getNewitemScreenPr); 
            popupPrList.Add(popupTutorialScreenPr); 
        }

    }

}
