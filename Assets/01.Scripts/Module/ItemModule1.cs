using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PassiveItem;

namespace Module
{
    public partial class ItemModule : AbBaseModule
    {
        public void SetPassiveItem(string _itemKey)
        {
            ItemPassive _itemPassive = null;

            if (_itemKey == "HpUp")
            {
                _itemPassive = new HpAccessories();
            }

            passiveItem.Add(_itemKey, _itemPassive);

            ApplyPassive();
        }
    }
}