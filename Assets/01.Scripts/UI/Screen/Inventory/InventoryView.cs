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
            drag_item,
            armor_equip_panel, // ��� ����
            accessoire_equip_panel, // ��ű� ����
            skill_equip_panel, // ��ų ���� 
           

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

        private SlotItemView dragItemView; // �巡�׾� ����� Ȱ��ȭ�� �� 

        // ������Ƽ
        private VisualElement DragItem => GetVisualElement((int)Elements.drag_item);
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
            dragItemView = new SlotItemView();
            VisualElement _v = GetVisualElement((int)Elements.drag_item); 
            dragItemView.InitUIParent(_v);
            dragItemView.Cashing();
            dragItemView.Init();
            dragItemView.AddDropper(() => DropItem()); ;

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
            }else if(_ui.slotItemViewList.Count > _row * _col) // �⺻ �κ��丮���� �� ������ �������� �ִ� �͵� �ƴϸ� 
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
            foreach(var _p in Enum.GetValues(typeof(InvenPanelElements)))
            {
                AddRadioBtnChangedEvent((int)_p, (x) => ActiveInventoryPanel((InvenPanelElements)_p,x));
            }
        }

        /// <summary>
        /// �κ��丮 �г� Ȱ��ȭor��Ȱ��ȭ ��Ű�� 
        /// </summary>
        /// <param name="_elementType"></param>
        private void ActiveInventoryPanel(InvenPanelElements _elementType,bool _isActive)
        {
            // �ٲ������ 
            if(curPanelType != _elementType)
            {
                // ��ũ�� �ʱ�ȭ 
                curPanelType = _elementType;
                GetScrollView((int)ScrollViews.inventory_scroll_panel).scrollOffset = Vector2.zero; 
            }
            if(_elementType == InvenPanelElements.weapon_panel || _elementType == InvenPanelElements.consumation_panel)
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

        private void InitEquipSlots()
        {
            List<VisualElement> _list = GetVisualElement((int)Elements.right_content_panel).Query<VisualElement>(className: "quick_slot").ToList();
            for (int i = 0; i < _list.Count(); i++)
            {
                itemSlotDic[ItemType.Weapon].AddEquipSlotView(new SlotItemView(_list[i],i)); 
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

                SlotItemView _slotView = _v.Item2 as SlotItemView;
                _slotView.AddDragger(dragItemView.Item, () => ClickItem(_slotView)); 
                itemSlotDic[_i].AddSlotView(_v.Item2 as SlotItemView);
                SetParent(_itemType,_v.Item1); 
            }
        }

        /// <summary>
        /// ������ �巡�� ����� �ؿ� ������ üũ�Լ� 
        /// </summary>
        private void DropItem()
        {
            // ����߸� ���� ������ �ִ��� üũ 
            VisualElement _v = GetVisualElement((int)Elements.drag_item);

           IEnumerable<SlotItemView> slots = itemSlotDic[invenItemUISO.GetItemType(curPanelType)].equipItemViewList.
                                                                    Where((x) => x.Item.worldBound.Overlaps(dragItemView.Item.worldBound));
            
            // ���Կ� ��� �ߴٸ�
            if(slots.Count() != 0)
            {
                // ���� ������ ����� ���� 
                SlotItemView _closedSlot = slots.OrderBy(x => Vector2.Distance(x.Item.worldBound.position, dragItemView.Item.worldBound.position)).First();

                _closedSlot.SetSprite(dragItemView.ItemSprite);
                _closedSlot.IsStackable = dragItemView.IsStackable; 
                _closedSlot.SetText(dragItemView.ItemCount);

                // SO �����͵� 
                //InventoryManager.Instance.SetQuickSlotItem(_closedSlot,_closedSlot.Index);
            }
            else
            {

            }
            ActiveDragItem(false); 
        }

        private void ClickItem(SlotItemView _slotView)
        {
            //dragItemView �� Ŭ���� ������ ������ �Ѱ��ֱ� 
            dragItemView.SetSpriteAndText(_slotView.ItemSprite, _slotView.ItemCount);

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
        private void SetParent(InvenPanelElements _itemType,VisualElement _v)
        {
            GetVisualElement((int)_itemType).Add(_v); 
        }

     //   private void 
    }
}

