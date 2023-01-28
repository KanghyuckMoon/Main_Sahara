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
        public enum Buttons
        {
            start_button,
            end_button
        }


        private Dictionary<Buttons, Action> callbackDic = new Dictionary<Buttons, Action>(); 
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
            AddButtonEvent<ClickEvent>((int)Buttons.start_button, () => callbackDic[Buttons.start_button].Invoke());
            AddButtonEvent<ClickEvent>((int)Buttons.end_button, () => callbackDic[Buttons.end_button].Invoke());
        }

        public void AddButtonEventToDic(Buttons buttonType, Action callback)
        {
            callbackDic[buttonType] = callback;
        }
    }

}
