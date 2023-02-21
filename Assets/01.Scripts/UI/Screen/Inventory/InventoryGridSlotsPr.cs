using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Inventory;
using System;
using UI.ConstructorManager; 

namespace UI.Inventory
{
    public class InventoryGridSlotsPr 
    {
        private InventoryGridSlotsView inventoryGridSlotsView;

        private Dictionary<ItemType, InventoryPanelUI> itemSlotDic = new Dictionary<ItemType, InventoryPanelUI>();

        private SlotItemPresenter dragItemPresenter; // �巡�׽� Ȱ��ȭ�� ��( ������ �̹��� �״�� �����ؼ� Ŀ�� ���󰡴� )  
        private InvenItemUISO invenItemUISO;

        private int col, row = 4; 
        public InventoryGridSlotsPr(VisualElement _parent)
        {
            this.inventoryGridSlotsView = new InventoryGridSlotsView();
            inventoryGridSlotsView.InitUIParent(_parent);
            inventoryGridSlotsView.Cashing();
            inventoryGridSlotsView.Init(); 
        }

        /// <summary>
        /// ��� �гθ��� ���� ���� 
        /// </summary>
        private void CreateAllSlots()
        {
            itemSlotDic.Clear();
            foreach (var _v in Enum.GetValues(typeof(InventoryGridSlotsView.InvenPanelElements)))
            {
      //          itemSlotDic.Add(invenItemUISO.GetItemType((InvenPanelElements)_v), new InventoryPanelUI());
                for (int j = 0; j < row; j++)
                {
        //            CreateRow((InvenPanelElements)_v);
                }
            }
        }

        /// <summary>
        /// ���� �� �� ���� 
        /// </summary>
        //private void CreateRow(InvenPanelElements _itemType)
        //{
        //    (VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(SlotItemView));
        //    ItemType _i = invenItemUISO.GetItemType(_itemType);

        //    SlotItemPresenter _slotPr = new SlotItemPresenter();
        //    // ���� �̺�Ʈ ��� 
        //    // _slotPr.AddDragger(this.dragItemPresenter.Item, () => ClickItem(_slotPr)); // �巡�� �̺�Ʈ 

        //    //_slotPr.AddHoverEvent(() => descriptionPresenter.SetItemData(_slotPr.ItemData, // ���콺 ���� �ѽ� ����â 
        //    //   _slotPr.WorldPos, _slotPr.ItemSize));
        //    //_slotPr.AddOutEvent(() => descriptionPresenter.ActiveView(false)); // ���콺 ������ ������ ����â ��Ȱ��ȭ

        //    //itemSlotDic[_i].AddSlotView(_slotPr); // �гο� ���� �� �߰� 
        //    this.inventoryGridSlotsView.SetParent(_itemType, _slotPr.Parent);
        //}

    }
}

