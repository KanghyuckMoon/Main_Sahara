#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Inventory
{
    public class InventoryAddEditor : EditorWindow
    {
        static InventoryAddEditor exampleWindow;
        
        [MenuItem("MoonTool/InventoryAddEditor")]
        static void Open ()
        {
            if (exampleWindow == null)
            {
                exampleWindow = CreateInstance<InventoryAddEditor> ();
            }

            exampleWindow.Show();
        }

        private string itemCode;
        private int itemCountToInv;
        private int itemCodeToSlotIndex = 0;
        private InventorySO inventorySO;
        private AllItemDataSO allItemDataSO;
        
        void OnGUI ()
        {
            EditorGUI.BeginChangeCheck ();
            inventorySO = (InventorySO)EditorGUILayout.ObjectField(inventorySO, typeof(InventorySO), false);
            allItemDataSO = (AllItemDataSO)EditorGUILayout.ObjectField(allItemDataSO, typeof(AllItemDataSO), false);
            //toggle 을 마우스로 클릭해서 수치를 변경
            itemCode = EditorGUILayout.TextField("AddItemCode To Inventory", itemCode);
            itemCountToInv = EditorGUILayout.IntField("AddItemCount To Inventory", itemCountToInv);
            itemCodeToSlotIndex = EditorGUILayout.IntField("AddItemCode To SlotIndex", itemCodeToSlotIndex);

            if (GUILayout.Button("AddItem To Inventory"))
            {
                if (inventorySO == null || allItemDataSO == null)
                {
                    return;
                }
                var _itemData= allItemDataSO.itemDataSOList.Find(x => x.key == itemCode);
                var _copyItemData = ItemData.CopyItemDataSO(_itemData);
                _copyItemData.count = itemCountToInv;
                inventorySO.itemDataList.Add(_copyItemData);
            }
            
            if (GUILayout.Button("AddItem To Slot"))
            {
                if (inventorySO == null || allItemDataSO == null)
                {
                    return;
                }
                var _itemData= allItemDataSO.itemDataSOList.Find(x => x.key == itemCode);
                var _copyItemData = ItemData.CopyItemDataSO(_itemData);
                inventorySO.quickSlot[itemCodeToSlotIndex] = _copyItemData;
            }
            
            if (GUILayout.Button("AddItem To Equip"))
            {
                if (inventorySO == null || allItemDataSO == null)
                {
                    return;
                }
                var _itemData= allItemDataSO.itemDataSOList.Find(x => x.key == itemCode);
                var _copyItemData = ItemData.CopyItemDataSO(_itemData);
                _copyItemData.count = itemCountToInv;
                inventorySO.equipments[(int)_copyItemData.equipmentType - 1] = _copyItemData;
            }

            //toggleValue 의 수치가 변경될 때마다 true가 됩니다
            //if (EditorGUI.EndChangeCheck ()) 
            //{
            //    
            //}
        }
    }
}

#endif