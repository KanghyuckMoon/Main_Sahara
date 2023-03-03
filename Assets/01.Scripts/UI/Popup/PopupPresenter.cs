using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.ConstructorManager;

namespace UI.Base
{
    public class PopupPresenter
    {
        private PopupView popupView;
        private VisualElement parent; 

        public PopupPresenter()
        {
            (VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(PopupView));
            this.popupView = _v.Item2 as PopupView;
            this.parent = _v.Item1;
        }
    }

}
