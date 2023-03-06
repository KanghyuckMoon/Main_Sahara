using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace Inventory
{
	public class DropItem
	{
		public void GetItem(ItemDataSO _itemDataSO)
		{
			InventoryManager.Instance.AddItem(_itemDataSO.key, _itemDataSO.count);
		}
	}
}

