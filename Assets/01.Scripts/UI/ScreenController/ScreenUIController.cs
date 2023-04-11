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
using UI.EventManage;
using UI.Manager;
using UI.MapLiner;


namespace UI
{
    /*public enum Keys
    {
        QuestUI, 
        InventoryUI, 
        MapUI, 
        SaveLoadUI,
        ShopUI, 
        UpgradeUI, 
        OptionUI,
            
        // ����� �Է� X 
        BuyUI = 100, 
        SellUI, 
        SmithUI, 
    }*/
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
        private InventoryPresenter inventoryPresenter;
        private MapPresenter mapPresenter;
        private DialoguePresenter dialoguePresenter;
        private QuestPresenter questPresenter;
        private UpgradePresenter upgradePresenter;
        private ShopPresenter shopPresenter;
        private SaveLoadPresenter saveLoadPresenter;
        private OptionPresenter _optionPresenter;
        
        private Dictionary<ScreenType, IScreen> screenDic = new Dictionary<ScreenType, IScreen>();
        private Dictionary<UIInputData, Action> inputDic = new Dictionary<UIInputData, Action>(); // ����� Ű �Է����� ��ũ�� Ȱ��ȭ
        private Dictionary<Keys, Action> notInputDic = new Dictionary<Keys, Action>(); // ��ȭ���� ������ ��ũ�� Ȱ��ȭ 

        private (Keys,IScreen) curActiveScreen; 
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
            SetNotInputEvent(); 
        }

        private void Start()
        {
            EnabledAllScreens();
            SetInputEvent(); 
        }

        private void Update()
        {
            UIInput();
            Debug.Log("Ŀ�� ��� ���� : "  + Cursor.lockState);
            Debug.Log("Ŀ�� ���̴� ���� : "+Cursor.visible);
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

        public void ActiveScreen(Keys _keyType)
        {
            if (notInputDic.TryGetValue(_keyType, out Action _action))
            {
                _action?.Invoke();
            }
        }
        
        private void InitScreenPresenters()
        {
            inventoryPresenter = GetComponentInChildren<InventoryPresenter>();
            mapPresenter = GetComponentInChildren<MapPresenter>();
            dialoguePresenter = GetComponentInChildren<DialoguePresenter>();
            questPresenter = GetComponentInChildren<QuestPresenter>();
            upgradePresenter = GetComponentInChildren<UpgradePresenter>();
            shopPresenter = GetComponentInChildren<ShopPresenter>();
            /*
            saveLoadPresenter = GetComponentInChildren<SaveLoadPresenter>();
            */
            _optionPresenter = GetComponentInChildren<OptionPresenter>(); 
            //// UIController �־��ֱ� 
           
            screenDic.Add(ScreenType.Inventory, inventoryPresenter);
            screenDic.Add(ScreenType.Map, mapPresenter);
            screenDic.Add(ScreenType.Dialogue, dialoguePresenter);
            //screenDic.Add(ScreenType.EventAlarm, eventAlarmPresenter);
            screenDic.Add(ScreenType.Quest, questPresenter);
            screenDic.Add(ScreenType.Upgrade, upgradePresenter);
            screenDic.Add(ScreenType.Shop, shopPresenter);
            /*screenDic.Add(ScreenType.Save, saveLoadPresenter);*/
            screenDic.Add(ScreenType.Option, _optionPresenter);
            
            foreach (var _pr in screenDic)
            {
                _pr.Value.Init(this);
            }
            
        }

        private void SetNotInputEvent()
        {
            notInputDic.Clear();
            
            notInputDic.Add(Keys.BuyUI, () => 
            {
                bool _isActive = shopPresenter.ActivetShop(ShopType.BuyShop);
                SetUIAndCursor(_isActive, Get(Keys.BuyUI));
                curActiveScreen = (Keys.BuyUI,shopPresenter); 
            });
            notInputDic.Add(Keys.SellUI, () => 
            {
                bool _isActive = shopPresenter.ActivetShop(ShopType.SellShop);
                SetUIAndCursor(_isActive, Get(Keys.SellUI));
                curActiveScreen = (Keys.SellUI,shopPresenter); 
            });
            notInputDic.Add(Keys.SmithUI, () => 
            {
                bool _isActive = upgradePresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.SmithUI));
                curActiveScreen = (Keys.SmithUI,upgradePresenter); 
            });
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
                LineCreateManager.Instance.ActvieParent(ScreenType.Upgrade, _isActive);
                UIManager.Instance.ActiveHud(! _isActive);
                mapPresenter.ActiveScreen(!_isActive);
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
            UIManager.Instance.ActiveCursor(_isActive);
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

            if (curActiveScreen.Item2 != null && Input.GetKeyDown(KeyCode.Escape))
            {
                SetUIAndCursor(false, Get(curActiveScreen.Item1));
                curActiveScreen.Item2.ActiveView(false);
                curActiveScreen.Item2 = null; 
                EventManager.Instance.TriggerEvent(EventsType.SetCanDialogue,false);
            }
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

        private string Get(Keys _key)
        {
            return UIUtil.GetEnumStr(typeof(Keys), (int)_key);
        }
    }
}
