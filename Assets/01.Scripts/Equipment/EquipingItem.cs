using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PassiveItem;

namespace EquipmentSystem
{
    public enum ItemType
    {
        HELMET,
        NONE
    }

    public class EquipingItem : MonoBehaviour
    {
        public ItemType itemType;
        public Vector3 setPos;
        public Quaternion setRot;
        public ItemDataSO itemDataSO;

        //public void OnValidate()
        //{
        //    setPos = transform.position;
        //}
    }
}