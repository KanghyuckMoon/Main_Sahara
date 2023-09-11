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

        private ItemDescriptionPresenter descriptionPresenter; // ����â - �����ۿ� Ŀ�� ���ٴ�� Ȱ��ȭ 
        private InvenItemUISO invenItemUISO;

        // �巡�� ���� ������ 
        private bool isDragger;
        private VisualElement target;
        private Action<SlotItemPresenter> startDragCallback;

        // Ŭ�� ���� ������ 
        private bool isClicker;
        private Action<ItemData> clickCallback;

        private int col = 4, row = 5;

        // ������Ƽ 
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

            // ����â �ʱ�ȭ 
            descriptionPresenter = new ItemDescriptionPresenter();
            descriptionPresenter.SetParent(_parent);
            descriptionPresenter.ActiveView(false);

            // SO �ҷ����� 
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
        /// �� �г��� ���� ������ ������ �ʱ�ȭ 
        /// </summary>
        public void ClearSlotDatas()
        {
            foreach (var _panel in _invenPanelDic)
            {
                _panel.Value.ClearDatas();
            }
        }

        /// <summary>
        /// ó���� �г� Ȱ��ȭ �ʱ�ȭ 
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
        /// ��� �гθ��� ���� ���� 
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
        /// ���� �� �� ���� 
        /// </summary>
        public void CreateRow(InventoryGridSlotsView.InvenPanelElements _itemType)
        {
            for (int i = 0; i < col; i++)
            {
                (VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(SlotItemView));
                ItemType _iType = invenItemUISO.GetItemType(_itemType);

                SlotItemPresenter _slotPr = new SlotItemPresenter();

                // ���� �̺�Ʈ ��� 
                if (isDragger == true)
                    _slotPr.AddDragger(target, () => startDragCallback?.Invoke(_slotPr)); // �巡�� �̺�Ʈ 
                if (isClicker == true)
                    _slotPr.AddClickEvent(() =>
                    {
                        clickCallback?.Invoke(_slotPr.ItemData);
                        // ���� ���
                        UIUtilManager.Instance.PlayUISound(UISoundType.Click);
                    });

                
                _slotPr.AddHoverEvent(() =>
                {
                    _slotPr.SlotItemView.ActiveBorder(true);
                    descriptionPresenter.SetItemData(_slotPr.ItemData, // ���콺 ���� �ѽ� ����â 
                        _slotPr.WorldPos, _slotPr.ItemSize);
                    // ���� ���
                    UIUtilManager.Instance.PlayUISound(UISoundType.Hover);
                });
                _slotPr.AddOutEvent(() =>
                {
                    _slotPr.SlotItemView.ActiveBorder(false);
                    descriptionPresenter.ActiveView(false);
                }); // ���콺 ������ ������ ����â ��Ȱ��ȭ

                //  ���� Ŭ�� �̺�Ʈ �߰� 
                _slotPr.AddDoubleClicker(() => doubleClickEvent?.Invoke(_slotPr));
                
                _invenPanelDic[_iType].AddSlotView(_slotPr); // �гο� ���� �� �߰� 
                this.inventoryGridSlotsView.SetParent(_itemType, _slotPr.Parent);
            }
        }


        /// <summary>
        /// �巡�� �߰� 
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