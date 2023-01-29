using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI.Screens
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
        public List<TipInfo> tipList = new List<TipInfo>();
    }

}


