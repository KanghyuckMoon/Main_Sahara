using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System;
using UI.ConstructorManager;
using UI.Production;
using Inventory;
using Utill.Addressable;

namespace UI.Inventory
{

    [Serializable]
    public class InventoryView : AbUI_Base
    {
        #region enum

        enum Elements
        {
            // 패널 다음 인덱스부터 시작 
            quick_slot_panel = 0, // 퀵슬롯
            armor_equip_panel, // 장비 장착
            accessoire_equip_panel, // 장신구 장착
            skill_equip_panel, // 스킬 장착 

            drag_item,
        }

        enum RadioButtonGroups
        {
            inventory_select_group
        }
        enum ScrollViews
        {
            inventory_scroll_panel
        }

        #endregion

        private InvenItemUISO invenItemUISO;

        private SlotItemPresenter dragItemPresenter; // 드래그시 활성화될 뷰( 아이템 이미지 그대로 복사해서 커서 따라가는 )  

        private InventoryGridSlotsPr inventoryGridSlotsPr; 
        // 프로퍼티
        private VisualElement DragItem => GetVisualElement((int)Elements.drag_item);
        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
            BindScrollViews(typeof(ScrollViews));
            Bind<RadioButtonGroup>(typeof(RadioButtonGroups));
        }

        public override void Init()
        {
            base.Init();

            // 드래그 아이템 초기화 
            dragItemPresenter = new SlotItemPresenter(DragItem);
            dragItemPresenter.AddDropper(() => DropItem());

            // 인벤토리 슬롯들 뷰 생성 
            inventoryGridSlotsPr = new InventoryGridSlotsPr(ParentElement);
            inventoryGridSlotsPr.AddDragger(dragItemPresenter.Item,ClickItem);
            // 슬롯 생성 
            inventoryGridSlotsPr.Init();

            // 버튼 이벤트 추가 
            inventoryGridSlotsPr.AddButtonEvent(InventoryGridSlotsView.RadioButtons.weapon_button, (x) => ShowVisualElement(GetVisualElement((int)Elements.quick_slot_panel), x));
            inventoryGridSlotsPr.AddButtonEvent(InventoryGridSlotsView.RadioButtons.consumation_button, (x) => ShowVisualElement(GetVisualElement((int)Elements.quick_slot_panel), x));
            inventoryGridSlotsPr.AddButtonEvent(InventoryGridSlotsView.RadioButtons.skill_button, (x) => ShowVisualElement(GetVisualElement((int)Elements.skill_equip_panel), x));
            inventoryGridSlotsPr.AddButtonEvent(InventoryGridSlotsView.RadioButtons.armor_button, (x) => ShowVisualElement(GetVisualElement((int)Elements.armor_equip_panel), x));
            inventoryGridSlotsPr.AddButtonEvent(InventoryGridSlotsView.RadioButtons.accessories_button, (x) => ShowVisualElement(GetVisualElement((int)Elements.accessoire_equip_panel), x));
             

            ActiveDragItem(false); 
            // SO 불러오기 
            invenItemUISO = AddressablesManager.Instance.GetResource<InvenItemUISO>("InvenItemUISO");
            //InitPanelList();

            // 장착 슬롯 초기화 
            InitEquipSlots();
        }

        public void ClearUI()
        {

        }

        /// <summary>
        /// 퀵 슬롯UI에 데이터 넣기 
        /// </summary>
        public void UpdateQuickSlotUI(ItemData _itemData, int _index)
        {
            inventoryGridSlotsPr.ItemSlotDic[ItemType.Weapon].SetEquipItemDataUI(_itemData, _index);
        }

        /// <summary>
        /// 인벤토리 슬롯UI에 데이터 넣기 
        /// </summary>
        /// <param name="_itemData"></param>
        public void UpdateInventoryUI(ItemData _itemData)
        {
            // 셀 수 있는건지 체크 
            // 타입 체크 
            // 슬롯 하나씩 가져와서 데이터 넣기 
            // 슬롯 개수 초과하면 한 줄 더 생성 
            // row 초과인데 데이터 없으면 삭제 

            // 슬롯에 순서대로 
            InventoryPanelUI _ui = inventoryGridSlotsPr.ItemSlotDic[_itemData.itemType];
            if (_ui.slotItemViewList.Count <= _ui.index)
            {
                inventoryGridSlotsPr.CreateRow(invenItemUISO.GetItemUIType(_itemData.itemType));
            }
            else if (_ui.slotItemViewList.Count > inventoryGridSlotsPr.Row * inventoryGridSlotsPr.Col) // 기본 인벤토리보다 더 많은데 아이템이 있는 것도 아니면 
            {
                //_ui.RemoveSlotView(); 
            }
            //  현재 남은 칸에 데이터 넣기 
            _ui.SetItemDataUI(_itemData);

        }

        /// <summary>
        /// 장착 슬롯 캐싱 초기화 
        /// </summary>
        private void InitEquipSlots()
        {
   //         equipInvenPanel = new EquipInventoryPanelUI();
            List<VisualElement> _list = GetVisualElement((int)Elements.quick_slot_panel).Query<VisualElement>(className: "quick_slot").ToList();
            for (int i = 0; i < _list.Count(); i++)
            {
                //           equipInvenPanel.AddEquipSlotView(new SlotItemView(_list[i]));
                // 버튼 2개에서 
                // 하나는 자기가 꺼지면 팬러끄고 
                // 하는ㄴ 자기가 키면 패널 키고 
                SlotItemPresenter _slotIPr = new SlotItemPresenter(_list[i], i);

                _slotIPr.AddHoverEvent(() => inventoryGridSlotsPr.DescriptionPr.SetItemData(_slotIPr.ItemData, // 마우스 위에 둘시 설명창 
                   _slotIPr.WorldPos, _slotIPr.ItemSize));
                _slotIPr.AddOutEvent(() => inventoryGridSlotsPr.DescriptionPr.ActiveView(false)); // 마우스 위에서 떠날시 설명창 비활성화

                inventoryGridSlotsPr.ItemSlotDic[ItemType.Weapon].AddEquipSlotView(_slotIPr);
            }
        }

        /// <summary>
        /// 인벤토리 패널 리스트에 넣기 (초기화)
        /// </summary>
        //private void InitPanelList()
        //{
        //    inventoryPanelList.Clear();

        //    // 인벤토리 패널 리스트에 추가 
        //    foreach (var _p in Enum.GetValues(typeof(InvenPanelElements)))
        //    {
        //        inventoryPanelList.Add(GetVisualElement((int)_p));
        //    }

        //    // weapon 패널만 활성화 후 나머진 비활성화 
        //    for (int i = 0; i < inventoryPanelList.Count; i++)
        //    {
        //        if (i == (int)InvenPanelElements.weapon_panel)
        //        {
        //            GetVisualElement(i).style.display = DisplayStyle.Flex;
        //            continue;
        //        }
        //        GetVisualElement(i).style.display = DisplayStyle.None;
        //    }
        //}


        /// <summary>
        /// 아이템 드래그 드랍시 밑에 스르롯 체크함수 
        /// </summary>
        private void DropItem()
        {
            // 떨어뜨린 곳이 슬롯이 있는지 체크 
            VisualElement _v = GetVisualElement((int)Elements.drag_item);

//            IEnumerable<SlotItemPresenter> slots = itemSlotDic[invenItemUISO.GetItemType(curPanelType)].equipItemViewList.
//                                                                     Where((x) => x.Item.worldBound.Overlaps(dragItemPresenter.Item.worldBound));

            IEnumerable<SlotItemPresenter> _slots = inventoryGridSlotsPr.CurInvenPanel.equipItemViewList.
                                                                    Where((x) => x.Item.worldBound.Overlaps(dragItemPresenter.Item.worldBound));

            // 슬롯에 드랍 했다면
            if (_slots.Count() != 0)
            {
                // 가장 가깝게 드랍한 슬롯 
                SlotItemPresenter _closedSlot = _slots.OrderBy(x => Vector2.Distance(x.Item.worldBound.position, dragItemPresenter.Item.worldBound.position)).First();

                _closedSlot.SetItemData(dragItemPresenter.ItemData); 

                // SO 데이터도 
                InventoryManager.Instance.SetQuickSlotItem(_closedSlot.ItemData,_closedSlot.Index);
            }
            else
            {

            }
            ActiveDragItem(false);
        }

        private void ClickItem(SlotItemPresenter _slotView)
        {
            //dragItemView 에 클릭한 슬롯의 아이템 넘겨주기 
            dragItemPresenter.SetItemData(_slotView.ItemData);

            ActiveDragItem(true);
        }
        private void ActiveDragItem(bool _isActive)
        {
            ShowVisualElement(GetVisualElement((int)Elements.drag_item), _isActive);
        }


    }
}

