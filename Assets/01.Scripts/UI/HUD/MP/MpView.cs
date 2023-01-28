using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 

namespace UI
{
    [Serializable]
    public class MpView : AbUI_Base
    {
        private SliderView _frontBarView;
        private SliderView _backBarView;

        // ĳ������ ��ҵ� 
        enum Elements
        {
            mp_frame,
        }
        enum ProgressBars
        {
            front_bar,
            back_bar
        }

        public override void Cashing()
        {
            base.Cashing();
            Bind<VisualElement>(typeof(Elements));
            Bind<ProgressBar>(typeof(ProgressBars));
        }

        public override void Init()
        {
            base.Init();

            // ���� 
            _frontBarView = new SliderView(Get<ProgressBar>((int)ProgressBars.front_bar));
            _backBarView = new SliderView(Get<ProgressBar>((int)ProgressBars.back_bar));
        }

        /// <summary>
        /// hp�� UI������Ʈ
        /// </summary>
        /// <param name="v"></param>
        public void SetBarUI(float v)
        {
            _frontBarView.SetSlider(v);
            _backBarView.SetSlider(v);
        }
    }

}

