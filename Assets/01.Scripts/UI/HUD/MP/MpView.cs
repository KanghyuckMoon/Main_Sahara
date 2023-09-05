using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 
using UI.Base;

namespace UI
{
    [Serializable]
    public class MpView : AbUI_Base
    {
        private SliderView _frontBarView;
        private SliderView _backBarView;

        // 캐싱해줄 요소들 
        enum Elements
        {
            mp_frame,
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
            _frontBarView = new SliderView(Get<ProgressBar>((int)ProgressBars.front_bar));
            _backBarView = new SliderView(Get<ProgressBar>((int)ProgressBars.back_bar));
        }

        /// <summary>
        /// hp바 UI업데이트
        /// </summary>
        /// <param name="v"></param>
        public void SetBarUI(float v)
        {
            _frontBarView.SetSlider(v);
            _backBarView.SetSlider(v);
        }

        public void SetMpText(float _curMp, float _maxMp)
        {
            GetLabel((int)Labels.cur_mp_text).text = _curMp.ToString(); 
            GetLabel((int)Labels.max_mp_text).text = _maxMp.ToString(); 
        }
    }

}

