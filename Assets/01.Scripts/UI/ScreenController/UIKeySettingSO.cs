using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base; 

namespace UI
{

    public class UIKeyInfo
    {
        public KeyCode keyCode;
        public ScreenType screenType;


    }

    [CreateAssetMenu(menuName = "SO/UI/UIKeySettingSO")]
    public class UIKeySettingSO : ScriptableObject
    {
        public List<UIKeyInfo> uiKeyInfoList = new List<UIKeyInfo>(); 
        
        //public ScreenType
    }

}
