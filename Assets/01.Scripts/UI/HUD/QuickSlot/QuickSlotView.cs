using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;
using UI.Inventory;
using Utill.Addressable;

namespace UI
{
    [System.Serializable]
    public class QuickSlotView : AbUI_Base
    {
        
        enum Elements
        {
            //quickslot_view,
            //quick_slot_parent,
            //select_effect,
            skill_image, 
            mana_guage,
            
        }

        enum Quickslots
        {
            quickslot_image_bot_temp,
            quickslot_image_bot,
            quickslot_image_mid,
            quickslot_image_top,
            quickslot_image_top_temp,
        }

        enum Labels
        {
            mana_text
        }

        private List<SlotItemPresenter> _slotList = new List<SlotItemPresenter>();
        private SlotItemPresenter arrowSlot;

        private SlotItemPresenter selectSlotPr; 

        public List<SlotItemPresenter> SlotList => _slotList;
        public SlotItemPresenter ArrowSlot { get => arrowSlot; set => arrowSlot = value; }

        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Quickslots));
            BindVisualElements(typeof(Elements));
            BindLabels(typeof(Labels));
        }

        public override void Init()
        {
            base.Init();
            //InitSlots(); 
            InitQuickSlotUIImage(); 
        }

        public void NotActiveMana(bool _isActive)
        {
            ShowVisualElement(GetVisualElement((int)Elements.mana_guage),_isActive);
        }
        public void SetSkillImage(Sprite _sprite)
        {
            GetVisualElement((int)Elements.skill_image).style.backgroundImage = new StyleBackground(_sprite);
        }
        public void SetManaText(int _mana)
        {
            GetLabel((int)Labels.mana_text).text = _mana.ToString(); 
        }

        /// <summary>
        /// 퀵슬롯HUD UI 이미지 업데이트  
        /// </summary>
        public void InitQuickSlotUIImage()
        {
            int _idx = 0; 
            foreach (var _elementEnum in Enum.GetValues(typeof(Quickslots)))
            {
                // 이미지 초기화 
                _slotList.Add(new SlotItemPresenter(GetVisualElement((int)_elementEnum)));
            //    ItemData _itemData = InventoryManager.Instance.GetQuickSlotItem(_idx); 
            //    GetVisualElement((int)_elementEnum).style.backgroundImage =
            //        AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey);
            //    ++_idx; 
            }
        }
        /// <summary>
        /// 선택된 퀵슬롯 활성화 이펙트 
        /// </summary>
        public void UpdateActiveEffect()
        {
            selectSlotPr.SelectSlot(false);

            //selectSlotPr = _slotList[InventoryManager.Instance.GetCurrentQuickSlotIndex()];
            selectSlotPr.SelectSlot(true);
            //selectSlotPr.Parent.Add(GetVisualElement((int)Elements.select_effect));
        }

        public void UpQuickslots()
        {
            GetVisualElement((int)Quickslots.quickslot_image_top_temp).AddToClassList("");
        }
        
        private void InitSlots()
        {
            int _index = 0; 
            /*List<VisualElement> _vList = GetVisualElement((int)Elements.quickslot_view).Query(className: "quick_slot_hud").ToList(); 
            foreach(var _v in _vList)
            {
                // 화살 슬롯은 다르게 처리 
                if(_v.name == "arrow_slot")
                {
                    arrowSlot = new SlotItemPresenter(_v);
                    continue; 
                }
                _slotList.Add(new SlotItemPresenter(_v, _index));
                ++_index; 
            }*/
            // 현재 선택 슬롯 설정 
            // selectSlotPr = _slotList[InventoryManager.Instance.GetCurrentQuickSlotIndex()];
        }

        

    }

}

