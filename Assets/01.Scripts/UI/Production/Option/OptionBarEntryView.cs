using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using UnityEngine.UIElements; 

namespace UI.Production
{
    public class OptionBarEntryView : AbUI_Base
    {
        enum Sliders
        {
            slider,
        }

        enum Labels
        {
            value_label,
            option_name 
        }

        // 프로퍼티 
        public SliderInt Slider => GetSliderInt((int)Sliders.slider);
        public Label ValueText => GetLabel((int)Labels.value_label); 
        
        public override void Cashing()
        {
            //   base.Cashing();
            BindSliderInts(typeof(Sliders));
            BindLabels(typeof(Labels));
        }
     
        public OptionBarEntryView()
        {   
        }
        public OptionBarEntryView(VisualElement _parent)
        {
            InitUIParent(_parent);
            Cashing();
            Init();
        }

        public void SetOptionLabel(string _optionStr)
        {
            GetLabel((int)Labels.option_name).text = _optionStr; 
        }
    }

}
