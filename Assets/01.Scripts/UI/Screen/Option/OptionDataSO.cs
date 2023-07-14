using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Option
{
    public enum OptionModifyType
    {
        Button, 
        Dropdown
    }

    public enum OntinoType
    {
        
    }

    public enum OptionCategoryType
    {   
        Graphics, 
        Sound, 
        GameInfo, 
        Help 
    }
    public class OptionData
    {
        public string optionType;
        public string optionModifyType;
        public string optionCategoryType;
        public string name; 
    }
    public class OptionDataSO : ScriptableObject
    {
        public Dictionary<string, OptionData> optionDataDic = new Dictionary<string, OptionData>(); 

    }    
}

