using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;
using UI.Production;
using UI.ConstructorManager; 

namespace UI.Popup
{
    public class PopupGetItemPr : MonoBehaviour, IPopup
    {
        private PopupGetItemView popupGetItemView;
        private VisualElement parent; 

        public PopupGetItemPr(VisualElement _v)
        {
            var _prod = UIConstructorManager.Instance.GetProductionUI(typeof(PopupGetItemView));
            this.popupGetItemView = _prod.Item2 as PopupGetItemView;
            this.parent = _prod.Item1; 
        }
        public void Active()
        {
        }

        public void Undo()
        {
        }
    }

}
