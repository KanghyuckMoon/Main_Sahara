using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;

namespace UI.Option
{
    [System.Serializable]   
    public class OptionView : AbUI_Base
    {
        enum Elements
        {
        
        }

        enum Labels
        {
        
        }
        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
        //    BindLabels(typeof(Elements));
        }

        public override void Init()
        {
            base.Init();
        }
        
    }
}