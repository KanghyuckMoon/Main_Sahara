using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;
using UI.Inventory;
using Inventory;
namespace UI
{
    [System.Serializable]
    public class QuickSlotView : AbUI_Base
    {
        enum Elements
        {
            quickslot_view,
            select_effect
        }

        private List<SlotItemPresenter> _slotList = new List<SlotItemPresenter>();
        private SlotItemPresenter arrowSlot;

        private SlotItemPresenter selectSlotPr; 

        public List<SlotItemPresenter> SlotList => _slotList;
        public SlotItemPresenter ArrowSlot { get => arrowSlot; set => arrowSlot = value; }

        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
        }

        public override void Init()
        {
            base.Init();
            InitSlots(); 
        }

        /// <summary>
        /// ���õ� ������ Ȱ��ȭ ����Ʈ 
        /// </summary>
        public void UpdateActiveEffect()
        {
            selectSlotPr.SelectSlot(false);

            selectSlotPr = _slotList[InventoryManager.Instance.GetCurrentQuickSlotIndex()];
            selectSlotPr.SelectSlot(true);
            selectSlotPr.Parent.Add(GetVisualElement((int)Elements.select_effect));
        }

        private void InitSlots()
        {
            int _index = 0; 
            List<VisualElement> _vList = GetVisualElement((int)Elements.quickslot_view).Query(className: "quick_slot_hud").ToList(); 
            foreach(var _v in _vList)
            {
                // ȭ�� ������ �ٸ��� ó�� 
                if(_v.name == "arrow_slot")
                {
                    arrowSlot = new SlotItemPresenter(_v);
                    continue; 
                }
                _slotList.Add(new SlotItemPresenter(_v, _index));
                ++_index; 
            }
            // ���� ���� ���� ���� 
            selectSlotPr = _slotList[InventoryManager.Instance.GetCurrentQuickSlotIndex()];
        }


    }

}

