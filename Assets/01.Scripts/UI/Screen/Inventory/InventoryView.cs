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
        public enum InvenPanelElements
        {
            // �гε鸸 �־���� 
            weapon_panel,
            armor_panel,
            consumation_panel,
            skill_panel,
            accessories_panel,
            material_panel,
            valuable_panel
        }

        enum Elements
        {
            // �г� ���� �ε������� ���� 
            right_content_panel = 7, // ������
            armor_equip_panel, // ��� ����
            accessoire_equip_panel, // ��ű� ����
            skill_equip_panel, // ��ų ���� 

            drag_item,
            description_panel,
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

        private List<VisualElement> inventoryPanelList = new List<VisualElement>();
        private Dictionary<ItemType, InventoryPanelUI> itemSlotDic = new Dictionary<ItemType, InventoryPanelUI>();

        private InvenPanelElements curPanelType; // ���� Ȱ��ȭ���� �г� 

        private SlotItemPresenter dragItemPresenter; // �巡�׽� Ȱ��ȭ�� ��( ������ �̹��� �״�� �����ؼ� Ŀ�� ���󰡴� )  
        private ItemDescriptionPresenter descriptionPresenter; // ����â - �����ۿ� Ŀ�� ���ٴ�� Ȱ��ȭ 

        // ������Ƽ
        private VisualElement DragItem => GetVisualElement((int)Elements.drag_item);
        private VisualElement Description => GetVisualElement((int)Elements.description_panel); 
        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(InvenPanelElements));
            BindVisualElements(typeof(Elements));
            BindRadioButtons(typeof(RadioButtons));
            BindScrollViews(typeof(ScrollViews));
        }

        public override void Init()
        {
            base.Init();

            // �巡�� ������ �ʱ�ȭ 
            dragItemPresenter = new SlotItemPresenter(DragItem);
            dragItemPresenter.AddDropper(() => DropItem()); ;
            // ����â �ʱ�ȭ 
            descriptionPresenter = new ItemDescriptionPresenter(Description); 

            // SO �ҷ����� 
            invenItemUISO = AddressablesManager.Instance.GetResource<InvenItemUISO>("InvenItemUISO");
            // ���� ���� 
            CreateAllSlots();
            // �κ��丮 �г� ����Ʈ�� �ְ� weapon�гθ� Ȱ��ȭ 
            InitPanelList();
            // ��ư �̺�Ʈ �߰� 
            AddButtonEvents();
            //    ItemTypeList.Add()

            InitEquipSlots();
        }

        /// <summary>
        /// �� ����UI�� ������ �ֱ� 
        /// </summary>
        public void UpdateQuickSlotUI(ItemData _itemData, int _index)
        {
            itemSlotDic[ItemType.Weapon].SetEquipItemDataUI(_itemData, _index);
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
            InventoryPanelUI _ui = itemSlotDic[_itemData.itemType];
            if (_ui.slotItemViewList.Count <= _ui.index)
            {
                CreateRow(invenItemUISO.GetItemUIType(_itemData.itemType));
            }
            else if (_ui.slotItemViewList.Count > _row * _col) // �⺻ �κ��丮���� �� ������ �������� �ִ� �͵� �ƴϸ� 
            {
                //_ui.RemoveSlotView(); 
            }
            _ui.SetItemDataUI(_itemData);

        }

        /// <summary>
        /// ��ư �̺�Ʈ �߰�
        /// </summary>
        private void AddButtonEvents()
        {
            // �г� Ȱ��ȭ 
            foreach (var _p in Enum.GetValues(typeof(InvenPanelElements)))
            {
                AddRadioBtnChangedEvent((int)_p, (x) => ActiveInventoryPanel((InvenPanelElements)_p, x));
            }
        }

        /// <summary>
        /// �κ��丮 �г� Ȱ��ȭor��Ȱ��ȭ ��Ű�� 
        /// </summary>
        /// <param name="_elementType"></param>
        private void ActiveInventoryPanel(InvenPanelElements _elementType, bool _isActive)
        {
            // �ٲ������ 
            if (curPanelType != _elementType)
            {
                // ��ũ�� �ʱ�ȭ 
                curPanelType = _elementType;
                GetScrollView((int)ScrollViews.inventory_scroll_panel).scrollOffset = Vector2.zero;
            }
            if (_elementType == InvenPanelElements.weapon_panel || _elementType == InvenPanelElements.consumation_panel)
            {
                ShowVisualElement(GetVisualElement((int)Elements.right_content_panel), _isActive);
            }
            else if (_elementType == InvenPanelElements.skill_panel)
            {
                ShowVisualElement(GetVisualElement((int)Elements.skill_equip_panel), _isActive);
            }
            else if (_elementType == InvenPanelElements.armor_panel)
            {
                ShowVisualElement(GetVisualElement((int)Elements.armor_equip_panel), _isActive);
            }
            else if (_elementType == InvenPanelElements.accessories_panel)
            {
                ShowVisualElement(GetVisualElement((int)Elements.accessoire_equip_panel), _isActive);
            }

            VisualElement _v = GetVisualElement((int)_elementType);
            ShowVisualElement(_v, _isActive);
        }

 //       private EquipInventoryPanelUI equipInvenPanel;
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
                itemSlotDic[ItemType.Weapon].AddEquipSlotView(new SlotItemPresenter(_list[i],i));
            }
        }

        /// <summary>
        /// �κ��丮 �г� ����Ʈ�� �ֱ� (�ʱ�ȭ)
        /// </summary>
        private void InitPanelList()
        {
            inventoryPanelList.Clear();

            // �κ��丮 �г� ����Ʈ�� �߰� 
            foreach (var _p in Enum.GetValues(typeof(InvenPanelElements)))
            {
                inventoryPanelList.Add(GetVisualElement((int)_p));
            }

            // weapon �гθ� Ȱ��ȭ �� ������ ��Ȱ��ȭ 
            for (int i = 0; i < inventoryPanelList.Count; i++)
            {
                if (i == (int)InvenPanelElements.weapon_panel)
                {
                    GetVisualElement(i).style.display = DisplayStyle.Flex;
                    continue;
                }
                GetVisualElement(i).style.display = DisplayStyle.None;
            }
        }



        private int _row = 4, _col = 4;

        /// <summary>
        /// ��� �гθ��� ���� ���� 
        /// </summary>
        private void CreateAllSlots()
        {
            itemSlotDic.Clear();
            foreach (var _v in Enum.GetValues(typeof(InvenPanelElements)))
            {
                itemSlotDic.Add(invenItemUISO.GetItemType((InvenPanelElements)_v), new InventoryPanelUI());
                for (int j = 0; j < _row; j++)
                {
                    CreateRow((InvenPanelElements)_v);
                }
            }
        }

        /// <summary>
        /// ���� �� �� ���� 
        /// </summary>
        private void CreateRow(InvenPanelElements _itemType)
        {
            for (int i = 0; i < _col; i++)
            {
                (VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(SlotItemView));
                ItemType _i = invenItemUISO.GetItemType(_itemType);

                SlotItemPresenter _slotPr = new SlotItemPresenter();
                // ���� �̺�Ʈ ��� 
                _slotPr.AddDragger(this.dragItemPresenter.Item, () => ClickItem(_slotPr));

                _slotPr.AddHoverEvent(() => descriptionPresenter.SetItemData(_slotPr.ItemData, // ���콺 ���� �ѽ� ����â 
                    new Vector2(_slotPr.WorldPos.x + _slotPr.ItemSize.x /2, _slotPr.WorldPos.y + _slotPr.ItemSize.y))); 
                _slotPr.AddOutEvent(() => descriptionPresenter.ActiveView(false)); // ���콺 ������ ������ ����â ��Ȱ��ȭ
                                                                                    
                itemSlotDic[_i].AddSlotView(_slotPr);
                SetParent(_itemType, _slotPr.Parent);
            }
        }

        /// <summary>
        /// ������ �巡�� ����� �ؿ� ������ üũ�Լ� 
        /// </summary>
        private void DropItem()
        {
            // ����߸� ���� ������ �ִ��� üũ 
            VisualElement _v = GetVisualElement((int)Elements.drag_item);

            IEnumerable<SlotItemPresenter> slots = itemSlotDic[invenItemUISO.GetItemType(curPanelType)].equipItemViewList.
                                                                     Where((x) => x.Item.worldBound.Overlaps(dragItemPresenter.Item.worldBound));

            // ���Կ� ��� �ߴٸ�
            if (slots.Count() != 0)
            {
                // ���� ������ ����� ���� 
                SlotItemPresenter _closedSlot = slots.OrderBy(x => Vector2.Distance(x.Item.worldBound.position, dragItemPresenter.Item.worldBound.position)).First();

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
        /// <summary>
        /// Ư�� �κ��丮 â�� ���� ���� 
        /// </summary>
        /// <param name="_itemType"></param>
        /// <param name="_v"></param>
        private void SetParent(InvenPanelElements _itemType, VisualElement _v)
        {
            GetVisualElement((int)_itemType).Add(_v);
        }

        //   private void 
    }
}

