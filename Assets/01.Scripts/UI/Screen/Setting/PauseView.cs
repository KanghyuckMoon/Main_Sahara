using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;

namespace UI.Option
{
    [System.Serializable]   
    public class PauseView : AbUI_Base
    {
        enum Elements
        {
        
        }

        public enum Buttons
        {
            continue_button, 
            option_button,
            exit_button
        }
        enum Labels
        {
        
        }
        
        private Dictionary<Buttons, Action> callbackDic = new Dictionary<Buttons, Action>(); 

        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
            BindButtons(typeof(Buttons));
            //    BindLabels(typeof(Elements));
        }

        public override void Init()
        {
            base.Init();
            AddButtonEvents(); 
        }
        
        private void AddButtonEvents()
        {
            AddButtonEvent<ClickEvent>((int)Buttons.continue_button, callbackDic[Buttons.continue_button]);
            AddButtonEvent<ClickEvent>((int)Buttons.option_button, callbackDic[Buttons.option_button]);
            AddButtonEvent<ClickEvent>((int)Buttons.exit_button, callbackDic[Buttons.exit_button]);
        }

        public void RemoveButtonEvents()
        {
            RemoveButtonEvent<ClickEvent>((int)Buttons.continue_button, callbackDic[Buttons.continue_button]);
            RemoveButtonEvent<ClickEvent>((int)Buttons.option_button, callbackDic[Buttons.option_button]);
            RemoveButtonEvent<ClickEvent>((int)Buttons.exit_button, callbackDic[Buttons.exit_button]);
        }
        
        public void AddButtonEventToDic(Buttons buttonType, Action callback)
        {
            callbackDic[buttonType] = callback;
        }
    }
}