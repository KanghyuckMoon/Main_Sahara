using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Dialogue;
using UI.EventAlarm;
using UI.Quest;
using UI.Inventory; 

namespace UI
{
    enum ScreenType
    {
        Inventory,
        Map,
        Dialogue,
        EventAlarm,
        Quest
    }

    public class ScreenUIController : MonoBehaviour
    {
        private InventoryPresenter inventoryPresenter;
        private MapPresenter mapPresenter;
        private DialoguePresenter dialoguePresenter;
        private EventAlarmPresenter eventAlarmPresenter;
        private QuestPresenter questPresenter; 

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
         //   screenDic.Add(ScreenType.Quest, questPresenter);
        }

        private void Start()
        {
            EnabledAllScreens();
        }

        private void Update()
        {
            UIInput();
        }

        private void InitScreenPresenters()
        {
            inventoryPresenter = GetComponentInChildren<InventoryPresenter>();
            mapPresenter = GetComponentInChildren<MapPresenter>();
            dialoguePresenter = GetComponentInChildren<DialoguePresenter>();
            eventAlarmPresenter = GetComponentInChildren<EventAlarmPresenter>();
            //questPresenter = GetComponentInChildren<QuestPresenter>(); 
        }
        private void UIInput()
        {
            if (isUIInput == false) return; 
            if (Input.GetKeyDown(KeyCode.I))
            {
                // 인벤토리 활성화 
                inventoryPresenter.ActiveView();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                // 맵 활성화
                mapPresenter.ActiveView();
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                // 퀘스트 활성화
                questPresenter.ActiveView(); 
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
    }
}
