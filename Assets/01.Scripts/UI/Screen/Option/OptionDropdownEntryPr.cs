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

        // 있는거 캐싱 
        public OptionDropdownEntryPr(VisualElement _v)
        {
            _optionDropEntryView = new OptionDropEntryView(_v);
            parent = _v;
            _optionDropEntryView.AddButtonsEvent();
        }


        // OptionData 받아두고 
        // OptionType으로 찾고 
        // 가져와서 
        public void SetData(Action<int> _callback, string _nameAddress, List<string> dropdownList, OptionData _optionData)
        {
            this.optionData = _optionData; 
            //string _name  = TextMan
            _optionDropEntryView.SetName(_nameAddress);
            _optionDropEntryView.SetDropdown(dropdownList);
            // 드롭다운 값이 변경될 때 
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
