using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using UnityEngine.UIElements; 

namespace UI.Upgrade 
{
    public class UpgradeSlotData
    {
        public VisualElement parentSlot; 
        public ItemData itemData;
        public int index;
        public int maxIndex; 
        public UpgradeSlotData(VisualElement _v, ItemData _data, int _idx, int _maxIdx)
        {
            this.parentSlot = _v;
            this.itemData = _data;
            this.index = _idx;
            this.maxIndex = _maxIdx; 
        }
    }

}

