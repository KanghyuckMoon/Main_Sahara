using System;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace UI.Production
{
    public class OptionBtnEntryView : AbUI_Base
    {
        public enum Buttons
        {
            select_button, 
        }

        enum Labels
        {
            option_name, 
            value_label 
        }


        private Dictionary<Buttons, Action> callbackDic = new Dictionary<Buttons, Action>(); 

        public override void Cashing()
        {
            //base.Cashing();
            BindButtons(typeof(Buttons)); 
            BindLabels(typeof(Labels));
        }

        public override void Init()
        {
            base.Init();
            // 버튼 누르면 Dropdown 값 변경 
           
            AddButtonEventToDic(Buttons.select_button, clickCallback);
        }

        private Action clickCallback; 
        public void AddButtonEvent(Action _callback)
        {
         //   clickCallback = _callback; 
        // AddButtonEventToDic(Buttons.select_button, _callback);
         AddButtonEvent<ClickEvent>((int)Buttons.select_button ,_callback);
        }

        public OptionBtnEntryView()
        {
            callbackDic.Add(Buttons.select_button, null);
        }
        public OptionBtnEntryView(VisualElement _parent)
        {
            callbackDic.Add(Buttons.select_button, null);
            InitUIParent(_parent);
            Cashing();
            Init();
        }

        
        //== 텍스트 설정 ==// 
        public void SetName(string _nameStr)
        {
            GetLabel((int)Labels.option_name).text = _nameStr; 
        }

        public void SetValue(int _value)
        {
            GetLabel((int)Labels.value_label).text = _value.ToString(); 
        }
        
        //== 버튼 이벤트 설정 ==// 
        public void AddButtonsEvent()
        {
            AddButtonEvent<ClickEvent>((int)Buttons.select_button ,callbackDic[Buttons.select_button]);
        }
        
        public void RemoveButtonsEvent()
        {
            RemoveButtonEvent<ClickEvent>((int)Buttons.select_button ,callbackDic[Buttons.select_button]);
        }

        public void AddButtonEventToDic(Buttons _type ,Action _callback)
        {
            callbackDic[_type] = _callback; 
        }
    }
}