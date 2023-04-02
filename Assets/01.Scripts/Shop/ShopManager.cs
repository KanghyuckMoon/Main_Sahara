using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Utill.Measurement;
using Module;
using Module.Shop;
using Inventory;

namespace Shop
{
	public class ShopManager : MonoSingleton<ShopManager>
	{
		private ShopModule currentShopModule;

		public void SetShopModule(ShopModule _shopModule)
		{
			currentShopModule = _shopModule;
		}

		public ItemData GetItemIndex(int _index)
		{
			return currentShopModule.ShopSO.itemDataList[_index];
		}

		public List<ItemData> GetAllItemData()
		{
			return currentShopModule.ShopSO.itemDataList;
		}

		public void BuyItem(ItemData _itemData)
		{
			if (_itemData.count == 0)
			{
				Logging.Log("더 이상 아이템을 구매할 수 없습니다");
				return;
			}

			if (_itemData.price > InventoryManager.Instance.GetMoney())
			{
				Logging.Log("돈이 부족합니다");
				return;
			}

			InventoryManager.Instance.AddItem(_itemData.key);
			if (_itemData.count > -1)
			{
				--_itemData.count;
			}
			InventoryManager.Instance.IncreaseMoney(-_itemData.price);
		}

		public void BuyItem(int index)
		{
			ItemData _itemData = GetItemIndex(index);
			if (_itemData.count == 0)
			{
				Logging.Log("더 이상 아이템을 구매할 수 없습니다");
				return;
			}

			if (_itemData.price > InventoryManager.Instance.GetMoney())
			{
				Logging.Log("돈이 부족합니다");
				return;
			}

			InventoryManager.Instance.AddItem(_itemData.key);
			if (_itemData.count > -1)
			{
				--_itemData.count;
			}
			InventoryManager.Instance.IncreaseMoney(-_itemData.price);
		}

		public void SellItem(ItemData _itemData)
		{
			InventoryManager.Instance.IncreaseMoney(_itemData.price);
			InventoryManager.Instance.ItemReduce(_itemData.key, 1);
		}
		public void SellItem(int _index)
		{
			ItemData _itemData = InventoryManager.Instance.GetItemIndex(_index);
			InventoryManager.Instance.IncreaseMoney(_itemData.price);
			InventoryManager.Instance.ItemReduce(_itemData.key, 1);
		}

	}
}
