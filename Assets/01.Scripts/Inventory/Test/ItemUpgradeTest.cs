using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	public class ItemUpgradeTest : MonoBehaviour
	{
		[SerializeField]
		private string key;

		[ContextMenu("UpgradeItem")]
		public void UpgradeItem()
		{
			ItemUpgradeManager.Instance.Upgrade(key);
		}

	}

}