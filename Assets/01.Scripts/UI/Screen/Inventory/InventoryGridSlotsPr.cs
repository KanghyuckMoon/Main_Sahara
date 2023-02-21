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

        private SlotItemPresenter dragItemPresenter; // 드래그시 활성화될 뷰( 아이템 이미지 그대로 복사해서 커서 따라가는 )  
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
        /// 모든 패널마다 슬롯 생성 
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
        /// 슬롯 한 줄 생성 
        /// </summary>
        //private void CreateRow(InvenPanelElements _itemType)
        //{
        //    (VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(SlotItemView));
        //    ItemType _i = invenItemUISO.GetItemType(_itemType);

        //    SlotItemPresenter _slotPr = new SlotItemPresenter();
        //    // 슬롯 이벤트 등록 
        //    // _slotPr.AddDragger(this.dragItemPresenter.Item, () => ClickItem(_slotPr)); // 드래거 이벤트 

        //    //_slotPr.AddHoverEvent(() => descriptionPresenter.SetItemData(_slotPr.ItemData, // 마우스 위에 둘시 설명창 
        //    //   _slotPr.WorldPos, _slotPr.ItemSize));
        //    //_slotPr.AddOutEvent(() => descriptionPresenter.ActiveView(false)); // 마우스 위에서 떠날시 설명창 비활성화

        //    //itemSlotDic[_i].AddSlotView(_slotPr); // 패널에 슬롯 뷰 추가 
        //    this.inventoryGridSlotsView.SetParent(_itemType, _slotPr.Parent);
        //}

    }
}

