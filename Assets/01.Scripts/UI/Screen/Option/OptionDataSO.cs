using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utill.SeralizableDictionary;

namespace UI.Option
{
    public enum OptionModifyType
    {
        Dropdown,
        Bar 
    }

    public enum OptionType
    {
        None, 
        // ���� 
        MasterVolume, 
        BackgroundVolume, 
        EffVolume, 
        EnvironmentVolume,
        // �׷��� 
        Resolution, 
        TextureQuality,
        ShadowQuality, 
        RefreshRate, 
        AntiAliasing,
        IsFullScreen, 
        /*
         * ���� �ɼ� Ÿ�� ��� �ɼǸ��� ���صΰ�
         * OptionPresenter���� OptionType�� Ű��
         * action �� �޾Ƶ�
         */
    }

    public enum OptionCategoryType
    {   
        Graphics, 
        Sound, 
        GameInfo, 
        Help 
    }

    [Serializable]
    public class OptionDataKey
    {
        public OptionCategoryType key;
        public List<OptionData> optionDataList;
    }
    [Serializable]
    public class OptionData
    {   
        public OptionType optionType;
        public OptionModifyType optionModifyType;
        public string name;
        [Space(10)]
        public string defaultDropdownStr; // �⺻ ��Ӵٿ� �� 
        public List<string> dropdownList = new List<string>(); // ��Ӵٿ ǥ�õ� ���ڵ� 
        [Space(10)]

        public int minValue, maxValue; 
        
        //public string methodName;

    }

    [System.Serializable]
    public class OptionCategoryData : SerializableDictionary<OptionCategoryType, OptionDataKey> { }

    [CreateAssetMenu(menuName = "SO/UI/OptionDataSO")]
    public class OptionDataSO : ScriptableObject
    {
        public List<OptionDataKey> OptionDataList = new List<OptionDataKey>(); 

        public OptionCategoryData optionDataDic = new OptionCategoryData(); 

        private void OnValidate()
        {
            foreach (var _type in Enum.GetValues(typeof(OptionCategoryType)))
            {
                OptionCategoryType type = (OptionCategoryType)_type;

                // -1 �̸� ���°� 
                int index = OptionDataList.FindIndex((x) => x.key == type);
                if (index == -1)
                {
                    OptionDataList.Add(new OptionDataKey{key = type});
                }
            }
        }

        [ContextMenu("ListToDic")]
        public void InputDataListToDic()
        {
            optionDataDic.Clear();
            foreach (var _obj in OptionDataList)
            {
                optionDataDic.Add(_obj.key, _obj);
            }
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

    }    
}

