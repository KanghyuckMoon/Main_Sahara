using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Inventory; 

namespace UI.Inventory
{
    [Serializable]
    public class InventoryPresenter : MonoBehaviour, IScreen
    {
        [SerializeField]
        private UIDocument uiDocument;
        [SerializeField]
        private Camera inventoryCam; 

        [SerializeField]
        private InventoryView inventoryView;

        private void Awake()
        {
            uiDocument ??= GetComponent<UIDocument>(); 

            inventoryView.InitUIDocument(uiDocument);
        }
        private void OnEnable()
        {
            inventoryView.Cashing();
            inventoryView.Init();
        }
        void Start()
        {
            UpdateUI(); 
        }
        
        public void ActiveView()
        {
            bool _isActive = inventoryView.ActiveScreen();
            inventoryCam.gameObject.SetActive(_isActive);
        }

        public void ActiveView(bool _isActive)
        {
            inventoryCam.gameObject.SetActive(_isActive); // 인벤토리 활성화시에만 카메라 활성화 
            inventoryView.ActiveScreen(_isActive);
        }

        public void UpdateUI()
        {
            List<ItemData> _itemList = InventoryManager.Instance.GetWeaponAndConsumptionList(); 
            foreach(var _itemData in _itemList)
            {
                this.inventoryView.UpdateInventoryUI(_itemData); 
            }

            for (int i = 0; i <5; i++ )
            {
                ItemData _quickSlotData = InventoryManager.Instance.GetQuickSlotItem(i);
                this.inventoryView.UpdateQuickSlotUI(_quickSlotData, i); 
            }

        }
    }

}

