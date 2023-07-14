using System;
using System.Collections;
using System.Collections.Generic;
using UI.Production;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Option
{
    public class OptionBtnEntryPr 
    {
        private OptionBtnEntryView optionBtnEntryView;
        private VisualElement parent;

        public VisualElement Parent => parent; 
        public OptionBtnEntryPr()
        {
            var _pr = ConstructorManager.UIConstructorManager.Instance.GetProductionUI(typeof(OptionBtnEntryView));
            optionBtnEntryView = _pr.Item2 as OptionBtnEntryView;
            parent = _pr.Item1;
            optionBtnEntryView.AddButtonsEvent();
        }

        // ÀÖ´Â°Å Ä³½Ì 
        public OptionBtnEntryPr(VisualElement _v)
        {
            optionBtnEntryView = new OptionBtnEntryView(_v);
            parent = _v;
            optionBtnEntryView.AddButtonsEvent();
        }


        public void SetData(Action<int> _callback, string _nameAddress)
        {
            //string _name  = TextMan
            optionBtnEntryView.SetName(_nameAddress);
            optionBtnEntryView.AddButtonEventToDic(OptionBtnEntryView.Buttons.left_button, () =>
            {
                _callback?.Invoke(-1);
            });
            optionBtnEntryView.AddButtonEventToDic(OptionBtnEntryView.Buttons.right_button,() =>
            {
                _callback?.Invoke(+1);
            });
        }
    }
    
}
