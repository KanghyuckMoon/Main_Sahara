using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using DG.Tweening; 
using UI.Base;

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
            //line 
        }
        enum ProgressBars
        {
            front_bar,
            back_bar
        }
        enum Labels
        {
            max_mp_text,
            cur_mp_text
        }
        // 프로퍼티 
        //public VisualElement AccentLine => GetVisualElement((int)Elements.line);
        //private StyleTranslate LineTrm => AccentLine.style.translate; 
        public override void Cashing()
        {
            base.Cashing();
            Bind<VisualElement>(typeof(Elements));
            Bind<ProgressBar>(typeof(ProgressBars));
            BindLabels(typeof(Labels));
        }

        public override void Init()
        {
            base.Init();
         
            // 생성 
     //       _frontBarView = new SliderView(Get<ProgressBar>((int)ProgressBars.front_bar));
      //      _backBarView = new SliderView(Get<ProgressBar>((int)ProgressBars.back_bar));

            _frontBarView = new SliderView(parentElement,"front_bar");
            _backBarView = new SliderView(parentElement,"back_bar");
        }

        /// <summary>
        /// hp바 UI업데이트
        /// </summary>
        /// <param name="v"></param>
        public void SetBarUI(float endV)
        {
            float _fV = _frontBarView.BarV; // 앞쪽 바 시작 value
            float _bV = _backBarView.BarV; // 뒤쪽 바 시작 value

            Sequence seq = DOTween.Sequence();
            seq.Append(DOTween.To(()    => _fV, (x) => _frontBarView.Bar.value = x, endV,0.5f));
            /*if (AccentLine != null)
            {
                float _x = _frontBarView.Bar.resolvedStyle.width;
                float _targetX = _x *  endV;

                seq.Join(DOTween.To(() => AccentLine.style.translate.value.x.value, (x) => AccentLine.style.translate 
                        = new Translate(x,LineTrm.value.y, LineTrm.value.z),
                    _targetX, 0.5f));
            }*/
            seq.Append(DOTween.To(() => _bV, (x) => _backBarView.Bar.value = x, endV,0.3f));

            //
            
            //_frontBarView.SetSlider(endV);
            // _backBarView.SetSlider(endV); 
        }

        public void Test(float endV)
        {
        //      _frontBarView.Bar.value = endV;
            _frontBarView.Bar.style.display = _frontBarView.Bar.resolvedStyle.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
        }
        public void SetMpText(float _curMp, float _maxMp)
        {
            GetLabel((int)Labels.cur_mp_text).text = _curMp.ToString(); 
            GetLabel((int)Labels.max_mp_text).text = _maxMp.ToString(); 
        }
    }

}

