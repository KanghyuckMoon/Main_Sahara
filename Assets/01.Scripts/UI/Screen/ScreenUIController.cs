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
                // �κ��丮 Ȱ��ȭ 
                inventoryPresenter.ActiveView();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                // �� Ȱ��ȭ
                mapPresenter.ActiveView();
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                // ����Ʈ Ȱ��ȭ
                questPresenter.ActiveView(); 
            }

        }

        /// <summary>
        /// ��� ��ũ�� ��Ȱ��ȭ 
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
