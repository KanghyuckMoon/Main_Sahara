using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Dialogue;
//using UI.EventAlarm;
using UI.Quest;
using UI.Inventory;
using UI.Base;
using UI.Upgrade;
using UI.Shop;
using System;
using UI.Save;
using TimeManager;
using InputSystem;
using UI.UtilManager; 
using UI.Option;

namespace UI
{
    public class UIInputData
    {
        //public string keyStr; 
        public string keyStr;
        public bool isCan; 

        public UIInputData(string _keyCode, bool _isCan)
        {
            this.keyStr = _keyCode;
            this.isCan = _isCan; 
        }
    }
    public class ScreenUIController : MonoBehaviour, IUIController
    {
        enum Keys
        {
            QuestUI, 
            InventoryUI, 
            MapUI, 
            SaveLoadUI,
            ShopUI, 
            UpgradeUI, 
            OptionUI
        }



        private InventoryPresenter inventoryPresenter;
        private MapPresenter mapPresenter;
        private DialoguePresenter dialoguePresenter;
        //private EventAlarmPresenter eventAlarmPresenter;
        private QuestPresenter questPresenter;
        private UpgradePresenter upgradePresenter;
        private ShopPresenter shopPresenter;
        private SaveLoadPresenter saveLoadPresenter;
        private OptionPresenter _optionPresenter;
        
        private Dictionary<ScreenType, IScreen> screenDic = new Dictionary<ScreenType, IScreen>();
        private Dictionary<UIInputData, Action> inputDic = new Dictionary<UIInputData, Action>();
        [SerializeField]
        private bool isUIInput = true;
        // ������Ƽ 
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

            screenDic.Add(ScreenType.Inventory, inventoryPresenter);
            screenDic.Add(ScreenType.Map, mapPresenter);
            screenDic.Add(ScreenType.Dialogue, dialoguePresenter);
            //screenDic.Add(ScreenType.EventAlarm, eventAlarmPresenter);
            screenDic.Add(ScreenType.Quest, questPresenter);
            screenDic.Add(ScreenType.Upgrade, upgradePresenter);
            screenDic.Add(ScreenType.Shop, shopPresenter);
            /*screenDic.Add(ScreenType.Save, saveLoadPresenter);*/
            screenDic.Add(ScreenType.Option, _optionPresenter);
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
        /// Ư�� ��ũ�� �������� 
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
            Debug.LogError("screenDic�� Ȯ����");
            return (T)_screen;
        }

        /// <summary>
        /// Ư�� ��ũ�� Ȱ��ȭ ��Ȱ��ȭ 
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
            inventoryPresenter = GetComponentInChildren<InventoryPresenter>();
            mapPresenter = GetComponentInChildren<MapPresenter>();
            dialoguePresenter = GetComponentInChildren<DialoguePresenter>();
            //eventAlarmPresenter = GetComponentInChildren<EventAlarmPresenter>();
            questPresenter = GetComponentInChildren<QuestPresenter>();
            upgradePresenter = GetComponentInChildren<UpgradePresenter>();
            shopPresenter = GetComponentInChildren<ShopPresenter>();
            /*
            saveLoadPresenter = GetComponentInChildren<SaveLoadPresenter>();
            */
            _optionPresenter = GetComponentInChildren<OptionPresenter>(); 
            //// UIController �־��ֱ� 
            foreach (var _pr in screenDic)
            {
                _pr.Value.Init(this);
            }
        }

        private void SetInputEvent()
        {
            inputDic.Clear();

            inputDic.Add(new UIInputData(Get(Keys.InventoryUI), true), () =>
            {
                // �κ��丮 Ȱ��ȭ 
                bool _isActive = inventoryPresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.InventoryUI)); 
            });
            inputDic.Add(new UIInputData(Get(Keys.MapUI), true), () =>
            {
                // �� Ȱ��ȭ
                bool _isActive = mapPresenter.ActiveView();
                SetUI(_isActive, Get(Keys.MapUI));
            });
            inputDic.Add(new UIInputData(Get(Keys.QuestUI), true), () =>
            {
                // ����Ʈ Ȱ��ȭ
                bool _isActive = questPresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.QuestUI)); 
            });
            inputDic.Add(new UIInputData(Get(Keys.UpgradeUI), true), () =>
            {
                //  Ȱ��ȭ
                bool _isActive = upgradePresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.UpgradeUI));
            });
            inputDic.Add(new UIInputData(Get(Keys.ShopUI), true), () =>
            {
                //  Ȱ��ȭ
                bool _isActive = shopPresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.InventoryUI));
            });
            /*inputDic.Add(new UIInputData(Get(Keys.SaveLoadUI), true), () =>
            {
                //  Ȱ��ȭ
                bool _isActive = saveLoadPresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.SaveLoadUI));
            });*/
            inputDic.Add(new UIInputData(Get(Keys.OptionUI), true), () =>
            {
                //  Ȱ��ȭ
                bool _isActive = _optionPresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.OptionUI));
            });
        }

        /// <summary>
        /// ��ũ�� Ȱ��ȭ�� ���� + Ŀ�� Ȱ��ȭ 
        /// </summary>
        private void SetUIAndCursor(bool _isActive, string _keyCode)
        {
            ActiveCursor(_isActive);
            SetTime(_isActive);
            SetKeyAble(_keyCode, _isActive);
        }
        /// <summary>
        /// ��ũ�� Ȱ��ȭ�� ���� 
        /// </summary>
        private void SetUI(bool _isActive, string _keyCode)
        {
            SetTime(_isActive);
            SetKeyAble(_keyCode, _isActive);
        }

        /// <summary>
        /// ��ũ�� Ȱ��ȭ�� ���� 
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
                if(_v.Key.isCan == true && InputManager.Instance.CheckKey(_v.Key.keyStr))
                {
                    _v.Value?.Invoke();
                }
            }


            //if (Input.GetKeyDown(KeyCode.I))
            //{
            //    // �κ��丮 Ȱ��ȭ 
            //    ActiveCursor(inventoryPresenter.ActiveView());
            //}
            //if (Input.GetKeyDown(KeyCode.M))
            //{
            //    // �� Ȱ��ȭ
            //    mapPresenter.ActiveView();
            //}
            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    // ����Ʈ Ȱ��ȭ
            //    ActiveCursor(questPresenter.ActiveView());
            //}
            //// �ӽ�
            //if (Input.GetKeyDown(KeyCode.U))
            //{
            //    //  Ȱ��ȭ
            //    ActiveCursor(upgradePresenter.ActiveView());
            //}
            //if (Input.GetKeyDown(KeyCode.O))
            //{
            //    //  Ȱ��ȭ
            //    ActiveCursor(shopPresenter.ActiveView());
            //}

        }

        [SerializeField]
        private float testV = 0.1f; 
        [ContextMenu("�ð� ���� �׽�Ʈ")]
        public void TestSetTime()
        {
            StaticTime.UITime = testV; 
        //    SetTime(true); 
        }
        [ContextMenu("�ð� �������� �׽�Ʈ")]
        public void TestSetTime2()
        {
            StaticTime.UITime = 1f; 
          //  SetTime(false);
        }
        /// <summary>
        /// �ð� ���� 
        /// </summary>
        private void SetTime(bool _isActive)
        {
            StaticTime.UITime = _isActive ? 0f : 1f; 
            //Time.timeScale = _isActive ? 0f : 1f; 
        }
        /// <summary>
        /// ��� ��ũ�� ��Ȱ��ȭ 
        /// </summary>
        private void EnabledAllScreens()
        {
            foreach (var v in screenDic)
            {
                screenDic[v.Key].ActiveView(false);
            }
        }

        private void SetKeyAble(string _keyStr, bool _isActive)
        {
            foreach (var _v in inputDic)
            {
                if(_v.Key.keyStr == _keyStr)
                {
                    _v.Key.isCan = true;
                    continue; 
                }
                _v.Key.isCan = _isActive? false : true; // ��ũ�� ��Ȱ��ȭ�� ��� Ű�Է� �����ϵ���  
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

        private string Get(Keys _key)
        {
            return UIUtil.GetEnumStr(typeof(Keys), (int)_key);
        }
    }
}
