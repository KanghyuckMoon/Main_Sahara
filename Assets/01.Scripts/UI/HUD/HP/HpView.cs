using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using DG.Tweening; 

namespace UI
{
    [Serializable]
    public class HpView : AbUI_Base
    {
        private SliderView _frontBarView;
        private SliderView _backBarView;
        
        // ĳ������ ��ҵ� 
        enum Elements
        {
            hp_frame,   
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
        public void SetBarUI(float endV)
        {
            float _fV = _frontBarView.Bar.value; // ���� �� ���� value
            float _bV = _backBarView.Bar.value; // ���� �� ���� value

            Sequence seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => _fV, (x) => _frontBarView.Bar.value = x, endV,0.5f));
            seq.Append(DOTween.To(() => _bV, (x) => _backBarView.Bar.value = x, endV,0.3f));

            //_frontBarView.SetSlider(endV);
           // _backBarView.SetSlider(endV); 
        }
    }

}

