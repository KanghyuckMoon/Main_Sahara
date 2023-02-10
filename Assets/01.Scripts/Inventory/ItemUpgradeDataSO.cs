using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	[CreateAssetMenu(fileName = "ItemUpgradeDataSO", menuName = "SO/ItemUpgradeDataSO")]
	public class ItemUpgradeDataSO : ScriptableObject
	{
		public string key;
		public int count = 1;
		public List<ItemData> needItemDataList = new List<ItemData>();

	}
}
