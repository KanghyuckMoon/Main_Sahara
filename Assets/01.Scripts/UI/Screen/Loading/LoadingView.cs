using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System; 

namespace UI.Screens
{
    [Serializable]
    public class LoadingView : AbUI_Base
    {
        enum Labels
        {
            tip_label
        }
        enum Elements
        {
            loading_icon
        }

        public override void Cashing()
        {
            base.Cashing();
            BindLabels(typeof(Labels));
            BindVisualElements(typeof(Elements));
        }

        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// 도움말 텍스트 설정 
        /// </summary>
        /// <param name="str"></param>
        public void SetLabelText(string str)
        {
            GetLabel((int)Labels.tip_label).text = str; 
        }

        public void LoopLoadingImg()
        {
            VisualElement _icon = GetVisualElement((int)Elements.loading_icon); 
            DOTween.To(() => 100f, x => _icon.style.opacity = new StyleFloat(x), 50,1f)
                .SetLoops(-1,LoopType.Yoyo).SetEase(Ease.OutQuad);
        }

        public void StopTween()
        {
            DOTween.KillAll(); 
        }
    }

}
