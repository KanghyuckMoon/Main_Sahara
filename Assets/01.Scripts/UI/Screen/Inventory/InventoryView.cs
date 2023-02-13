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
            // 패널들만 넣어야해 
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
            // 패널 다음 인덱스부터 시작 
            right_content_panel = 7, // 퀵슬롯
            armor_equip_panel, // 장비 장착
            accessoire_equip_panel, // 장신구 장착
            skill_equip_panel, // 스킬 장착 

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
            //장비
            //소비
            //기타
        }

        enum ScrollViews
        {
            inventory_scroll_panel
        }
        #endregion
        private InvenItemUISO invenItemUISO;

        private List<VisualElement> inventoryPanelList = new List<VisualElement>();
        private Dictionary<ItemType, InventoryPanelUI> itemSlotDic = new Dictionary<ItemType, InventoryPanelUI>();

        private InvenPanelElements curPanelType; // 현재 활성화중인 패널 

        private SlotItemPresenter dragItemPresenter; // 드래그시 활성화될 뷰( 아이템 이미지 그대로 복사해서 커서 따라가는 )  
        private ItemDescriptionPresenter descriptionPresenter; // 설명창 - 아이템에 커서 갖다댈시 활성화 

        // 프로퍼티
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

            // 드래그 아이템 초기화 
            dragItemPresenter = new SlotItemPresenter(DragItem);
            dragItemPresenter.AddDropper(() => DropItem()); ;
            // 설명창 초기화 
            descriptionPresenter = new ItemDescriptionPresenter(Description); 

            // SO 불러오기 
            invenItemUISO = AddressablesManager.Instance.GetResource<InvenItemUISO>("InvenItemUISO");
            // 슬롯 생성 
            CreateAllSlots();
            // 인벤토리 패널 리스트에 넣고 weapon패널만 활성화 
            InitPanelList();
            // 버튼 이벤트 추가 
            AddButtonEvents();
            //    ItemTypeList.Add()

            InitEquipSlots();
        }

        /// <summary>
        /// 퀵 슬롯UI에 데이터 넣기 
        /// </summary>
        public void UpdateQuickSlotUI(ItemData _itemData, int _index)
        {
            itemSlotDic[ItemType.Weapon].SetEquipItemDataUI(_itemData, _index);
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
            InventoryPanelUI _ui = itemSlotDic[_itemData.itemType];
            if (_ui.slotItemViewList.Count <= _ui.index)
            {
                CreateRow(invenItemUISO.GetItemUIType(_itemData.itemType));
            }
            else if (_ui.slotItemViewList.Count > _row * _col) // 기본 인벤토리보다 더 많은데 아이템이 있는 것도 아니면 
            {
                //_ui.RemoveSlotView(); 
            }
            _ui.SetItemDataUI(_itemData);

        }

        /// <summary>
        /// 버튼 이벤트 추가
        /// </summary>
        private void AddButtonEvents()
        {
            // 패널 활성화 
            foreach (var _p in Enum.GetValues(typeof(InvenPanelElements)))
            {
                AddRadioBtnChangedEvent((int)_p, (x) => ActiveInventoryPanel((InvenPanelElements)_p, x));
            }
        }

        /// <summary>
        /// 인벤토리 패널 활성화or비활성화 시키기 
        /// </summary>
        /// <param name="_elementType"></param>
        private void ActiveInventoryPanel(InvenPanelElements _elementType, bool _isActive)
        {
            // 바뀌었으면 
            if (curPanelType != _elementType)
            {
                // 스크롤 초기화 
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
        /// 장착 슬롯 캐싱 초기화 
        /// </summary>
        private void InitEquipSlots()
        {
   //         equipInvenPanel = new EquipInventoryPanelUI();
            List<VisualElement> _list = GetVisualElement((int)Elements.right_content_panel).Query<VisualElement>(className: "quick_slot").ToList();
            for (int i = 0; i < _list.Count(); i++)
            {
     //           equipInvenPanel.AddEquipSlotView(new SlotItemView(_list[i]));
                // 버튼 2개에서 
                // 하나는 자기가 꺼지면 팬러끄고 
                // 하는ㄴ 자기가 키면 패널 키고 
                itemSlotDic[ItemType.Weapon].AddEquipSlotView(new SlotItemPresenter(_list[i],i));
            }
        }

        /// <summary>
        /// 인벤토리 패널 리스트에 넣기 (초기화)
        /// </summary>
        private void InitPanelList()
        {
            inventoryPanelList.Clear();

            // 인벤토리 패널 리스트에 추가 
            foreach (var _p in Enum.GetValues(typeof(InvenPanelElements)))
            {
                inventoryPanelList.Add(GetVisualElement((int)_p));
            }

            // weapon 패널만 활성화 후 나머진 비활성화 
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
        /// 모든 패널마다 슬롯 생성 
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
        /// 슬롯 한 줄 생성 
        /// </summary>
        private void CreateRow(InvenPanelElements _itemType)
        {
            for (int i = 0; i < _col; i++)
            {
                (VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(SlotItemView));
                ItemType _i = invenItemUISO.GetItemType(_itemType);

                SlotItemPresenter _slotPr = new SlotItemPresenter();
                // 슬롯 이벤트 등록 
                _slotPr.AddDragger(this.dragItemPresenter.Item, () => ClickItem(_slotPr));

                _slotPr.AddHoverEvent(() => descriptionPresenter.SetItemData(_slotPr.ItemData, // 마우스 위에 둘시 설명창 
                    new Vector2(_slotPr.WorldPos.x + _slotPr.ItemSize.x /2, _slotPr.WorldPos.y + _slotPr.ItemSize.y))); 
                _slotPr.AddOutEvent(() => descriptionPresenter.ActiveView(false)); // 마우스 위에서 떠날시 설명창 비활성화
                                                                                    
                itemSlotDic[_i].AddSlotView(_slotPr);
                SetParent(_itemType, _slotPr.Parent);
            }
        }

        /// <summary>
        /// 아이템 드래그 드랍시 밑에 스르롯 체크함수 
        /// </summary>
        private void DropItem()
        {
            // 떨어뜨린 곳이 슬롯이 있는지 체크 
            VisualElement _v = GetVisualElement((int)Elements.drag_item);

            IEnumerable<SlotItemPresenter> slots = itemSlotDic[invenItemUISO.GetItemType(curPanelType)].equipItemViewList.
                                                                     Where((x) => x.Item.worldBound.Overlaps(dragItemPresenter.Item.worldBound));

            // 슬롯에 드랍 했다면
            if (slots.Count() != 0)
            {
                // 가장 가깝게 드랍한 슬롯 
                SlotItemPresenter _closedSlot = slots.OrderBy(x => Vector2.Distance(x.Item.worldBound.position, dragItemPresenter.Item.worldBound.position)).First();

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
        /// <summary>
        /// 특정 인벤토리 창에 슬롯 생성 
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

