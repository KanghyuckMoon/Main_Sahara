using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PassiveItem;

namespace Module
{
    public partial class ItemModule : AbBaseModule
    {
        public void SetPassiveItem(AccessoriesItemType _itemKey)
        {
            if (passiveItem[_itemKey] == null)
            {
                ItemPassive _itemPassive = null;

                if (_itemKey == AccessoriesItemType.HpUp)
                {
                    _itemPassive = new HpAccessories();
                }

                passiveItem.Add(_itemKey, _itemPassive);

                ApplyPassive();
            }
        }

        public void SetEquipmentItem()
        {

        }
    }
}