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

        // �ִ°� ĳ�� 
        public OptionBtnEntryPr(VisualElement _v)
        {
            optionBtnEntryView = new OptionBtnEntryView(_v);
            parent = _v;
            optionBtnEntryView.AddButtonsEvent();
        }


        // OptionData �޾Ƶΰ� 
        // OptionType���� ã�� 
        // �����ͼ� 
        public void SetData(Action _callback, string _name)
        {
            //this.optionData = _optionData; 
            //string _name  = TextMan
            optionBtnEntryView.SetName(_name);
            optionBtnEntryView.AddButtonEvent(_callback);
            // ��Ӵٿ� ���� ����� �� 

        }

    }
    
}
