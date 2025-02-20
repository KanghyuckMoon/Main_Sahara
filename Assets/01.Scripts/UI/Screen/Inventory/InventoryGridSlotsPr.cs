using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Inventory;
using System;
using UI.ConstructorManager;
using UI.Production;
using Utill.Addressable;
using UI.Base;

namespace UI.Inventory
{
    public class InventoryGridSlotsPr
    {
        private InventoryGridSlotsView inventoryGridSlotsView;

        private Dictionary<ItemType, InventoryPanelUI> itemSlotDic = new Dictionary<ItemType, InventoryPanelUI>();

        private ItemDescriptionPresenter descriptionPresenter; // 설명창 - 아이템에 커서 갖다댈시 활성화 
        private InvenItemUISO invenItemUISO;

        // 드래거 관련 데이터 
        private bool isDragger;
        private VisualElement target;
        private Action<SlotItemPresenter> callback;

        private int col=4, row = 4;

        // 프로퍼티 
        public Dictionary<ItemType, InventoryPanelUI> ItemSlotDic => itemSlotDic;
        public InventoryPanelUI CurInvenPanel => ItemSlotDic[invenItemUISO.GetItemType(inventoryGridSlotsView.CurPanelType)];
        public int Col => col;
        public int Row => row;
        public ItemDescriptionPresenter DescriptionPr => descriptionPresenter;
        public InventoryGridSlotsPr(VisualElement _parent)
        {
            this.inventoryGridSlotsView = new InventoryGridSlotsView();
            inventoryGridSlotsView.InitUIParent(_parent);
            inventoryGridSlotsView.Cashing();
            inventoryGridSlotsView.Init();

            // 설명창 초기화 
            descriptionPresenter = new ItemDescriptionPresenter();
            descriptionPresenter.SetParent(_parent);
            descriptionPresenter.ActiveView(false);

            // SO 불러오기 
            invenItemUISO = AddressablesManager.Instance.GetResource<InvenItemUISO>("InvenItemUISO");

        }

        public void Init()
        {
            CreateAllSlots();
        }

        public void AddButtonEvent(InventoryGridSlotsView.RadioButtons _type, Action<bool> _callback)
        {
            inventoryGridSlotsView.AddButtonEvent(_type, _callback);
        }
        public void SetData()
        {

        }
        /// <summary>
        /// 각 패널의 슬롯 아이템 데이터 초기화 
        /// </summary>
        public void ClearSlotDatas()
        {
            foreach(var _panel in itemSlotDic)
            {
                _panel.Value.ClearDatas(); 
            }
        }

        /// <summary>
        /// 처음에 패널 활성화 초기화 
        /// </summary>
        private void InitActivePanel()
        {
            foreach(var _slot in ItemSlotDic)
            {
                if(InventoryGridSlotsView.InvenPanelElements.weapon_panel == invenItemUISO.GetItemUIType(_slot.Key))
                {
                    _slot.Value.Parent.style.display = DisplayStyle.Flex;
                    continue;
                }
                 _slot.Value.Parent.style.display = DisplayStyle.None;
            }
         
        }

        /// <summary>
        /// 모든 패널마다 슬롯 생성 
        /// </summary>
        private void CreateAllSlots()
        {
            itemSlotDic.Clear();
            foreach (var _v in Enum.GetValues(typeof(InventoryGridSlotsView.InvenPanelElements)))
            {
                InventoryGridSlotsView.InvenPanelElements _panelType = (InventoryGridSlotsView.InvenPanelElements)_v;
                
                itemSlotDic.Add(invenItemUISO.GetItemType(_panelType), new InventoryPanelUI(inventoryGridSlotsView.GetPanel(_panelType)));
                for (int j = 0; j < row; j++)
                {
                    CreateRow((InventoryGridSlotsView.InvenPanelElements)_v);
                }
            }
            InitActivePanel(); 
        }

        /// <summary>
        /// 슬롯 한 줄 생성 
        /// </summary>
        public void CreateRow(InventoryGridSlotsView.InvenPanelElements _itemType)
        {
            for (int i = 0; i < col; i++)
            {
                (VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(SlotItemView));
                ItemType _iType = invenItemUISO.GetItemType(_itemType);

                SlotItemPresenter _slotPr = new SlotItemPresenter();

                // 슬롯 이벤트 등록 
                if(isDragger == true)
                    _slotPr.AddDragger(target, () => callback?.Invoke(_slotPr)); // 드래거 이벤트 

                _slotPr.AddHoverEvent(() => descriptionPresenter.SetItemData(_slotPr.ItemData, // 마우스 위에 둘시 설명창 
                   _slotPr.WorldPos, _slotPr.ItemSize));
                _slotPr.AddOutEvent(() => descriptionPresenter.ActiveView(false)); // 마우스 위에서 떠날시 설명창 비활성화

                itemSlotDic[_iType].AddSlotView(_slotPr); // 패널에 슬롯 뷰 추가 
                this.inventoryGridSlotsView.SetParent(_itemType, _slotPr.Parent);
            }
        }


        /// <summary>
        /// 드래거 추가 
        /// </summary>
        /// <param name="_target"></param>
        /// <param name="_callback"></param>
        public void AddDragger(VisualElement _target, Action<SlotItemPresenter> _callback)
        {
            this.target = _target;
            this.callback = _callback;
            this.isDragger = true;
        }

    }
}

