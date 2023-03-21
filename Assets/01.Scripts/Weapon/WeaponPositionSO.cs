using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.SeralizableDictionary;

namespace Weapon
{
    [System.Serializable]
    public class StringListWeaponPositionData : SerializableDictionary<string, WeaponPositionData> { };

    [CreateAssetMenu(menuName = "SO/WeaponPositionSO")]
    public class WeaponPositionSO : ScriptableObject
    {
        public StringListWeaponPositionData positionDatas;// = new StringListWeaponPositionData();

        public WeaponPositionData GetWeaponPoritionData(string _str)
        {
            if (positionDatas.TryGetValue(_str, out var _value))
            {
                return _value;
            }

            return null;
        }

        public void UploadWeaponPositionData(WeaponPositionData _weaponPositionData)
        {
            if (_weaponPositionData.objectName is null)
            {
                return;
            }

            if (positionDatas.TryGetValue(_weaponPositionData.objectName, out var _list))
            {
                positionDatas[_weaponPositionData.objectName] = WeaponPositionData.Copy(_weaponPositionData);
            }
            else
            {
                positionDatas.Add(_weaponPositionData.objectName, _weaponPositionData);
            }
            
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
            
        }
    }

    [System.Serializable]
    public class WeaponPositionDataList
    {
        public List<WeaponPositionData> hitBoxDataList = new List<WeaponPositionData>();
    }

    [System.Serializable]
    public class WeaponPositionData
    {
        public string objectName;
        public Vector3 weaponPosition;
        public Quaternion weaponRotation;

        public static WeaponPositionData Copy(WeaponPositionData _weaponPositionData)
        {
            WeaponPositionData _data = new WeaponPositionData();
            _data.objectName = _weaponPositionData.objectName;
            _data.weaponPosition = _weaponPositionData.weaponPosition;
            _data.weaponRotation = _weaponPositionData.weaponRotation;
            return _data;
        }
    }
    
    
}