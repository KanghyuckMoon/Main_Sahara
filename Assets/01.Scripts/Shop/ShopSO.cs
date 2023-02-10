using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

namespace Shop
{
	[CreateAssetMenu(fileName = "ShopSO", menuName = "SO/ShopSO")]
	public class ShopSO : ScriptableObject
	{
        public string shopName;
		public List<ItemData> itemDataList = new List<ItemData>();

        public ShopSave shopSave = new ShopSave();
        public ShopSave SaveData()
        {
            shopSave.shopName = this.shopName;
            shopSave.itemDataList = this.itemDataList;
            return shopSave;
        }

        public void LoadData()
        {
            this.itemDataList = shopSave.itemDataList;
        }

    }
    [HideInInspector]
    [System.Serializable]
    public class ShopSave
    {
        public string shopName;
        public List<ItemData> itemDataList = new List<ItemData>();
    }
}
