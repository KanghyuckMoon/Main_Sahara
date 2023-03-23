using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base; 

namespace UI.Popup
{
    [System.Serializable]
    public class PopupHudView : AbUI_Base
    {
        enum Elements
        {
            itemget_popup_parent, 
        }

        public VisualElement PopupParent => GetVisualElement((int)Elements.itemget_popup_parent);
        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
        }

        public void SetParent(VisualElement _v)
        {
            PopupParent.Add(_v);   
        }

        public void Remove(VisualElement _v)
        {
            _v.RemoveFromHierarchy();
        }
    }

}
