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
            quickslot_view
        }

        private List<SlotItemPresenter> _slotList = new List<SlotItemPresenter>();

        public List<SlotItemPresenter> SlotList => _slotList; 
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

        private void InitSlots()
        {
            int _index = 0; 
            List<VisualElement> _vList = GetVisualElement((int)Elements.quickslot_view).Query(className: "quick_slot_hud").ToList(); 
            foreach(var _v in _vList)
            {
                _slotList.Add(new SlotItemPresenter(_v, _index));
                ++_index; 
            }
        }


    }

}

