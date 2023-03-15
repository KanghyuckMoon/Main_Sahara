using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.ConstructorManager;
using UI.Production; 

namespace UI.Popup
{
    public class PopupAcceptPresenter
    {
        private PopupAcceptView popupView;
        private VisualElement parent; 

        public PopupAcceptPresenter()
        {
            (VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(PopupAcceptView));
            this.popupView = _v.Item2 as PopupAcceptView;
            this.parent = _v.Item1;
        }
    }

}
