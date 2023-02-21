using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Inventory;
using System;
using UI.ConstructorManager;
using UI.Production;
using Utill.Addressable;

namespace UI.Inventory
{
    public class InventoryGridSlotsPr
    {
        private InventoryGridSlotsView inventoryGridSlotsView;

        private Dictionary<ItemType, InventoryPanelUI> itemSlotDic = new Dictionary<ItemType, InventoryPanelUI>();

        private ItemDescriptionPresenter descriptionPresenter; // ����â - �����ۿ� Ŀ�� ���ٴ�� Ȱ��ȭ 
        private InvenItemUISO invenItemUISO;

        // �巡�� ���� ������ 
        private bool isDragger;
        private VisualElement target;
        private Action<SlotItemPresenter> callback;

        private int col=4, row = 4;

        // ������Ƽ 
        public Dictionary<ItemType, InventoryPanelUI> ItemSlotDic => itemSlotDic;
        public InventoryPanelUI CurInvenPanel => ItemSlotDic[invenItemUISO.GetItemType(inventoryGridSlotsView.CurPanelType)];
        public int Col => col;
        public int Row => row; 

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
        /// ��� �гθ��� ���� ���� 
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
                if(isDragger == true)
                    _slotPr.AddDragger(target, () => callback?.Invoke(_slotPr)); // �巡�� �̺�Ʈ 

                _slotPr.AddHoverEvent(() => descriptionPresenter.SetItemData(_slotPr.ItemData, // ���콺 ���� �ѽ� ����â 
                   _slotPr.WorldPos, _slotPr.ItemSize));
                _slotPr.AddOutEvent(() => descriptionPresenter.ActiveView(false)); // ���콺 ������ ������ ����â ��Ȱ��ȭ

                itemSlotDic[_iType].AddSlotView(_slotPr); // �гο� ���� �� �߰� 
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
            this.callback = _callback;
            this.isDragger = true;
        }

    }
}

