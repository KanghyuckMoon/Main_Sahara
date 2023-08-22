
using System.Collections;
using System.Collections.Generic;
using Codice.Client.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class ToolkitSlider
    {
        private SliderInt slider;
        private Label label;

        public SliderInt Slider => slider;
        public Label ValueText => label; 
        public ToolkitSlider(SliderInt _slider, Label _label)
        {
            this.slider = _slider;
            this.label = _label;
            
            this.slider.RegisterValueChangedCallback((x) =>
            {
                _label.text = x.newValue.ToString(); 
            });
        }
    }
    
}
