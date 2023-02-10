using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using Utill.Addressable;
using Utill.Measurement;

namespace Inventory
{
	public class ItemUpgradeManager : MonoSingleton<ItemUpgradeManager>
	{
		AllItemUpgradeDataSO allItemUpgradeDataSO;

		public void Start()
		{
			allItemUpgradeDataSO = AddressablesManager.Instance.GetResource<AllItemUpgradeDataSO>("AllItemUpgradeDataSO");
			allItemUpgradeDataSO.Awake();
		}

		public void Upgrade(string _key)
		{
			ItemUpgradeDataSO _itemUpgradeDataSO = allItemUpgradeDataSO.GetItemUpgradeDataSO(_key);

			for (int i = 0; i < _itemUpgradeDataSO.needItemDataList.Count; ++i)
			{
				ItemData _itemData = _itemUpgradeDataSO.needItemDataList[i];
				if (!InventoryManager.Instance.ItemCheck(_itemData.key, _itemData.count))
				{
					Logging.Log("��� ����");
					return;
				}
			}

			for (int i = 0; i < _itemUpgradeDataSO.needItemDataList.Count; ++i)
			{
				ItemData _itemData = _itemUpgradeDataSO.needItemDataList[i];
				InventoryManager.Instance.ItemReduce(_itemData.key, _itemData.count);
			}
			InventoryManager.Instance.AddItem(_itemUpgradeDataSO.key, _itemUpgradeDataSO.count);
			Logging.Log("������ ����");

		}


	}

}