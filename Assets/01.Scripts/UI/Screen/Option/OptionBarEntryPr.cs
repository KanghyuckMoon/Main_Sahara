using System;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Production;


namespace UI.Option
{
    
    public class OptionBarEntryPr  : IOptionEntry 
    {
        private OptionBarEntryView optionBarEntryView;
        private VisualElement parent;
        private OptionData optionData;

        // 프로퍼티 
        public SliderInt Slider => optionBarEntryView.Slider; 
        public Label Text => optionBarEntryView.ValueText; 
        
        public OptionData OptionData => optionData;
        public VisualElement Parent => parent; 

        public OptionBarEntryPr()
        {
            var _pr = ConstructorManager.UIConstructorManager.Instance.GetProductionUI(typeof(OptionBarEntryView));
            optionBarEntryView = _pr.Item2 as OptionBarEntryView;
            parent = _pr.Item1;
            AddEvent(() => Text.text = Slider.value.ToString()); 
            // bar 
            // 기본 값 설정 
        }

        // 있는거 캐싱 
        public OptionBarEntryPr(VisualElement _v)
        {
            optionBarEntryView = new OptionBarEntryView(_v);
            parent = _v;
        }

        public void SetOptionData(OptionData _optionData)
        {
            this.optionData = _optionData;
                optionBarEntryView.SetOptionLabel(optionData.name);
                Slider.lowValue = _optionData.minValue;
                Slider.highValue = _optionData.maxValue;

        }
        public void AddEvent(Action _callback)
        {
            Slider.RegisterValueChangedCallback((x) => _callback?.Invoke()); 
            
        }

    }
    
}
