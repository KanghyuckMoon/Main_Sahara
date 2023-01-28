using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class TitleView : AbUI_Base
    {
        enum Buttons
        {
            start_button,
            end_button 
        }

        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Buttons));
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
