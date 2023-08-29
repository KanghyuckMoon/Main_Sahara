using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Popup
{
    [System.Serializable]
    public class PopupTutorialScreenView : AbUI_Base
    {
        enum Elements
        {
            popup_parent, 
        }
        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
        }

        public override void Init()
        {
            base.Init();
        }

        public void SetParent(VisualElement _v)
        {
            GetVisualElement((int)Elements.popup_parent).Add(_v);
        }
    }    
}