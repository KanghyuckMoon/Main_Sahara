using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	[CreateAssetMenu(fileName = "AllItemDataSO", menuName = "SO/AllItemDataSO")]
	public class AllItemDataSO : ScriptableObject
	{
		public List<ItemDataSO> itemDataSOList = new List<ItemDataSO>();
		public Dictionary<string, ItemData> itemDataDic = new Dictionary<string, ItemData>();

		public void ItemDataGenerate()
		{
			itemDataDic.Clear();
			for (int i = 0; i < itemDataSOList.Count; ++i)
			{
				itemDataDic.Add(itemDataSOList[i].key, ItemData.CopyItemDataSO(itemDataSOList[i]));
			}
		}

		public ItemData GetItemData(string key)
		{
			ItemData itemData;
			if (itemDataDic.TryGetValue(key, out itemData))
			{
				return itemData;
			}
			return null;
		}
	}
}
