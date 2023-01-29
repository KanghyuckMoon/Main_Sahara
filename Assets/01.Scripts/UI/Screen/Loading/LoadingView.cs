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
        public void SetTipText(string _str, float _delay)
        {
            Label _tip = GetLabel((int)Labels.tip_label);
            Sequence seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => 1f, x => _tip.style.opacity = x, 0f, _delay * 0.5f).SetEase(Ease.OutQuart));
            seq.AppendCallback(() => _tip.text = _str);
            seq.Append(DOTween.To(() => 0f, x => _tip.style.opacity = x, 1f, _delay * 0.5f).SetEase(Ease.InQuart));
        }

        public void LoopLoadingImg()
        {
            VisualElement _icon = GetVisualElement((int)Elements.loading_icon);
            DOTween.To(() => 1f, x => _icon.style.opacity = new StyleFloat(x), 0.5f, 1.4f)
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutQuad);
        }

        public void StopTween()
        {
            DOTween.KillAll();
        }
    }

}
