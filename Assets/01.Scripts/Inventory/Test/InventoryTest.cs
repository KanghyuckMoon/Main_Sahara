using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	public class InventoryTest : MonoBehaviour
	{
	    public string itemAddress;
	
		[ContextMenu("AddItemTest")]
		public void AddItemTest()
		{
			InventoryManager.Instance.AddItem(itemAddress);
		}

		[ContextMenu("GetItemTest")]
		public void GetItemTest()
		{
			InventoryManager.Instance.GetItem(itemAddress);
		}

	}

}