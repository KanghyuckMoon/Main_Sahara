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
        
        // 캐싱해줄 요소들 
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
         
            // 생성 
            _frontBarView = new SliderView(Get<ProgressBar>((int)ProgressBars.front_bar));
            _backBarView = new SliderView(Get<ProgressBar>((int)ProgressBars.back_bar));
        }

        /// <summary>
        /// hp바 UI업데이트
        /// </summary>
        /// <param name="v"></param>
        public void SetBarUI(float endV)
        {
            float _fV = _frontBarView.Bar.value; // 앞쪽 바 시작 value
            float _bV = _backBarView.Bar.value; // 뒤쪽 바 시작 value

            Sequence seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => _fV, (x) => _frontBarView.Bar.value = x, endV,0.5f));
            seq.Append(DOTween.To(() => _bV, (x) => _backBarView.Bar.value = x, endV,0.3f));

            //_frontBarView.SetSlider(endV);
           // _backBarView.SetSlider(endV); 
        }
    }

}

