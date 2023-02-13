using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public enum TextKeyType
    {
        // 타이틀
        title = 0,
        titleStart,
        titleSetting,
        titleEnd,

        // 로딩 팁 
        loadingTip = 10, 
    }

    [System.Serializable]
    public class TextKeyData
    {
        public TextKeyType textKeyType;
        public string key; 
    }

    [CreateAssetMenu(menuName = "SO/TextKeySO")]
    public class TextKeySO : ScriptableObject
    {
        public List<TextKeyData> textKeyDataList = new List<TextKeyData>();

        public string FindKey(TextKeyType type)
        {
           return textKeyDataList.Find((x) => x.textKeyType == type).key;
        }
    }

}
