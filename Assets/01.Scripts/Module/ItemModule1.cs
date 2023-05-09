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
	        if (passiveItem.ContainsKey(_itemKey))
	        {
		        //Debug.LogError(("sfasdafagagagaeg"));
		        passiveItem[_itemKey].UpgradeEffect();
	        }
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
					case AccessoriesItemType.TimeSlow:
						_itemPassive = GetItemWithPool<TimeSlow_Accessories>("TimeSlow_Accessories");
						break;
					case AccessoriesItemType.Burning:
						_itemPassive = GetItemWithPool<Burning_Accessories>("Burning_Accessories");
						break;
					case AccessoriesItemType.Shield:
						_itemPassive = GetItemWithPool<Shield_Accessories>("Shield_Accessories");
						break;
					case AccessoriesItemType.Flame:
						_itemPassive = GetItemWithPool<Flame_Accessories>("Flame_Accessories");
						break;
					case AccessoriesItemType.ChargeJump:
						_itemPassive = GetItemWithPool<SuperJump_Accessories>("SuperJump_Accessories");
						break;
					case AccessoriesItemType.AddSpeed:
						_itemPassive = GetItemWithPool<AddSpeed_Accessories>("AddSpeed_Accessories");
						break;
					case AccessoriesItemType.Resurrection:
						_itemPassive = GetItemWithPool<Resurrection_Accessories>("Resurrection_Accessories");
						break;
					case AccessoriesItemType.Glare:
						_itemPassive = GetItemWithPool<Glare_Accessories>("Glare_Accessories");
						break;
					case AccessoriesItemType.Crawling:
						_itemPassive = GetItemWithPool<Crawling_Accessories>("Crawling_Accessories");
						break;
					case AccessoriesItemType.UnlockInteraction:
						_itemPassive = GetItemWithPool<UnlockInteraction_Accessories>("UnlockInteraction_Accessories");
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