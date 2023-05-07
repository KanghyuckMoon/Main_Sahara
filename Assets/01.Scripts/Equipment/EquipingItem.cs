using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PassiveItem;

namespace EquipmentSystem
{
    public enum ItemType
    {
        HELMET,
        WRIST,
        SHOULDER,
        BACK,
        EAR,    
        NONE
    }

    public class EquipingItem : MonoBehaviour
    {
        public ItemType itemType;
        public Vector3 setPos;
        public Quaternion setRot;
        public Vector3 scale;
        public ItemDataSO itemDataSO;

        //public void OnValidate()
        //{
        //    setPos = transform.position;
        //}

        [ContextMenu("¼³Á¤")]
        public void SettingInfo()
        {
            setPos = transform.localPosition;
            setRot = transform.localRotation;
            scale = transform.localScale;
        }
    }
    
    
}