using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryAllAdd : MonoBehaviour
    {
        [SerializeField]
        private AllItemDataSO allItemDataSo;
        [SerializeField]
        private InventorySO inventorySO;
        
        [ContextMenu("AllAdd")]
        public void AllAdd()
        {
            inventorySO.itemDataList.Clear();
            foreach (var _value in allItemDataSo.itemDataSOList)
            {
                inventorySO.itemDataList.Add(ItemData.CopyItemDataSO(_value));
            }
            
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(inventorySO);
#endif
        }

    }   
}
