using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 
namespace UI
{
    [Serializable]
    public class TitleView : AbUI_Base
    {
        enum Buttons
        {
            start_button,
            end_button
        }

        enum ButtonCallbacks
        {
            start,
            end
        }

        private Dictionary<ButtonCallbacks, Action> callbackDic = new Dictionary<ButtonCallbacks, Action>(); 
        private Action a;
        public override void Cashing()
        {
            base.Cashing();
            BindButtons(typeof(Buttons));
        }

        public override void Init()
        {
            base.Init();
            AddButtonEvents(); 
        }

        private void AddButtonEvents()
        {
            AddButtonEvent<ClickEvent>((int)Buttons.start_button, () => Debug.Log("Ω√¿€"));
            AddButtonEvent<ClickEvent>((int)Buttons.end_button, () => Debug.Log("≥°"));
        }
    }

}
