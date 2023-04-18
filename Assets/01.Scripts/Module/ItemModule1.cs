using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PassiveItem;
using Pool;

namespace Module
{
    public partial class ItemModule : AbBaseModule
    {
        public void SetPassiveItem(AccessoriesItemType _itemKey)
        {
            if (!passiveItem.ContainsKey(_itemKey))
            {
                ItemPassive _itemPassive = null;

				switch (_itemKey)
				{
					case AccessoriesItemType.HpUp:
						_itemPassive = GetItemWithPool<HpAccessories>("HpAccessories");
						break;
					case AccessoriesItemType.Fire:
						_itemPassive = GetItemWithPool<FireAccessories>("FireAccessories");
						break;
					case AccessoriesItemType.DoubleJump:
						_itemPassive = GetItemWithPool<DoubleJump_Accessories>("DoubleJump_Accessories");
						break;
					case AccessoriesItemType.Dash:
						_itemPassive = GetItemWithPool<Dash_Accessories>("Dash_Accessories"); 
						break;
					case AccessoriesItemType.NONE:
						break;
				}

                passiveItem.Add(_itemKey, _itemPassive);

                ApplyPassive();
			}
		}
	}
}