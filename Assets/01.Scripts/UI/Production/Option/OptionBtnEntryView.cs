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
            left_button,
            right_button
        }

        enum Labels
        {
            option_name, 
            value_label 
        }

        enum Dropdowns
        {
            dropdown, 
            
        }

        private Dictionary<Buttons, Action> callbackDic = new Dictionary<Buttons, Action>(); 

        // 프로퍼티
        public DropdownField DropDown => GetDropdown((int)Dropdowns.dropdown);
        public override void Cashing()
        {
            //base.Cashing();
            BindButtons(typeof(Buttons)); 
            BindLabels(typeof(Labels));
            BindDropdowns(typeof(Dropdowns));
        }

        public override void Init()
        {
            base.Init();
            // 버튼 누르면 Dropdown 값 변경 
           
            AddButtonEventToDic(Buttons.left_button, ChangeDropdownIdx);
            AddButtonEventToDic(Buttons.right_button, ChangeDropdownIdx);
        }

        private void ChangeDropdownIdx()
        {
            int _maxCount = DropDown.choices.Count;
            int changeIdx = Mathf.Clamp(DropDown.index + 1, 0, _maxCount);
            DropDown.index = changeIdx; 

        }

        public OptionBtnEntryView()
        {
            callbackDic.Add(Buttons.left_button, null);
            callbackDic.Add(Buttons.right_button, null);
        }
        public OptionBtnEntryView(VisualElement _parent)
        {
            callbackDic.Add(Buttons.left_button, null);
            callbackDic.Add(Buttons.right_button, null);    
            InitUIParent(_parent);
            Cashing();
            Init();
        }

        //== 드롭다운 설정 ==// 
        public void SetDropdown(List<string> _choices)
        {
            DropDown.choices = _choices;
        }

        public void SetDropdownEvent(Action<int> _callback)
        {
            DropDown.RegisterValueChangedCallback((x) => _callback(DropDown.index));
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
            AddButtonEvent<ClickEvent>((int)Buttons.left_button ,callbackDic[Buttons.left_button]);
            AddButtonEvent<ClickEvent>((int)Buttons.right_button ,callbackDic[Buttons.right_button]);
        }
        
        public void RemoveButtonsEvent()
        {
            RemoveButtonEvent<ClickEvent>((int)Buttons.left_button ,callbackDic[Buttons.left_button]);
            RemoveButtonEvent<ClickEvent>((int)Buttons.right_button ,callbackDic[Buttons.right_button]);
        }

        public void AddButtonEventToDic(Buttons _type ,Action _callback)
        {
            callbackDic[_type] = _callback; 
        }
    }
}