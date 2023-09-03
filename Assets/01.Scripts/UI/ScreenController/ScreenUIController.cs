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
using Inventory;
using UI.UtilManager; 
using UI.Option;
using UI.EventManage;
using UI.Manager;
using UI.Canvas;
using UI.ActiveManager;
using CondinedModule;
using Module;
                                                                                                 
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
        private GameObject uiCam; 
        
        private InventoryPresenter inventoryPresenter;
        private MapPresenter mapPresenter;
        private DialoguePresenter dialoguePresenter;
        private QuestPresenter questPresenter;
        private UpgradePresenter upgradePresenter;
        private ShopPresenter shopPresenter;
        private SaveLoadPresenter saveLoadPresenter;
        private PausePresenter _pausePresenter;
        private OptionPresenter optionPresenter; 
        
        private Dictionary<ScreenType, IScreen> screenDic = new Dictionary<ScreenType, IScreen>();
        private Dictionary<UIInputData, Action> inputDic = new Dictionary<UIInputData, Action>(); // 사용자 키 입력으로 스크린 활성화
        private Dictionary<Keys, Action> notInputDic = new Dictionary<Keys, Action>(); // 대화같은 곳에서 스크린 활성화 

        private (Keys,IScreen) curActiveScreen;

        private Action<bool> screenCallback = null; 
        
        [SerializeField]
        private bool isUIInput = true;
        // 프로퍼티 
        public InventoryPresenter InventoryPresenter => inventoryPresenter;
        public MapPresenter MapPresenter => mapPresenter;

        private Player player; 
        private Player Player
        {
            get
            {
                if (player == null)
                {
                    if (UIManager.Instance.PlayerObject != null)
                    {
                        this.player = UIManager.Instance.PlayerObject.GetComponent<Player>();
                        if (player != null)
                        {
                            return player; 
                        }
                    }
                    
                    return null;
                }

                return player; 

            }
        }
        
        private void Awake()
        {
            uiCam = transform.parent.GetComponentInChildren<Camera>().gameObject; 
            uiCam.SetActive(false);

            if (UIManager.Instance.PlayerObject is null)
            {
                screenCallback = (x) =>
                {
                    UIManager.Instance.PlayerObject.GetComponent<Player>().SetInput(x);
                };    
            }
             
        }

        private void OnEnable()
        {
            Debug.Log("ONEnable");
            StartCoroutine(Init());
            EventManager.Instance.StartListening(EventsType.SetUIInputAndDialogue,(x) => ClearUI((bool)x));
            EventManager.Instance.StartListening(EventsType.SetUIInput,(x) => SetUIInput((bool)x));
        }

        private void SetUIInput(bool _isActive)
        {
            isUIInput = _isActive; 
        }
        private IEnumerator Init()
        {
            while (true)
            {
                if (Player == null)
                {
                    yield return null; 
                }    
                EventManager.Instance.StartListening(EventsType.SetPlayerCam, (x) => Player.SetInput((bool)x));
                yield break;
            }
            
        }

        private void OnDisable()
        {
            EventManager.Instance.StopListening(EventsType.SetPlayerCam, (x) => player.SetInput((bool)x));
            EventManager.Instance.StopListening(EventsType.SetUIInputAndDialogue,(x) =>ClearUI((bool)x));
            EventManager.Instance.StopListening(EventsType.SetUIInput,(x) => SetUIInput((bool)x));
        }

        private void Start()
        {
            InitScreenPresenters();
            SetNotInputEvent();

            EnabledAllScreens();
            SetInputEvent();
            isUIInput = UIManager.Instance.IsUIInput; 
        }

        private void Update()
        {
            UIInput();
        }

        /// <summary>
        /// 활성화중인 UI 비활성화 하고 
        /// </summary>
        private void ClearUI(bool _isActive)
        {
            UIManager.Instance.IsUIInput = _isActive; 
            isUIInput = _isActive;
            EnabledScreens(ScreenType.Dialogue);
        }
        private void ActiveUICam(bool _isActive)
        {
            this.uiCam.SetActive(_isActive);
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
            
            //saveLoadPresenter = GetComponentInChildren<SaveLoadPresenter>();
            
            _pausePresenter = GetComponentInChildren<PausePresenter>();
            //optionPresenter = GetComponentInChildren<OptionPresenter>();
            optionPresenter = OptionPresenter.Instance; 
            screenDic.Add(ScreenType.Inventory, inventoryPresenter);
            screenDic.Add(ScreenType.Map, mapPresenter);
            screenDic.Add(ScreenType.Dialogue, dialoguePresenter);
            //screenDic.Add(ScreenType.EventAlarm, eventAlarmPresenter);
            screenDic.Add(ScreenType.Quest, questPresenter);
            screenDic.Add(ScreenType.Upgrade, upgradePresenter);
            screenDic.Add(ScreenType.Shop, shopPresenter);
            //screenDic.Add(ScreenType.Save, saveLoadPresenter);
            screenDic.Add(ScreenType.Pause, _pausePresenter);
            screenDic.Add(ScreenType.Option, optionPresenter);
            

            //// UIController 넣어주기 
            foreach (var _pr in screenDic)
            {
                _pr.Value.Init(this);
            }
            
        }

        private void SetOnActiveEvt()
        {

            
        }
        private void SetNotInputEvent()
        {
            notInputDic.Clear();
            
            notInputDic.Add(Keys.BuyUI, () => 
            {
                bool _isActive = shopPresenter.ActivetShop(ShopType.BuyShop);
                SetUIAndCursor(_isActive, Get(Keys.BuyUI));
                curActiveScreen = (Keys.BuyUI,shopPresenter); 
                if(_isActive == false) curActiveScreen.Item2 = null; 
            });
            notInputDic.Add(Keys.SellUI, () => 
            {
                bool _isActive = shopPresenter.ActivetShop(ShopType.SellShop);
                SetUIAndCursor(_isActive, Get(Keys.SellUI));
                curActiveScreen = (Keys.SellUI,shopPresenter); 
                if(_isActive == false) curActiveScreen.Item2 = null; 
            });
            notInputDic.Add(Keys.SmithUI, () => 
            {
                bool _isActive = upgradePresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.SmithUI));
                curActiveScreen = (Keys.SmithUI,upgradePresenter);

                ActiveUpgrade(_isActive);
                if(_isActive == false) curActiveScreen.Item2 = null; 
            });
            
            notInputDic.Add(Keys.InventoryUI, () =>
            {
                // 인벤토리 활성화 
                bool _isActive = inventoryPresenter.ActiveView();   
                UIManager.Instance.ActiveHud(!_isActive);
                mapPresenter.Active(! _isActive);
                LineCreateManager.Instance.ActvieScreen(ScreenType.Inventory, _isActive);

                SetUIAndCursor(_isActive, Get(Keys.InventoryUI));
                curActiveScreen = (Keys.InventoryUI, inventoryPresenter);
                if(_isActive == false) curActiveScreen.Item2 = null; 
            });
            notInputDic.Add(Keys.MapUI, () =>
            {
                // 맵 활성화
                bool _isActive = mapPresenter.ActiveView();
                UIManager.Instance.ActiveHud(! _isActive);
                SetUIAndCursor(_isActive, Get(Keys.MapUI));
                curActiveScreen = (Keys.MapUI, mapPresenter);
                if(_isActive == false) curActiveScreen.Item2 = null; 
            });
            notInputDic.Add(Keys.QuestUI, () =>
            {
                // 퀘스트 활성화
                bool _isActive = questPresenter.ActiveView();
                questPresenter.UpdateUI();
                LineCreateManager.Instance.ActvieScreen(ScreenType.Quest, _isActive);
                UIManager.Instance.ActiveHud(! _isActive);
                mapPresenter.Active(! _isActive);
                SetUIAndCursor(_isActive, Get(Keys.QuestUI)); 
                curActiveScreen = (Keys.QuestUI, questPresenter);
                if(_isActive == false) curActiveScreen.Item2 = null; 
            });
            notInputDic.Add(Keys.PauseUI, () =>
            {
                //  활성화
                // 다른 끌 스크린 있으면 그것부터 비활성화 
                bool _isActive = _pausePresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.PauseUI));
                curActiveScreen = (Keys.PauseUI, _pausePresenter);
                if(_isActive == false) curActiveScreen.Item2 = null; 
            });
            notInputDic.Add(Keys.OptionUI, () =>
            {
                //  활성화
                bool _isActive = optionPresenter.ActiveView();
                curActiveScreen = (Keys.OptionUI, optionPresenter);
                if (_isActive == false) curActiveScreen.Item2 = null;
            });
            _pausePresenter.OnActiveScreen = () =>
            {
                //  활성화
                // 다른 끌 스크린 있으면 그것부터 비활성화 
                bool _isActive = _pausePresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.PauseUI));
                curActiveScreen = (Keys.PauseUI, _pausePresenter);
                if (_isActive == false) curActiveScreen.Item2 = null;
            };
            
            optionPresenter.OnActiveScreen = () =>
            {
                //  활성화
                // 다른 끌 스크린 있으면 그것부터 비활성화 
                bool _isActive = optionPresenter.ActiveView();
                //SetUIAndCursor(_isActive, Get(Keys.PauseUI));
                curActiveScreen = (Keys.OptionUI, optionPresenter);
                if (_isActive == false) curActiveScreen.Item2 = null;
            };
        }

        private void SetIsDialogue(bool _isActive)
        {
            if (_isActive == false)
            {
                dialoguePresenter.IsDialogue = false;
            }
        }
        private void ActiveUpgrade(bool _isActive)
        {
            LineCreateManager.Instance.ActvieScreen(ScreenType.Upgrade, _isActive);
            //UIManager.Instance.ActiveHud(! _isActive);
            mapPresenter.Active(! _isActive);

            (UIActiveManager.Instance as IUIManager).Execute(_isActive);
            
            InventoryManager.Instance.gameObject.SetActive(!_isActive);
        }
        private void SetInputEvent()
        {
            inputDic.Clear();

            inputDic.Add(new UIInputData(Get(Keys.InventoryUI), true), () =>
            {
                // 인벤토리 활성화 
                bool _isActive = inventoryPresenter.ActiveView();   
                UIManager.Instance.ActiveHud(!_isActive);
                mapPresenter.Active(! _isActive);
                LineCreateManager.Instance.ActvieScreen(ScreenType.Inventory, _isActive);

                SetUIAndCursor(_isActive, Get(Keys.InventoryUI));
                curActiveScreen = (Keys.InventoryUI, inventoryPresenter);
                if(_isActive == false) curActiveScreen.Item2 = null; 
            });
            inputDic.Add(new UIInputData(Get(Keys.MapUI), true), () =>
            {
                // 맵 활성화
                bool _isActive = mapPresenter.ActiveView();
                UIManager.Instance.ActiveHud(! _isActive);
                SetUIAndCursor(_isActive, Get(Keys.MapUI));
                curActiveScreen = (Keys.MapUI, mapPresenter);
                if(_isActive == false) curActiveScreen.Item2 = null; 
            });
            inputDic.Add(new UIInputData(Get(Keys.QuestUI), true), () =>
            {
                // 퀘스트 활성화
                bool _isActive = questPresenter.ActiveView();
                questPresenter.UpdateUI();
                LineCreateManager.Instance.ActvieScreen(ScreenType.Quest, _isActive);
                UIManager.Instance.ActiveHud(! _isActive);
                    mapPresenter.Active(! _isActive);
                SetUIAndCursor(_isActive, Get(Keys.QuestUI)); 
                curActiveScreen = (Keys.QuestUI, questPresenter);
                
                if(_isActive == false) curActiveScreen.Item2 = null; 

            });
            inputDic.Add(new UIInputData(Get(Keys.PauseUI), true), () =>
            {
                //  활성화
                // 다른 끌 스크린 있으면 그것부터 비활성화 
                bool _isActive = _pausePresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.PauseUI));
                curActiveScreen = (Keys.PauseUI, _pausePresenter);
                if(_isActive == false) curActiveScreen.Item2 = null; 
            });
            /*inputDic.Add(new UIInputData(Get(Keys.UpgradeUI), true), () =>
            {
                //  활성화
                bool _isActive = upgradePresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.UpgradeUI));
                LineCreateManager.Instance.ActvieScreen(ScreenType.Upgrade, _isActive);
                UIManager.Instance.ActiveHud(! _isActive);
                mapPresenter.Active(! _isActive);
                screenCallback?.Invoke(_isActive);
            });*/
            /*inputDic.Add(new UIInputData(Get(Keys.ShopUI), true), () =>
            {
                //  활성화
                bool _isActive = shopPresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.InventoryUI));
                
            });*/
            /*inputDic.Add(new UIInputData(Get(Keys.SaveLoadUI), true), () =>
            {Marker
                //  활성화
                bool _isActive = saveLoadPresenter.ActiveView();
                SetUIAndCursor(_isActive, Get(Keys.SaveLoadUI));
            });*/

        }

        /// <summary>
        /// 스크린 활성화시 세팅 + 커서 활성화 
        /// </summary>
        private void SetUIAndCursor(bool _isActive, string _keyCode)
        {
            UIManager.Instance.ActiveCursor(_isActive);
            if (Player is not null)
            {
                Player.SetInput(_isActive);
            }
            // UIManager.Instance.
            SetTime(_isActive);
            SetKeyAble(_keyCode, _isActive);
            ActiveUICam(_isActive); 
        }

        private void UIInput()
        {
            if (UIManager.Instance.IsUIInput == false) return;
            foreach(var _v in inputDic)
            {
                // ESC 입력시 
                if (_v.Key.keyStr == Get(Keys.PauseUI)&& InputManager.Instance.CheckKey(_v.Key.keyStr))
                {
                        bool isCheck = CheckUndoScreen();
                       // if (isCheck == true)
                        //{
                         //   notInputDic[Get(_v.Key.keyStr)]?.Invoke();  
                       // }
                       if(isCheck == true)
                        return; 
                }
                if(_v.Key.isCan == true && InputManager.Instance.CheckKey(_v.Key.keyStr))   
                {
                    _v.Value?.Invoke();
                }
            }


        }

        private bool CheckUndoScreen()
        {
            if (curActiveScreen.Item2 != null)
            {
                notInputDic[curActiveScreen.Item1]?.Invoke();
                //SetUIAndCursor(false, Get(curActiveScreen.Item1));
                //curActiveScreen.Item2.ActiveView(false);
                curActiveScreen.Item2 = null; 
                // 흠.. 왜 꺼주었을까.. 
                EventManager.Instance.TriggerEvent(EventsType.SetCanDialogue,false);

                //if (curActiveScreen.Item1 == Keys.SmithUI)
                //{
                //    ActiveUpgrade(false);
                //    SetIsDialogue(false);
                //}

                return true; 
            }

            return false;
        }
        
        /// <summary>
        /// 시간 정지 
        /// </summary>
        private void SetTime(bool _isActive)
        {
            StaticTime.UITime = _isActive ? 0f : 1f; 
            //Time.timeScale = _isActive ? 0f : 1f; 
        }

        /// <summary>
        /// 모든 스크린 비활성화 
        /// </summary>
        private void EnabledScreens(ScreenType ignoreType)
        {
            foreach (var v in screenDic)
            {
                if(v.Key ==ignoreType) continue;;
                screenDic[v.Key].ActiveView(false);
            }
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

        private void SetKeyAble(string _keyStr, bool _isActive)
        {
            foreach (var _v in inputDic)
            {
                if(_v.Key.keyStr == _keyStr)
                {
                    _v.Key.isCan = true;
                    continue; 
                }
                _v.Key.isCan = _isActive? false : true; // 스크린 비활성화면 모두 키입력 가능하도록  
            }

        }

        private string Get(Keys _key)
        {
            return UIUtil.GetEnumStr(typeof(Keys), (int)_key);
        }

        private Keys Get(string _keyStr)
        {
            return UIUtil.GetEnumStr<Keys>(_keyStr); 
        }
    }
}
