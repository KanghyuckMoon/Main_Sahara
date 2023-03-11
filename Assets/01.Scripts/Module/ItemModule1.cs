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
            if (!passiveItem.ContainsKey(_itemKey))
            {
                ItemPassive _itemPassive = null;

				switch (_itemKey)
				{
					case AccessoriesItemType.HpUp:
						_itemPassive = new HpAccessories();
						break;
					case AccessoriesItemType.Fire:
						_itemPassive = new FireAccessories(mainModule);
						break;
					case AccessoriesItemType.DoubleJump:
						_itemPassive = new DoubleJump_Accessories(mainModule);
						break;
					case AccessoriesItemType.Dash:
						_itemPassive = new Dash_Accessories(mainModule);
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