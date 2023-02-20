using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Dialogue;
using UI.EventAlarm;
using UI.Quest;
using UI.Inventory;
using UI.Base;
using UI.Upgrade; 

namespace UI
{
    public enum ScreenType
    {
        Inventory,
        Map,
        Dialogue,
        EventAlarm,
        Quest,
        Upgrade,
    }

    public class ScreenUIController : MonoBehaviour
    {
        private InventoryPresenter inventoryPresenter;
        private MapPresenter mapPresenter;
        private DialoguePresenter dialoguePresenter;
        private EventAlarmPresenter eventAlarmPresenter;
        private QuestPresenter questPresenter;
        private UpgradePresenter upgradePresenter; 

        private Dictionary<ScreenType, IScreen> screenDic = new Dictionary<ScreenType, IScreen>();

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

            screenDic.Add(ScreenType.Inventory, inventoryPresenter);
            screenDic.Add(ScreenType.Map, mapPresenter);
            screenDic.Add(ScreenType.Dialogue, dialoguePresenter);
            screenDic.Add(ScreenType.EventAlarm, eventAlarmPresenter);
            screenDic.Add(ScreenType.Quest, questPresenter);
            screenDic.Add(ScreenType.Upgrade, upgradePresenter); 
        }

        private void Start()
        {
            EnabledAllScreens();
        }

        private void Update()
        {
            UIInput();
        }

        public T GetScreen<T>(ScreenType _screenType) where T : IScreen
        {
            IScreen _screen;
           if(screenDic.TryGetValue(_screenType, out _screen))
            {
                return (T)_screen;
            }
            Debug.LogError("screenDic을 확인해");
            return (T)_screen;

        }
        private void InitScreenPresenters()
        {
            inventoryPresenter = GetComponentInChildren<InventoryPresenter>();
            mapPresenter = GetComponentInChildren<MapPresenter>();
            dialoguePresenter = GetComponentInChildren<DialoguePresenter>();
            eventAlarmPresenter = GetComponentInChildren<EventAlarmPresenter>();
            questPresenter = GetComponentInChildren<QuestPresenter>();
            upgradePresenter = GetComponentInChildren<UpgradePresenter>(); 
        }
        private void UIInput()
        {
            if (isUIInput == false) return; 
            if (Input.GetKeyDown(KeyCode.I))
            {
                // 인벤토리 활성화 
                ActiveCursor(inventoryPresenter.ActiveView());
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                // 맵 활성화
                mapPresenter.ActiveView();
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                // 퀘스트 활성화
                ActiveCursor(questPresenter.ActiveView());
            }
            // 임시
            if (Input.GetKeyDown(KeyCode.U))
            {
                //  활성화
                ActiveCursor(upgradePresenter.ActiveView());
            }

        }

        /// <summary>
        /// 모든 스크린 비활성화 
        /// </summary>
        private void EnabledAllScreens()
        {
            foreach(var v in screenDic)
            {
                screenDic[v.Key].ActiveView(false); 
            }
        }

        private void ActiveCursor(bool _isActive)
        {
            if(_isActive == true)
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
