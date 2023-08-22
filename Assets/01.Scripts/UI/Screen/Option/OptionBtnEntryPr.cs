using System;
using System.Collections;
using System.Collections.Generic;
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
        public void SetData(Action<int> _callback, string _nameAddress, List<string> dropdownList, OptionData _optionData)
        {
            this.optionData = _optionData; 
            //string _name  = TextMan
            optionBtnEntryView.SetName(_nameAddress);
            optionBtnEntryView.SetDropdown(dropdownList);
            // ��Ӵٿ� ���� ����� �� 
            optionBtnEntryView.SetDropdownEvent(_callback); 
            
            /*optionBtnEntryView.AddButtonEventToDic(OptionBtnEntryView.Buttons.left_button, () =>
            {
                _callback?.Invoke(-1);
            });
            optionBtnEntryView.AddButtonEventToDic(OptionBtnEntryView.Buttons.right_button,() =>
            {
                _callback?.Invoke(+1);
            });*/
        }

    }
    
}
