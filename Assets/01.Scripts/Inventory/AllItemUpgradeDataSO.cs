using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	[CreateAssetMenu(fileName = "AllItemUpgradeDataSO", menuName = "SO/AllItemUpgradeDataSO")]
	public class AllItemUpgradeDataSO : ScriptableObject
	{
		public List<ItemUpgradeDataSO> itemUpgradeDataList = new List<ItemUpgradeDataSO>();
		private Dictionary<string, ItemUpgradeDataSO> itemUpgradeDataDic = new Dictionary<string, ItemUpgradeDataSO>();

		public void Awake()
		{
			itemUpgradeDataDic.Clear();
			for (int i = 0; i < itemUpgradeDataList.Count; ++i)
			{
				ItemUpgradeDataSO itemUpgradeDataSO = itemUpgradeDataList[i];
				itemUpgradeDataDic.Add(itemUpgradeDataSO.key, itemUpgradeDataSO);
			}
		}

		public ItemUpgradeDataSO GetItemUpgradeDataSO(string _key)
		{
			ItemUpgradeDataSO itemUpgradeData = null;
			if (itemUpgradeDataDic.TryGetValue(_key, out itemUpgradeData))
			{
				return itemUpgradeData;
			}
			return null;
		}

	}
}
