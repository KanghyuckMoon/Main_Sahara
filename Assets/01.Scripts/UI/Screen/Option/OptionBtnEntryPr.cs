using System;
using System.Collections;
using System.Collections.Generic;
using UI.Popup;
using UI.Production;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Option
{
    public class OptionBtnEntryPr : IOptionEntry 
    {
        private OptionBtnEntryView optionBtnEntryView;
        private VisualElement parent;
        private OptionData optionData; 
        
        public OptionData OptionData  => optionData; 
        public VisualElement Parent => parent;
        public OptionBtnEntryPr()
        {
            var _pr = ConstructorManager.UIConstructorManager.Instance.GetProductionUI(typeof(OptionBtnEntryView));
            optionBtnEntryView = _pr.Item2 as OptionBtnEntryView;
            parent = _pr.Item1;
            optionBtnEntryView.AddButtonsEvent();
            
        }

        // 있는거 캐싱 
        public OptionBtnEntryPr(VisualElement _v)
        {
            optionBtnEntryView = new OptionBtnEntryView(_v);
            parent = _v;
            optionBtnEntryView.AddButtonsEvent();
        }


        // OptionData 받아두고 
        // OptionType으로 찾고 
        // 가져와서 
        public void SetData(Action _callback, string _name)
        {
            //this.optionData = _optionData; 
            //string _name  = TextMan
            optionBtnEntryView.SetName(_name);
            optionBtnEntryView.AddButtonEvent(_callback);
            // 드롭다운 값이 변경될 때 

        }

    }
    
}
