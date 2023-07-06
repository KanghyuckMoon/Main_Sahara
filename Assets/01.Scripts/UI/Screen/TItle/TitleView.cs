using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using GoogleSpreadSheet;
using UI.Manager; 
using UI.Base;

namespace UI
{
    [Serializable]
    public class TitleView : AbUI_Base
    {
        enum Labels
        {
            title_label
        }

        public enum Buttons
        {
            start_button,
            //setting_button,
            end_button
        }


        private Dictionary<Buttons, Action> callbackDic = new Dictionary<Buttons, Action>(); 
        private Action a;
        public override void Cashing()
        {
            base.Cashing();
            BindLabels(typeof(Labels));
            BindButtons(typeof(Buttons));
        }

        public override void Init()
        {
            base.Init();
            GetLabel((int)Labels.title_label).text = TextManager.Instance.GetText(UIManager.Instance.TextKeySO.FindKey(TextKeyType.title));;
            GetButton((int)Buttons.start_button).text = TextManager.Instance.GetText(UIManager.Instance.TextKeySO.FindKey(TextKeyType.titleStart));
            //GetButton((int)Buttons.setting_button).text = TextManager.Instance.GetText(UIManager.Instance.TextKeySO.FindKey(TextKeyType.titleSetting));
            GetButton((int)Buttons.end_button).text = TextManager.Instance.GetText(UIManager.Instance.TextKeySO.FindKey(TextKeyType.titleEnd));
            AddButtonEvents();
        }

        private void AddButtonEvents()
        {
            AddButtonEvent<ClickEvent>((int)Buttons.start_button, callbackDic[Buttons.start_button]);
            AddButtonEvent<ClickEvent>((int)Buttons.end_button, callbackDic[Buttons.end_button]);
        }

        public void RemoveButtonEvents()
        {
            RemoveButtonEvent<ClickEvent>((int)Buttons.start_button, callbackDic[Buttons.start_button]);
            RemoveButtonEvent<ClickEvent>((int)Buttons.end_button, callbackDic[Buttons.end_button]);

        }
        
        public void AddButtonEventToDic(Buttons buttonType, Action callback)
        {
            callbackDic[buttonType] = callback;
        }
    }

}
