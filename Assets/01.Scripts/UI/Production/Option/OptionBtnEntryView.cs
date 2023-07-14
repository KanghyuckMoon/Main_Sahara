using System;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Production
{
    public class OptionBtnEntryView : AbUI_Base
    {
        public enum Buttons
        {
            left_button,
            right_button
        }

        enum Labels
        {
            name_label, 
            value_label 
        }

        private Dictionary<Buttons, Action> callbackDic = new Dictionary<Buttons, Action>(); 

        public override void Cashing()
        {
            base.Cashing();
            BindButtons(typeof(Buttons)); 
            BindLabels(typeof(Labels));
        }

        public override void Init()
        {
            base.Init();
        }

        public OptionBtnEntryView()
        {
            
        }
        public OptionBtnEntryView(VisualElement _parent)
        {
            InitUIParent(_parent);
            Cashing();
            Init();
        }

        //== 텍스트 설정 ==// 
        public void SetName(string _nameStr)
        {
            GetLabel((int)Labels.name_label).text = _nameStr; 
        }

        public void SetValue(int _value)
        {
            GetLabel((int)Labels.value_label).text = _value.ToString(); 
        }
        
        //== 버튼 이벤트 설정 ==// 
        public void AddButtonsEvent()
        {
            AddElementEvent<ClickEvent>((int)Buttons.left_button ,callbackDic[Buttons.left_button]);
            AddElementEvent<ClickEvent>((int)Buttons.right_button ,callbackDic[Buttons.right_button]);
        }
        
        public void RemoveButtonsEvent()
        {
            RemoveElementEvent<ClickEvent>((int)Buttons.left_button ,callbackDic[Buttons.left_button]);
            RemoveElementEvent<ClickEvent>((int)Buttons.right_button ,callbackDic[Buttons.right_button]);
        }

        public void AddButtonEventToDic(Buttons _type ,Action _callback)
        {
            callbackDic[_type] = _callback; 
        }
    }
}