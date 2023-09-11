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
using UI.UtilManager;

namespace UI.Inventory
{
    public class InventoryGridSlotsPr
    {
        private InventoryGridSlotsView inventoryGridSlotsView;

        private Dictionary<ItemType, InventoryPanelUI> _invenPanelDic = new Dictionary<ItemType, InventoryPanelUI>();

        private ItemDescriptionPresenter descriptionPresenter; // 설명창 - 아이템에 커서 갖다댈시 활성화 
        private InvenItemUISO invenItemUISO;

        // 드래거 관련 데이터 
        private bool isDragger;
        private VisualElement target;
        private Action<SlotItemPresenter> startDragCallback;

        // 클릭 관련 데이터 
        private bool isClicker;
        private Action<ItemData> clickCallback;

        private int col = 4, row = 5;

        // 프로퍼티 
        public Dictionary<ItemType, InventoryPanelUI> InvenPanelDic => _invenPanelDic;

        public InventoryPanelUI CurInvenPanel =>
            InvenPanelDic[invenItemUISO.GetItemType(inventoryGridSlotsView.CurPanelType)];

        public ItemType CurItemType => invenItemUISO.GetItemType(inventoryGridSlotsView.CurPanelType);

        public int Col => col;
        public int Row => row;
        public ItemDescriptionPresenter DescriptionPr => descriptionPresenter;
        public InventoryGridSlotsView GridView => inventoryGridSlotsView;

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

        public InventoryPanelUI GetInvenPanel(ItemType _itemType)
        {
            return InvenPanelDic[_itemType];
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
            foreach (var _panel in _invenPanelDic)
            {
                _panel.Value.ClearDatas();
            }
        }

        /// <summary>
        /// 처음에 패널 활성화 초기화 
        /// </summary>
        private void InitActivePanel()
        {
            foreach (var _slot in InvenPanelDic)
            {
                if (InventoryGridSlotsView.InvenPanelElements.weapon_panel == invenItemUISO.GetItemUIType(_slot.Key))
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
            _invenPanelDic.Clear();
            foreach (var _v in Enum.GetValues(typeof(InventoryGridSlotsView.InvenPanelElements)))
            {
                InventoryGridSlotsView.InvenPanelElements _panelType = (InventoryGridSlotsView.InvenPanelElements)_v;

                _invenPanelDic.Add(invenItemUISO.GetItemType(_panelType),
                    new InventoryPanelUI(inventoryGridSlotsView.GetPanel(_panelType)));
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
                if (isDragger == true)
                    _slotPr.AddDragger(target, () => startDragCallback?.Invoke(_slotPr)); // 드래거 이벤트 
                if (isClicker == true)
                    _slotPr.AddClickEvent(() =>
                    {
                        clickCallback?.Invoke(_slotPr.ItemData);
                        // 사운드 재생
                        UIUtilManager.Instance.PlayUISound(UISoundType.Click);
                    });

                
                _slotPr.AddHoverEvent(() =>
                {
                    _slotPr.SlotItemView.ActiveBorder(true);
                    descriptionPresenter.SetItemData(_slotPr.ItemData, // 마우스 위에 둘시 설명창 
                        _slotPr.WorldPos, _slotPr.ItemSize);
                    // 사운드 재생
                    UIUtilManager.Instance.PlayUISound(UISoundType.Hover);
                });
                _slotPr.AddOutEvent(() =>
                {
                    _slotPr.SlotItemView.ActiveBorder(false);
                    descriptionPresenter.ActiveView(false);
                }); // 마우스 위에서 떠날시 설명창 비활성화

                //  더블 클릭 이벤트 추가 
                _slotPr.AddDoubleClicker(() => doubleClickEvent?.Invoke(_slotPr));
                
                _invenPanelDic[_iType].AddSlotView(_slotPr); // 패널에 슬롯 뷰 추가 
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
            this.startDragCallback = _callback;
            this.isDragger = true;
        }

        public void AddClickEvent(Action<ItemData> _callback)
        {
            this.isClicker = true;
            this.clickCallback = _callback;
        }

        private Action<SlotItemPresenter> doubleClickEvent = null; 
        public void AddDoubleClickEvent(Action<SlotItemPresenter> _callback)
        {
            doubleClickEvent = _callback; 
        }
    }
}