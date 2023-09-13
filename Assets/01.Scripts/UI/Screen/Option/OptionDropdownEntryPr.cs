using System;
using System.Collections;
using System.Collections.Generic;
using UI.Production;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Option
{
    public class OptionDropdownEntryPr : IOptionEntry 
    {
        private OptionDropEntryView _optionDropEntryView;
        private VisualElement parent;
        private OptionData optionData; 
        
        public OptionData OptionData  => optionData; 
        public VisualElement Parent => parent;
        public DropdownField Dropdown => _optionDropEntryView.DropDown; 
        public OptionDropdownEntryPr()
        {
            var _pr = ConstructorManager.UIConstructorManager.Instance.GetProductionUI(typeof(OptionDropEntryView));
            _optionDropEntryView = _pr.Item2 as OptionDropEntryView;
            parent = _pr.Item1;
            _optionDropEntryView.AddButtonsEvent();
            
        }

        // �ִ°� ĳ�� 
        public OptionDropdownEntryPr(VisualElement _v)
        {
            _optionDropEntryView = new OptionDropEntryView(_v);
            parent = _v;
            _optionDropEntryView.AddButtonsEvent();
        }


        // OptionData �޾Ƶΰ� 
        // OptionType���� ã�� 
        // �����ͼ� 
        public void SetData(Action<int> _callback, string _nameAddress, List<string> dropdownList, OptionData _optionData)
        {
            this.optionData = _optionData; 
            //string _name  = TextMan
            _optionDropEntryView.SetName(_nameAddress);
            _optionDropEntryView.SetDropdown(dropdownList);
            // ��Ӵٿ� ���� ����� �� 
            _optionDropEntryView.SetDropdownEvent(_callback);
            _optionDropEntryView.DropDown.index =
                _optionData.dropdownList.IndexOf(_optionData
                    .defaultDropdownStr);
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
