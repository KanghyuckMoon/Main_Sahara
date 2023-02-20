using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EquipmentSystem
{
    public class BoneItem : MonoBehaviour
    {
        public List<Transform> itemLists = new List<Transform>();

        public void RemoveBones()
        {
            foreach (Transform item in itemLists)
            {
                Destroy(item.gameObject);
            }
        }
    }
}