using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    
    public class EventItem : MonoBehaviour
    {
        [SerializeField]
        private string itemKey;
        [SerializeField]
        private int count = 1;
        
        public void AddItem()
        {
            InventoryManager.Instance.AddItem(itemKey, count);
        }
    }
}
