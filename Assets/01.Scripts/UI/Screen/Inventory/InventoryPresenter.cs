using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Inventory;
using UI.EventManage;
using UI.Base;
using UnityEngine.PlayerLoop;

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

     //   private 

        // 프로퍼티 
        public IUIController UIController { get; set; }
        
        private void Awake()
        {
            uiDocument ??= GetComponent<UIDocument>(); 

            inventoryView.InitUIDocument(uiDocument);
        }
        private void OnEnable()
        {
            inventoryView.Cashing();
            inventoryView.Init();

            inventoryView.AddButtonEvt(InventoryGridSlotsView.RadioButtons.weapon_button, (x) => AnimateSlot(InventoryView.Elements.quick_slot_panel, x));
            inventoryView.AddButtonEvt(InventoryGridSlotsView.RadioButtons.consumation_button, (x) => AnimateSlot(InventoryView.Elements.quick_slot_panel, x));
            inventoryView.AddButtonEvt(InventoryGridSlotsView.RadioButtons.skill_button, (x) => AnimateSlot(InventoryView.Elements.skill_equip_panel, x));
            inventoryView.AddButtonEvt(InventoryGridSlotsView.RadioButtons.armor_button, (x) => AnimateSlot(InventoryView.Elements.armor_equip_panel, x));
            inventoryView.AddButtonEvt(InventoryGridSlotsView.RadioButtons.accessories_button, (x) => AnimateSlot(InventoryView.Elements.accessoire_equip_panel, x));
            
            EventManager.Instance.StartListening(EventsType.UpdateInventoryUI, UpdateUI);
        }

        private void OnDisable()
        {
            EventManager.Instance.StopListening(EventsType.UpdateInventoryUI, UpdateUI);
        }

        void Start()
        {
            UpdateUI(); 
        }
        
        public bool ActiveView()
        {
            bool _isActive = inventoryView.ActiveScreen();
            inventoryCam.gameObject.SetActive(_isActive);

            EventManager.Instance.TriggerEvent(EventsType.UpdateQuickSlot);
            UpdateUI(); 
            return _isActive; 
        }

        public void ActiveView(bool _isActive)
        {
            inventoryCam.gameObject.SetActive(_isActive); // 인벤토리 활성화시에만 카메라 활성화 
            inventoryView.ActiveScreen(_isActive);
        }

        public void UpdateUI()
        {
            // 인벤토리 데이터 설정 
            List<ItemData> _itemList = InventoryManager.Instance.GetAllItem(); 
            foreach(var _itemData in _itemList)
            {
                this.inventoryView.UpdateInventoryUI(_itemData); 
            }

            // 퀵슬롯 데이터 설정 
            for (int i = 0; i <5; i++ )
            {
                ItemData _quickSlotData = InventoryManager.Instance.GetQuickSlotItem(i);
                this.inventoryView.UpdateQuickSlotUI(_quickSlotData, i);
            }

            ItemData _arrowData = InventoryManager.Instance.GetArrow();
            this.inventoryView.UpdateQuickSlotUI(_arrowData, 5);

        }

        private void AnimateSlot(InventoryView.Elements _type, bool _isActive)
        {
            StartCoroutine(AnimateCo(_type, _isActive));
        }

        private IEnumerator AnimateCo(InventoryView.Elements _type, bool _isActive)
        {
            WaitForSeconds _w = new WaitForSeconds(0.1f); 
            var _list = inventoryView.GetSlotList(_type, _isActive);
            foreach (var _slot in _list)
            {
                if (_isActive == false)
                {
                    //_slot.style.translate = new StyleTranslate(new Translate(500f,0));
                    _slot.AddToClassList("quick_slot_init");
                }
                else
                {
                    //_slot.style.translate = new StyleTranslate(new Translate(0, 0));
                    _slot.RemoveFromClassList("quick_slot_init");
                }
                yield return _w; 
            }
        }
    }

}

