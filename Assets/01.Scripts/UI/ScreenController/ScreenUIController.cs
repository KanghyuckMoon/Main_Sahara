using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Dialogue;
using UI.EventAlarm;
using UI.Quest;
using UI.Inventory;
using UI.Base;
using UI.Upgrade;
using UI.Shop;
using System;
using UI.Save; 

namespace UI
{
    public class UIInputData
    {
        public KeyCode keyCode;
        public bool isCan; 

        public UIInputData(KeyCode _keyCode, bool _isCan)
        {
            this.keyCode = _keyCode;
            this.isCan = _isCan; 
        }
    }
    public class ScreenUIController : MonoBehaviour, IUIController
    {
        private InventoryPresenter inventoryPresenter;
        private MapPresenter mapPresenter;
        private DialoguePresenter dialoguePresenter;
        private EventAlarmPresenter eventAlarmPresenter;
        private QuestPresenter questPresenter;
        private UpgradePresenter upgradePresenter;
        private ShopPresenter shopPresenter;
        private SaveLoadPresenter saveLoadPresenter; 

        private Dictionary<ScreenType, IScreen> screenDic = new Dictionary<ScreenType, IScreen>();
        private Dictionary<UIInputData, Action> inputDic = new Dictionary<UIInputData, Action>();
        [SerializeField]
        private bool isUIInput = true;
        // 프로퍼티 
        public InventoryPresenter InventoryPresenter => inventoryPresenter;
        public MapPresenter MapPresenter => mapPresenter;
        public bool lsUIInput
        {
            get => isUIInput;
            set => isUIInput = value;
        }
        private void Awake()
        {
            InitScreenPresenters();

            //screenDic.Add(ScreenType.Inventory, inventoryPresenter);
            screenDic.Add(ScreenType.Map, mapPresenter);
        //    screenDic.Add(ScreenType.Dialogue, dialoguePresenter);
        //    screenDic.Add(ScreenType.EventAlarm, eventAlarmPresenter);
        //    screenDic.Add(ScreenType.Quest, questPresenter);
        //    screenDic.Add(ScreenType.Upgrade, upgradePresenter);
        //    screenDic.Add(ScreenType.Shop, shopPresenter);
        //    screenDic.Add(ScreenType.Save, saveLoadPresenter);
        }

        private void Start()
        {
            EnabledAllScreens();
            SetInputEvent(); 
        }

        private void Update()
        {
            UIInput();
        }

        /// <summary>
        /// 특정 스크린 가져오기 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_screenType"></param>
        /// <returns></returns>
        public T GetScreen<T>(ScreenType _screenType) where T : IScreen
        {
            IScreen _screen;
            if (screenDic.TryGetValue(_screenType, out _screen))
            {
                return (T)_screen;
            }
            Debug.LogError("screenDic을 확인해");
            return (T)_screen;
        }

        /// <summary>
        /// 특정 스크린 활성화 비활성화 
        /// </summary>
        /// <param name="_screenType"></param>
        /// <param name="_isActive"></param>
        public void ActiveScreen(ScreenType _screenType, bool _isActive)
        {
            IScreen _screen;
            if (screenDic.TryGetValue(_screenType, out _screen))
            {
                _screen.ActiveView(_isActive);
            }
        }
        private void InitScreenPresenters()
        {
            //inventoryPresenter = GetComponentInChildren<InventoryPresenter>();
            mapPresenter = GetComponentInChildren<MapPresenter>();
            //dialoguePresenter = GetComponentInChildren<DialoguePresenter>();
            //eventAlarmPresenter = GetComponentInChildren<EventAlarmPresenter>();
            //questPresenter = GetComponentInChildren<QuestPresenter>();
            //upgradePresenter = GetComponentInChildren<UpgradePresenter>();
            //shopPresenter = GetComponentInChildren<ShopPresenter>();
            //saveLoadPresenter = GetComponentInChildren<SaveLoadPresenter>();

            //// UIController 넣어주기 
            foreach (var _pr in screenDic)
            {
                _pr.Value.Init(this);
            }
        }

        private void SetInputEvent()
        {
            inputDic.Clear(); 

            inputDic.Add(new UIInputData(KeyCode.I, true), () =>
            {
                // 인벤토리 활성화 
                bool _isActive = inventoryPresenter.ActiveView();
                SetUIAndCursor(_isActive, KeyCode.I); 
            });
            inputDic.Add(new UIInputData(KeyCode.M, true), () =>
            {
                // 맵 활성화
                bool _isActive = mapPresenter.ActiveView();
                SetUI(_isActive, KeyCode.M);
            });
            inputDic.Add(new UIInputData(KeyCode.Q,true), () =>
            {
                // 퀘스트 활성화
                bool _isActive = questPresenter.ActiveView();
                SetUIAndCursor(_isActive, KeyCode.Q); 
            });
            inputDic.Add(new UIInputData(KeyCode.U, true), () =>
            {
                //  활성화
                bool _isActive = upgradePresenter.ActiveView();
                SetUIAndCursor(_isActive, KeyCode.U);
            });
            inputDic.Add(new UIInputData(KeyCode.O,true), () =>
            {
                //  활성화
                bool _isActive = shopPresenter.ActiveView();
                SetUIAndCursor(_isActive, KeyCode.O);
            });
            inputDic.Add(new UIInputData(KeyCode.L, true), () =>
            {
                //  활성화
                bool _isActive = saveLoadPresenter.ActiveView();
                SetUIAndCursor(_isActive, KeyCode.L);
            });
        }

        /// <summary>
        /// 스크린 활성화시 세팅 + 커서 활성화 
        /// </summary>
        private void SetUIAndCursor(bool _isActive, KeyCode _keyCode)
        {
            ActiveCursor(_isActive);
            SetTime(_isActive);
            SetKeyAble(_keyCode, _isActive);
        }
        /// <summary>
        /// 스크린 활성화시 세팅 
        /// </summary>
        private void SetUI(bool _isActive, KeyCode _keyCode)
        {
            SetTime(_isActive);
            SetKeyAble(_keyCode, _isActive);
        }

        /// <summary>
        /// 스크린 활성화시 세팅 
        /// </summary>
        //private void SetActiveUI(bool _isActive, KeyCode _keyCode)
        //{
        //    ActiveCursor(_isActive);
        //    SetTime(_isActive);
        //    SetKeyAble(_keyCode);
        //}

        private void UIInput()
        {
            if (isUIInput == false) return;
            foreach(var _v in inputDic)
            {
                if(_v.Key.isCan == true && Input.GetKeyDown(_v.Key.keyCode))
                {
                    _v.Value?.Invoke();
                }
            }


            //if (Input.GetKeyDown(KeyCode.I))
            //{
            //    // 인벤토리 활성화 
            //    ActiveCursor(inventoryPresenter.ActiveView());
            //}
            //if (Input.GetKeyDown(KeyCode.M))
            //{
            //    // 맵 활성화
            //    mapPresenter.ActiveView();
            //}
            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    // 퀘스트 활성화
            //    ActiveCursor(questPresenter.ActiveView());
            //}
            //// 임시
            //if (Input.GetKeyDown(KeyCode.U))
            //{
            //    //  활성화
            //    ActiveCursor(upgradePresenter.ActiveView());
            //}
            //if (Input.GetKeyDown(KeyCode.O))
            //{
            //    //  활성화
            //    ActiveCursor(shopPresenter.ActiveView());
            //}

        }

        /// <summary>
        /// 시간 정지 
        /// </summary>
        private void SetTime(bool _isActive)
        {
            //Time.timeScale = _isActive ? 0f : 1f; 
        }
        /// <summary>
        /// 모든 스크린 비활성화 
        /// </summary>
        private void EnabledAllScreens()
        {
            foreach (var v in screenDic)
            {
                screenDic[v.Key].ActiveView(false);
            }
        }

        private void SetKeyAble(KeyCode _keyCode, bool _isActive)
        {
            foreach (var _v in inputDic)
            {
                if(_v.Key.keyCode == _keyCode)
                {
                    _v.Key.isCan = true;
                    continue; 
                }
                _v.Key.isCan = _isActive? false : true; // 스크린 비활성화면 모두 키입력 가능하도록  
            }

        }

        private void ActiveCursor(bool _isActive)
        {
            if (_isActive == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                return;
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }
}
