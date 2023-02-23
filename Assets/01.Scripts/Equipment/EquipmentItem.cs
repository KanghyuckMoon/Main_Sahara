using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EquipmentSystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/New Item")]  
    public class EquipmentItem : ScriptableObject
    {
        public GameObject objModelPrefab;

        public List<string> boneNameLists = new List<string>();

        private void OnValidate()
        {
            boneNameLists.Clear();
            if (objModelPrefab == null || objModelPrefab.GetComponentInChildren<SkinnedMeshRenderer>() == null)
            {
                return;
            }
            SkinnedMeshRenderer renderer = objModelPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
            var bones = renderer.bones;
            foreach (var t in bones)
            {
                boneNameLists.Add(t.name);
            }
        }
    }
}