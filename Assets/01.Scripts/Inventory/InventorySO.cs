using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "InventorySO", menuName = "SO/InventorySO")]
    public class InventorySO : ScriptableObject
    {
        public int money = 0;
        public List<ItemData> itemDataList = new List<ItemData>();
        public ItemData[] quickSlot = new ItemData[5];
        public ItemData[] equipments = new ItemData[4];
        public ItemData[] accessories = new ItemData[4];
        public ItemData[] skills = new ItemData[2];

        public InventorySave inventorySave = new InventorySave();

        public InventorySave SaveData()
		{
            inventorySave.itemDataList = this.itemDataList; 
            inventorySave.quickSlot = this.quickSlot;
            return inventorySave;
        }

        public void LoadData()
        {
            this.itemDataList = inventorySave.itemDataList;
            this.quickSlot = inventorySave.quickSlot;
        }
    }

    [System.Serializable]
    public class InventorySave
    {
        public List<ItemData> itemDataList = new List<ItemData>();
        public ItemData[] quickSlot = new ItemData[5];


    }
}