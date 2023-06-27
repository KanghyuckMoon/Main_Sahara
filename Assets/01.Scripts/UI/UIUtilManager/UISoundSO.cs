using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.SeralizableDictionary;

namespace UI.UtilManager
{
    [System.Serializable]
    public class StringUISoundTypeDict : SerializableDictionary<UISoundType, UISoundData> { };

    public enum UISoundType
    {
        Hover, 
        Click, 
        ShowScreen, 
        EquipItem, 
        NextDialogue, 
        GetItem, 
        MarkMap, 
        ChangeInvenCategory,
        Error, 
        EventAlarm
        
        
    }

    [CreateAssetMenu(menuName = "SO/UI/UISoundSO")]
    public class UISoundSO : ScriptableObject
    {
        public List<UISoundData> UISoundDataList = new List<UISoundData>(); 
        public StringUISoundTypeDict uiSoundTypeAddressDic = new StringUISoundTypeDict();

        private void OnValidate()
        {
            foreach (var _type in Enum.GetValues(typeof(UISoundType)))
            {
                UISoundType type = (UISoundType)_type;

                // -1 이면 없는거 
                int index = UISoundDataList.FindIndex((x) => x.key == type);
                if (index == -1)
                {
                    UISoundDataList.Add(new UISoundData{key = type, address = String.Empty});
                }
            }
        }

        [ContextMenu("ListToDic")]
        public void InputDataListToDic()
        {
            uiSoundTypeAddressDic.Clear();
            foreach (var _obj in UISoundDataList)
            {
                uiSoundTypeAddressDic.Add(_obj.key, _obj);
            }
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
        public string GetSoundAddress(UISoundType _type)
        {
            if (uiSoundTypeAddressDic.TryGetValue(_type,out UISoundData _data))
            {
                return _data.address; 
            }
            Debug.LogWarning(Enum.GetName(typeof(UISoundType),_type) + "에 맞는 주소가 없습니다 .UISoundSO를 확인하세요");
            return String.Empty;
        }
    }

    [System.Serializable]
    public class UISoundData
    {
        public UISoundType key;
        public string address; 
    }
}

