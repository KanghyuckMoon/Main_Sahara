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
            // �г� ���� �ε������� ���� 
            right_content_panel = 0, // ������
            armor_equip_panel, // ��� ����
            accessoire_equip_panel, // ��ű� ����
            skill_equip_panel, // ��ų ���� 

            drag_item,
        }

        enum RadioButtons
        {
            weapon_button,
            armor_button,
            consumation_button,
            skill_button,
            accessories_button,
            material_button,
            valuable_button
            //���
            //�Һ�
            //��Ÿ
        }

        enum ScrollViews
        {
            inventory_scroll_panel
        }

        #endregion

        private InvenItemUISO invenItemUISO;

        private SlotItemPresenter dragItemPresenter; // �巡�׽� Ȱ��ȭ�� ��( ������ �̹��� �״�� �����ؼ� Ŀ�� ���󰡴� )  

        private InventoryGridSlotsPr inventoryGridSlotsPr; 
        // ������Ƽ
        private VisualElement DragItem => GetVisualElement((int)Elements.drag_item);
        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
            BindRadioButtons(typeof(RadioButtons));
            BindScrollViews(typeof(ScrollViews));
        }

        public override void Init()
        {
            base.Init();

            // �巡�� ������ �ʱ�ȭ 
            dragItemPresenter = new SlotItemPresenter(DragItem);
            dragItemPresenter.AddDropper(() => DropItem());

            // �κ��丮 ���Ե� �� ���� 
            inventoryGridSlotsPr = new InventoryGridSlotsPr(ParentElement);
            // ���� ���� 
            inventoryGridSlotsPr.Init();
            inventoryGridSlotsPr.AddDragger(dragItemPresenter.Item,ClickItem); 


            ActiveDragItem(false); 
            // SO �ҷ����� 
            invenItemUISO = AddressablesManager.Instance.GetResource<InvenItemUISO>("InvenItemUISO");
            //InitPanelList();

            // ���� ���� �ʱ�ȭ 
            InitEquipSlots();
        }

        public void ClearUI()
        {

        }

        /// <summary>
        /// �� ����UI�� ������ �ֱ� 
        /// </summary>
        public void UpdateQuickSlotUI(ItemData _itemData, int _index)
        {
            inventoryGridSlotsPr.ItemSlotDic[ItemType.Weapon].SetEquipItemDataUI(_itemData, _index);
        }

        /// <summary>
        /// �κ��丮 ����UI�� ������ �ֱ� 
        /// </summary>
        /// <param name="_itemData"></param>
        public void UpdateInventoryUI(ItemData _itemData)
        {
            // �� �� �ִ°��� üũ 
            // Ÿ�� üũ 
            // ���� �ϳ��� �����ͼ� ������ �ֱ� 
            // ���� ���� �ʰ��ϸ� �� �� �� ���� 
            // row �ʰ��ε� ������ ������ ���� 

            // ���Կ� ������� 
            InventoryPanelUI _ui = inventoryGridSlotsPr.ItemSlotDic[_itemData.itemType];
            if (_ui.slotItemViewList.Count <= _ui.index)
            {
                inventoryGridSlotsPr.CreateRow(invenItemUISO.GetItemUIType(_itemData.itemType));
            }
            else if (_ui.slotItemViewList.Count > inventoryGridSlotsPr.Row * inventoryGridSlotsPr.Col) // �⺻ �κ��丮���� �� ������ �������� �ִ� �͵� �ƴϸ� 
            {
                //_ui.RemoveSlotView(); 
            }
            //  ���� ���� ĭ�� ������ �ֱ� 
            _ui.SetItemDataUI(_itemData);

        }

        /// <summary>
        /// ���� ���� ĳ�� �ʱ�ȭ 
        /// </summary>
        private void InitEquipSlots()
        {
   //         equipInvenPanel = new EquipInventoryPanelUI();
            List<VisualElement> _list = GetVisualElement((int)Elements.right_content_panel).Query<VisualElement>(className: "quick_slot").ToList();
            for (int i = 0; i < _list.Count(); i++)
            {
     //           equipInvenPanel.AddEquipSlotView(new SlotItemView(_list[i]));
                // ��ư 2������ 
                // �ϳ��� �ڱⰡ ������ �ҷ����� 
                // �ϴ¤� �ڱⰡ Ű�� �г� Ű�� 
                inventoryGridSlotsPr.ItemSlotDic[ItemType.Weapon].AddEquipSlotView(new SlotItemPresenter(_list[i],i));
            }
        }

        /// <summary>
        /// �κ��丮 �г� ����Ʈ�� �ֱ� (�ʱ�ȭ)
        /// </summary>
        //private void InitPanelList()
        //{
        //    inventoryPanelList.Clear();

        //    // �κ��丮 �г� ����Ʈ�� �߰� 
        //    foreach (var _p in Enum.GetValues(typeof(InvenPanelElements)))
        //    {
        //        inventoryPanelList.Add(GetVisualElement((int)_p));
        //    }

        //    // weapon �гθ� Ȱ��ȭ �� ������ ��Ȱ��ȭ 
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
        /// ������ �巡�� ����� �ؿ� ������ üũ�Լ� 
        /// </summary>
        private void DropItem()
        {
            // ����߸� ���� ������ �ִ��� üũ 
            VisualElement _v = GetVisualElement((int)Elements.drag_item);

//            IEnumerable<SlotItemPresenter> slots = itemSlotDic[invenItemUISO.GetItemType(curPanelType)].equipItemViewList.
//                                                                     Where((x) => x.Item.worldBound.Overlaps(dragItemPresenter.Item.worldBound));

            IEnumerable<SlotItemPresenter> _slots = inventoryGridSlotsPr.CurInvenPanel.equipItemViewList.
                                                                    Where((x) => x.Item.worldBound.Overlaps(dragItemPresenter.Item.worldBound));

            // ���Կ� ��� �ߴٸ�
            if (_slots.Count() != 0)
            {
                // ���� ������ ����� ���� 
                SlotItemPresenter _closedSlot = _slots.OrderBy(x => Vector2.Distance(x.Item.worldBound.position, dragItemPresenter.Item.worldBound.position)).First();

                _closedSlot.SetItemData(dragItemPresenter.ItemData); 

                // SO �����͵� 
                InventoryManager.Instance.SetQuickSlotItem(_closedSlot.ItemData,_closedSlot.Index);
            }
            else
            {

            }
            ActiveDragItem(false);
        }

        private void ClickItem(SlotItemPresenter _slotView)
        {
            //dragItemView �� Ŭ���� ������ ������ �Ѱ��ֱ� 
            dragItemPresenter.SetItemData(_slotView.ItemData);

            ActiveDragItem(true);
        }
        private void ActiveDragItem(bool _isActive)
        {
            ShowVisualElement(GetVisualElement((int)Elements.drag_item), _isActive);
        }

    }
}

