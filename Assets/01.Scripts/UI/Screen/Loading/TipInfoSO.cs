using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI.Loading
{
    [Serializable]
    public class TipInfo
    {
        //public int idx;
        [TextArea(3, 7)]
        public string tip;
    }

    [CreateAssetMenu(menuName = "SO/TipInfoSO")]
    public class TipInfoSO : ScriptableObject
    {
        //public List<TipInfo> tipList = new List<TipInfo>();

        [Header("스프레드시트에서 가져올 팁 개수")]
        public int count; 
        [TextArea(3, 7)]
        public List<string> tipList = new List<string>();

        public void ClearList()
        {
            tipList.Clear(); 
        }
        public void AddTip(string _tip)
        {
           tipList.Add(_tip); 
        }
        
    }

}


