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
    public class QuickSlotPresenter : MonoBehaviour
    {
        [SerializeField]
        private UIDocument uiDocument;

        [SerializeField]
        private QuickSlotView quickSlotView;

        private void OnEnable()
        {
            uiDocument = GetComponent<UIDocument>();
            quickSlotView.InitUIDocument(uiDocument);
            quickSlotView.Cashing();
            quickSlotView.Init();

            EventManager.Instance.StartListening(EventsType.UpdateQuickSlot, UpdateUI);
    
            UpdateUI(); 
        }

        private void OnDisable()
        {
            EventManager.Instance.StopListening(EventsType.UpdateQuickSlot, UpdateUI);
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
    }

}

