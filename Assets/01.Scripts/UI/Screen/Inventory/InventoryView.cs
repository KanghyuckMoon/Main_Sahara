using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System;
using GoogleSpreadSheet;
using UI.ConstructorManager;
using UI.Production;
using Inventory;
using Utill.Addressable;
using UI.Base;
using UI.UtilManager;

using RadioButtons = UI.Inventory.InventoryGridSlotsView.RadioButtons;
namespace UI.Inventory
{

    [Serializable]
    public class InventoryView : AbUI_Base
    {
        #region enum

        public enum Elements
        {
            // 패널 다음 인덱스부터 시작 
            quick_slot_panel = 0, // 퀵슬롯
            armor_equip_panel, // 장비 장착
            accessoire_equip_panel, // 장신구 장착
            //skill_equip_panel, // 스킬 장착 

            drag_item,
            contents,
            select_weapon_image,
            accent_pattern
        }

        enum RadioButtonGroups
        {
            inventory_select_group
        }
        enum ScrollViews
        {
            inventory_scroll_panel
        }

        enum Labels
        {
            item_title_label,
            item_detail_label,
            title_label
        }

        #endregion

        private InvenItemUISO invenItemUISO;

        private SlotItemPresenter dragItemPresenter; // 드래그시 활성화될 뷰( 아이템 이미지 그대로 복사해서 커서 따라가는 )  

        private InventoryGridSlotsPr inventoryGridSlotsPr;
        private Dictionary<ItemType, Func<ItemData, int,bool>> slotCallbackDic = new Dictionary<ItemType, Func<ItemData, int,bool>>();
        private Dictionary<ItemType, Action<int>> equipSlotRemoveCallbackDic = new Dictionary<ItemType, Action<int>>();
        private Action<ItemData> callback = null; 
        
        // 프로퍼티
        public InventoryGridSlotsPr GridPr => inventoryGridSlotsPr;
        public VisualElement SelectImage => GetVisualElement((int)Elements.select_weapon_image); 
        private VisualElement DragItem => GetVisualElement((int)Elements.drag_item);
        
        
        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
            BindScrollViews(typeof(ScrollViews));
            BindLabels(typeof(Labels));
            BindRadioButtonGroups(typeof(RadioButtonGroups));
        }

        public override void Init()
        {
            base.Init();

            // 드래그 아이템 초기화 
            dragItemPresenter = new SlotItemPresenter(DragItem);
            dragItemPresenter.AddDropper(DropItem);

            // 인벤토리 슬롯들 뷰 생성 
            inventoryGridSlotsPr = new InventoryGridSlotsPr(GetVisualElement((int)Elements.contents));
            inventoryGridSlotsPr.AddDragger(dragItemPresenter.Item, 
            (x) =>
            {
                if (x.ItemData == null) return; 
                ClickItem(x);
                SetItemText(x.ItemData);
                callback?.Invoke(x.ItemData);
            });
            inventoryGridSlotsPr.AddDoubleClickEvent(() => { }
                // 장착 
                );
            //inventoryGridSlotsPr.AddClickEvent(SetItemText);
            // 슬롯 생성 
            inventoryGridSlotsPr.Init();

            ActiveDragItem(false);
            // SO 불러오기 
            invenItemUISO = AddressablesManager.Instance.GetResource<InvenItemUISO>("InvenItemUISO");
            //InitPanelList();

            // 장착 슬롯 초기화 
            InitEquipSlots();

            // 아이템 드랍시 실행할 함수 초기화 
            InitCallbackDic();
            UpdateEquipUI(); 
            
  
        }

        public override void ActiveScreen(bool _isActive)
        {
            base.ActiveScreen(_isActive);
            if (_isActive == true)
            {
                // 슬롯 초기화 
                inventoryGridSlotsPr.ClearSlotDatas();
            }
        }

        public override bool ActiveScreen()
        {
            bool _isActive = base.ActiveScreen(); 
            if(_isActive == true) 
            {
                // 슬롯 초기화 
                inventoryGridSlotsPr.ClearSlotDatas();
            }
            return _isActive; 
        }

        public void SetInventoryTitleName(RadioButtons _itemType)
        {
            string _titleName = ""; 
            switch (_itemType)
            {
                case RadioButtons.weapon_button:
                    _titleName = "인벤토리 : 무기";
                    break;
                case RadioButtons.consumation_button:
                    _titleName = "인벤토리 : 소비";
                    break;
                case RadioButtons.armor_button:
                    _titleName = "인벤토리 : 장비";
                    break;
                case RadioButtons.accessories_button:
                    _titleName = "인벤토리 : 소울";
                    break;
                case RadioButtons.material_button:
                    _titleName = "인벤토리 : 기타";
                    break;
                case RadioButtons.valuable_button:
                    _titleName = "인벤토리 : 귀중";
                    break;
                case RadioButtons.marker_button:
                    _titleName = "인벤토리 : 마커";
                    break;
            }

            GetLabel((int)Labels.title_label).text = _titleName; 

        }

        private void SetInventoryTItleName(ItemType _itemType)
        {
        }

        public void AddSlotClickEvent(Action<ItemData> _callback)
        {
            callback = _callback; 
        }
        /// <summary>
        /// 아이템 이름, 설명 텍스트 띄우기 
        /// </summary>
        /// <param name="_itemData"></param>
        public void SetItemText(ItemData _itemData)
        {
            if (_itemData == null)
            {
                GetLabel((int)Labels.item_title_label).text = string.Empty; 
                GetLabel((int)Labels.item_detail_label).text = string.Empty;
                return; 
            }

            string _nameStr =TextManager.Instance.GetText(_itemData.nameKey);
            string _detailStr = TextManager.Instance.GetText(_itemData.explanationKey);
            UIUtilManager.Instance.AnimateText(GetLabel((int)Labels.item_title_label), _nameStr);
            UIUtilManager.Instance.AnimateText(GetLabel((int)Labels.item_detail_label), _detailStr);
            //GetVisualEleme    nt((int)Elements.select_weapon_image)
            // 제목 텍스트 설정
            // 내용 텍스트 설정+
            // 무기 이미지 설정
        }

        public void ClearUI()
        {
        }

        public void AddButtonEvt(InventoryGridSlotsView.RadioButtons _btnType, Action<bool> _callback)
        {
            inventoryGridSlotsPr.AddButtonEvent(_btnType, (x)=> _callback?.Invoke(x));
            /*switch (_btnType)
            {
                case InventoryGridSlotsView.RadioButtons.weapon_button:
                    inventoryGridSlotsPr.AddButtonEvent(_btnType, (x) => _callback?.Invoke(x));
                    break;
                case Invent oryGridSlotsView.RadioButtons.armor_button:
                    inventoryGridSlotsPr.AddButtonEvent(_btnType, (x) => _callback?.Invoke(x));
                    break;
                case InventoryGridSlotsView.RadioButtons.consumation_button:
                    inventoryGridSlotsPr.AddButtonEvent(_btnType, (x) => _callback?.Invoke(x));
                    break;
                case InventoryGridSlotsView.RadioButtons.skill_button:
                    inventoryGridSlotsPr.AddButtonEvent(_btnType, (x) => _callback?.Invoke(x));
                    break;
                case InventoryGridSlotsView.RadioButtons.accessories_button:
                    inventoryGridSlotsPr.AddButtonEvent(_btnType, (x) => _callback?.Invoke(x));
                    break;
            }*/

        }
        
        /// <summary>
        /// 퀵 슬롯UI에 데이터 넣기 
        /// </summary>
        public void UpdateQuickSlotUI(ItemData _itemData, int _index)
        {
            inventoryGridSlotsPr.InvenPanelDic[ItemType.Weapon].SetEquipItemDataUI(_itemData, _index);
        }

        public void UpdateArrowSlotUI(ItemData _itemData, int _idx)
        {

            inventoryGridSlotsPr.InvenPanelDic[ItemType.Consumption].SetEquipItemDataUI(_itemData,_idx);
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
            if (_itemData.itemType is ItemType.Skill) return; 
            // 인벤토리 패널 UI 가져와 
            InventoryPanelUI _ui = inventoryGridSlotsPr.InvenPanelDic[_itemData.itemType];
            // 현재 패널 ui 슷롯 인덱스 체크 
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

        public List<VisualElement> GetSlotList(Elements _type, bool _isActive)
        {
            ShowVisualElement(GetVisualElement((int)_type), _isActive);

            List<VisualElement> _slotList = GetVisualElement((int)_type).Query<VisualElement>(className: "quick_slot_transition").ToList();
            return _slotList; 
        }
    

        /// <summary>
        /// 장착 슬롯 캐싱 초기화 
        /// </summary>
        private void InitEquipSlots()   
        {
   //         equipInvenPanel = new EquipInventoryPanelUI();
            List<VisualElement> _list = GetVisualElement((int)Elements.quick_slot_panel).Query<VisualElement>(className: "quick_slot_transition").ToList();
            for (int i = 0; i < _list.Count(); i++)
            {
                SlotItemPresenter _slotIPr = new SlotItemPresenter(_list[i], i);
                if(_list[i].name == "ArrowSlot")
                {
                    _slotIPr.SetSlotType(ItemType.Consumption); 
                    _slotIPr.SetEquipSlotType(new ItemType[]{ItemType.Weapon, ItemType.Consumption});
                    AddEquipSlotEvt(_slotIPr, ItemType.Consumption);
                }
                else
                {
                    _slotIPr.SetSlotType(ItemType.Weapon);
                    _slotIPr.SetEquipSlotType(new ItemType[]{ItemType.Weapon, ItemType.Consumption});
                    AddEquipSlotEvt(_slotIPr, ItemType.Weapon);
                }

            }
            List<VisualElement> _armorList = GetVisualElement((int)Elements.armor_equip_panel).Query<VisualElement>(className: "quick_slot_transition").ToList();
            AddEquipSlotsEvt(_armorList, ItemType.Equipment,new ItemType[]{ItemType.Equipment});
           // List<VisualElement> _skillList = GetVisualElement((int)Elements.skill_equip_panel).Query<VisualElement>(className: "quick_slot_transition").ToList();
            //AddEquipSlotsEvt(_skillList, ItemType.Skill);
            List<VisualElement> _accesList = GetVisualElement((int)Elements.accessoire_equip_panel).Query<VisualElement>(className: "quick_slot_transition").ToList();
            AddEquipSlotsEvt(_accesList, ItemType.Accessories,new ItemType[]{ItemType.Accessories});
        }

        private void AddEquipSlotsEvt(List<VisualElement> _list, ItemType _itemType,ItemType[] _equipTypeArr = null)
        {
            for (int i = 0; i < _list.Count() ; i++)
            {
                SlotItemPresenter _slotIPr = new SlotItemPresenter(_list[i], i);
                _slotIPr.SetSlotType(_itemType);
                _slotIPr.SetEquipSlotType(_equipTypeArr);

                AddEquipSlotEvt(_slotIPr, _itemType);
            }   
        }
        
        private void AddEquipSlotEvt(SlotItemPresenter _slotPr,ItemType _itemType)
        {
            _slotPr.AddHoverEvent(() => inventoryGridSlotsPr.DescriptionPr.SetItemData(_slotPr.ItemData, // 마우스 위에 둘시 설명창 
                _slotPr.WorldPos, _slotPr.ItemSize));
            _slotPr.AddOutEvent(() => inventoryGridSlotsPr.DescriptionPr.ActiveView(false)); // 마우스 위에서 떠날시 설명창 비활성화
            _slotPr.AddAltClicker(() =>
            {
                if (_slotPr.ItemData == null) return; 
                Debug.Log("더블클릭");
                //inventoryGridSlotsPr.InvenPanelDic[_itemType].RemoveEquipSlotView(_slotPr);
                equipSlotRemoveCallbackDic[_slotPr.ItemData.itemType]?.Invoke(_slotPr.Index);
                _slotPr.ClearData();

            });
            inventoryGridSlotsPr.InvenPanelDic[_itemType].AddEquipSlotView(_slotPr);
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
                                                                                                                                
        //    // weapon 패널만 활성화 후 나머진 비활            성화 
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
            
            // 무기, 소비템은 공유 
            List<SlotItemPresenter> _targetList = inventoryGridSlotsPr.CurInvenPanel.equipItemViewList; 
            if (inventoryGridSlotsPr.CurItemType is ItemType.Consumption)
                _targetList = inventoryGridSlotsPr.GetInvenPanel(ItemType.Weapon).equipItemViewList;
            
            IEnumerable<SlotItemPresenter> _slots =_targetList.
                                                                    Where((x) => x.Item.worldBound.Overlaps(dragItemPresenter.Item.worldBound));

            
            // 슬롯에 드랍 했다면
            if (_slots.Count() != 0)
            {
                
                // 가장 가깝게 드랍한 슬롯 
                SlotItemPresenter _closedSlot = _slots.OrderBy(x =>
                    Vector2.Distance(x.Item.worldBound.position, dragItemPresenter.Item.worldBound.position)).First();

                // 장착 성공 여부 ( 같은 타입의 아이템인지) 
                Nullable<bool> _isSuccessed = false; 
                foreach (var _type in _closedSlot.EquipTypeList)
                {
                     _isSuccessed =slotCallbackDic[_type]?.Invoke(dragItemPresenter.ItemData, _closedSlot.Index);
                     if (_isSuccessed == true) break; 
                }
                //_isSuccessed = slotCallbackDic[_closedSlot.SlotType]?.Invoke(dragItemPresenter.ItemData, _closedSlot.Index);
                   
                // SO 데이터도 설정
                //InventoryMana ger.Instance.SetQuickSlotItem(_closedSlot.ItemData, _closedSlot.Index);
                // 전체 UI 업데이트 
                if (_isSuccessed == true)
                {
                    UpdateEquipUI(inventoryGridSlotsPr.CurItemType);
                    //Check(_closedSlot, true); 
                    // 사운드 재생
                    UIUtilManager.Instance.PlayUISound(UISoundType.EquipItem);

                    //_closedSlot.SetItemData(dragItemPresenter.ItemData);
                }
                else
                {
                    // 사운드 재생
                    UIUtilManager.Instance.PlayUISound(UISoundType.Error);
                }

            }
            else
            {

            }
            ActiveDragItem(false);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        }

        /// <summary>
        /// 장비템 장착 
        /// </summary>
        private void EquipEquipment(List<SlotItemPresenter> _targetList)
        {
            SlotItemPresenter _targetSlot = null; 
            if (_targetList.Count > 0)
            {
                // 남은 칸 이씅면 
                bool _isEmptySlot = false;
                foreach (var _target in _targetList)
                {
                    // 여기로 장착 
                    if (_target.ItemData.equipmentType == dragItemPresenter.ItemData.equipmentType)
                    {
                        _targetSlot = _target; 
                        if(_target.ItemData == null ||
                           string.IsNullOrEmpty(_target.ItemData.key))
                        {
                            _isEmptySlot = true; 
                            // 데이터 적용 
                            slotCallbackDic[_target.ItemData.itemType]?.Invoke(dragItemPresenter.ItemData, _target.Index);
                            UpdateEquipUI(); // UI 업데이트
                            UIUtilManager.Instance.PlayUISound(UISoundType.EquipItem);
                            break;
                        }
                    }
                         
                }
                
                // 빈칸이 없으면 있는 거 장착해제 하고 
                // 현재꺼 장착 
                if (_isEmptySlot == false && _targetSlot != null)
                {
                    // 장착 해제
                    equipSlotRemoveCallbackDic[_targetSlot.ItemData.itemType]?.Invoke(_targetSlot.Index);
                    
                    // 장착
                    slotCallbackDic[_targetSlot.ItemData.itemType]?.Invoke(dragItemPresenter.ItemData, _targetSlot.Index); 
                    UpdateEquipUI(); // UI 업데이트
                    UIUtilManager.Instance.PlayUISound(UISoundType.EquipItem);
                }
            }
        }
        private void Equip()
        {
            SlotItemPresenter _targetSlot = null; 
            // 현재 패널의 장착 슬롯 가져오고 
            List<SlotItemPresenter> _targetList = inventoryGridSlotsPr.CurInvenPanel.equipItemViewList; 
            
            // 장비 패널이면 
            if (inventoryGridSlotsPr.CurItemType == ItemType.Equipment)
            {
                // 장비 타입 데이터 맞는 걸로 찾기 
                EquipEquipment(_targetList); 
            }

            // 장착 슬롯 count가 0 보다 큰지 확인하고 
            if (_targetList.Count > 0)
            {
                // 남은 칸 이씅면 
                bool _isEmptySlot = false; 
                foreach (var _target in _targetList)
                {
                    // 여기로 장착 
                    if (_target.ItemData.itemType == dragItemPresenter.ItemData.itemType &&  _target.ItemData == null || string.IsNullOrEmpty(_target.ItemData.key))
                    {
                        _targetSlot = _target; 
                        _isEmptySlot = true;
                        // 데이터 적용 
                        slotCallbackDic[_target.ItemData.itemType]?.Invoke(dragItemPresenter.ItemData, _target.Index); 
                        UpdateEquipUI(); // UI 업데이트
                        UIUtilManager.Instance.PlayUISound(UISoundType.EquipItem);
                        break; 
                    }
                }

                // 빈칸이 없으면 있는 거 장착해제 하고 
                // 현재꺼 장착 
                if (_isEmptySlot == false)
                {
                    // 장착 해제
                    _targetSlot = _targetList[0];
                    equipSlotRemoveCallbackDic[_targetSlot.ItemData.itemType]?.Invoke(_targetSlot.Index);
                    
                    // 장착
                    slotCallbackDic[_targetSlot.ItemData.itemType]?.Invoke(dragItemPresenter.ItemData, _targetSlot.Index); 
                    UpdateEquipUI(); // UI 업데이트
                    UIUtilManager.Instance.PlayUISound(UISoundType.EquipItem);
                }
                // 장착 칸이 없으면 리턴 
                if (_targetList.Count == 0)
                {
                    return; 
                }
                
            }
            // 남은 칸이 있는지 확인하고 
            // 남은 칸이 없으면 서로 바꾸기 
            // 목표 칸 찾고 
            // 장착 이벤트 실행 
        }

        private void RemoveItemData(SlotItemPresenter slot)
        {
            InventoryManager.Instance.ItemRemove(slot.ItemData);
        }

        private void Check(SlotItemPresenter slot, bool _isActive)
        {
            DoubleClicker doubleClicker =new DoubleClicker(() =>
            {
                RemoveItemData(slot);
            });
            if (_isActive == true)
            {
                slot.Item.AddManipulator(doubleClicker);
            }
            else 
            {
                slot.Item.AddManipulator(doubleClicker);
            }
        }

        private  void UpdateEquipUI(ItemType _itemTYpe)
        {
            List<ItemData> _dataList = new List<ItemData>();
            List<SlotItemPresenter> _slotList = new List<SlotItemPresenter>(); 
            switch (_itemTYpe)
            {
                case ItemType.Weapon:
                    _dataList = InventoryManager.Instance.GetEquipWeaponList(); 
                    _slotList = inventoryGridSlotsPr.InvenPanelDic[ItemType.Weapon].equipItemViewList;
                    break;
                case ItemType.Consumption:
                    _dataList = new List<ItemData>(); 
                    _dataList.Add(InventoryManager.Instance.GetEquipArrow()); 
                    _slotList = inventoryGridSlotsPr.InvenPanelDic[ItemType.Consumption].equipItemViewList;
                    break;
                case ItemType.Skill:
                    break;
                case ItemType.Equipment:
                    _dataList = InventoryManager.Instance.GetEquipItemList(); 
                    _slotList = inventoryGridSlotsPr.InvenPanelDic[ItemType.Equipment].equipItemViewList;
                    break;
                case ItemType.Accessories:
                    _dataList = InventoryManager.Instance.GetEquipSoulList(); 
                    _slotList = inventoryGridSlotsPr.InvenPanelDic[ItemType.Accessories].equipItemViewList;
                    break;
                case ItemType.Material:
                    break;
                case ItemType.Valuable:
                    break;
                case ItemType.Marker:
                    break;
                case ItemType.None:
                    break;
            }
         //   var _slotList = inventoryGridSlotsPr.CurInvenPanel.equipItemViewList;

            //var slotList = inventoryGridSlotsPr.InvenPanelDic[ItemType.Accessories].equipItemViewList;
            for (int i = 0; i < _dataList.Count(); i++)
            {
                _slotList[i].SetItemData(_dataList[i]);
            }
    
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

        private void InitCallbackDic()
        {
            this.slotCallbackDic.Clear(); 

            this.slotCallbackDic.Add(ItemType.None, (x1, x2) => { return false;});
            this.slotCallbackDic.Add(ItemType.Weapon, (x1, x2) => InventoryManager.Instance.SetQuickSlotItem(x1, x2));
            this.slotCallbackDic.Add(ItemType.Consumption, (x1, x2) => InventoryManager.Instance.EquipArrow(x1));
            this.slotCallbackDic.Add(ItemType.Equipment, (x1, x2) => InventoryManager.Instance.EquipEquipment(x2,x1));
            this.slotCallbackDic.Add(ItemType.Accessories , (x1, x2) => InventoryManager.Instance.EquipAccessories(x2,x1));
            
            this.equipSlotRemoveCallbackDic.Clear(); 

            this.equipSlotRemoveCallbackDic.Add(ItemType.Weapon, (x1) => InventoryManager.Instance.RemoveQuickSlotItem(x1));
            this.equipSlotRemoveCallbackDic.Add(ItemType.Equipment, (x1 ) => InventoryManager.Instance.RemoveEquipment(x1));
            this.equipSlotRemoveCallbackDic.Add(ItemType.Accessories , (x1) => InventoryManager.Instance.RemoveAccessories(x1));

        }

        /// <summary>
        /// 장착 UI 업데이트(현재SO 에서 데이터 받고 그에 따라 업데이트하기 ) 
        /// </summary>
        private void UpdateEquipUI()
        {
            UpdateEquipUI(ItemType.Accessories);
            UpdateEquipUI(ItemType.Consumption);
            UpdateEquipUI(ItemType.Weapon);
            UpdateEquipUI(ItemType.Equipment);
            
            //InventoryManager.Instance.Inventory
        }
    }
}

