using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using UnityEngine.UIElements;
using Module;
using Inventory;
using UI.EventManage; 

namespace UI
{
    [System.Serializable]
    public class QuickSlotPresenter : IUIFollower
    {
 //       [SerializeField]
//        private UIDocument uiDocument;

        [SerializeField]
        private QuickSlotView quickSlotView;

        // 프로퍼티 
        public UIDocument RootUIDocument { get; set ; }
       
        public void OnDisable()
        {
            EventManager.Instance.StopListening(EventsType.UpdateQuickSlot, UpdateUI);
        }
        
        public void Awake()
        {
            quickSlotView.InitUIDocument(RootUIDocument);
            quickSlotView.Cashing();
            quickSlotView.Init();

            EventManager.Instance.StartListening(EventsType.UpdateQuickSlot, UpdateUI);
    
            UpdateUI(); 
        }

        public void Start(object _data)
        {
        }

        public void UpdateUI()
        {
            for (int i = 0; i < quickSlotView.SlotList.Count; i++)
            {
                quickSlotView.SlotList[i].SetItemData(InventoryManager.Instance.GetQuickSlotItem(i));
            }
            quickSlotView.ArrowSlot.SetItemData(InventoryManager.Instance.GetArrow());

            quickSlotView.UpdateActiveEffect(); 
        }

        public void ActiveScreen(bool _isActive)
        {
            quickSlotView.ActiveScreen(_isActive);
        }
    }

}

