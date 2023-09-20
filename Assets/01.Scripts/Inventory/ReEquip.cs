using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	public class ReEquip : MonoBehaviour
	{
		public void Start()
		{
			var soulList = InventoryManager.Instance.GetEquipSoulList();
			for(int i = 0; i < soulList.Count; ++i)
			{
				InventoryManager.Instance.ReSetPassiveItem(soulList[i]);
			}
		}
	}
}
