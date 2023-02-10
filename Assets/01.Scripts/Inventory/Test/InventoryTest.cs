using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	public class InventoryTest : MonoBehaviour
	{
		[ContextMenu("AddItemTest")]
		public void AddItemTest()
		{
			InventoryManager.Instance.AddItem("ItemTest");
		}

		[ContextMenu("GetItemTest")]
		public void GetItemTest()
		{
			InventoryManager.Instance.GetItem("ItemTest");
		}

	}

}