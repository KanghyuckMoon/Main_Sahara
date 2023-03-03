using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 

namespace UI.Base
{
    public class PopupView : AbUI_Base
    {
        enum Labels
        {
            text_label,

        }
        enum Buttons
        {
            accept_button, 
            cancel_button
        }

        public override void Cashing()
        {
            //base.Cashing();
            BindLabels(typeof(Labels));
            BindButtons(typeof(Buttons)); 
        }

        public override void Init()
        {
            base.Init();
        }

        public void SetScale(bool _isActive)
        {
            float _v = _isActive ? 1f : 0f; 
            parentElement.style.scale = new StyleScale(new Scale(new Vector2(_v,_v))); 
        }
        public void SetText(string _str)
        {
            GetLabel((int)Labels.text_label).text = _str; 
        }
        public void AddAcceptBtnEvent(Action _callback)
        {
            AddButtonEvent<ClickEvent>((int)Buttons.accept_button, _callback);
        }
        public void AddCancelBtnEvent(Action _callback)
        {
            AddButtonEvent<ClickEvent>((int)Buttons.cancel_button, _callback);
        }

    }
}

