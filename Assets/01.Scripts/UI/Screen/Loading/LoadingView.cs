using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System;
using UI.Base;

namespace UI.Loading
{
    [Serializable]
    public class LoadingView : AbUI_Base
    {
        [SerializeField, Header("로딩팁 유지 시간")]
        private float time = 1f; 
        enum Labels
        {
            tip_label
        }
        enum Elements
        {
            loading_icon,
            panels
        }

        enum Decos
        {
            deco_1 =2,
            deco_2,
            deco_3,
            deco_4,
        }

        public VisualElement Panels => GetVisualElement((int)Elements.panels);
        public override void Cashing()
        {
            base.Cashing();
            BindLabels(typeof(Labels));
            BindVisualElements(typeof(Elements));
            BindVisualElements(typeof(Decos));
        }

        public override void Init()
        {
            base.Init();
            InitDecosStyle();
            Panels.style.opacity = 0f; 
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
            DOTween.To(() => 1f, x => _icon.style.opacity = new StyleFloat(x), 0.5f, time)
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutQuad);

//            DOTween.To(() => 1f, x => _icon.style.rotate = new StyleRotate(new Rotate(x)), 0.5f, time)
 //               .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutQuad);
        }

        public void StopTween()
        {
            DOTween.KillAll();
        }

        public List<VisualElement> GetDecos()
        {
            List<VisualElement> _list = new List<VisualElement>(); 
            foreach (var _deco in Enum.GetValues(typeof(Decos)))
            {
                _list.Add(GetVisualElement((int)_deco));
            }
            return _list; 
        }

        public void InitDecosStyle()
        {
            bool _isUp = true;
            foreach (var _deco in Enum.GetValues(typeof(Decos)))
            {
                VisualElement _e = GetVisualElement((int)_deco);
                if (_isUp == true)
                {
                    //GetVisualElement((int)_deco).AddToClassList("panel_deco_top");
                    DOTween.To(() => _e.transform.position, (x) => _e.transform.position = x, new Vector3(-3000,0,0), 0f);
                    //GetVisualElement((int)_deco).style.translate = new StyleTranslate(new Translate(3000, 0));
                    _isUp = false;
                    continue;
                }
                // GetVisualElement((int)_deco).AddToClassList("panel_deco_bot");
                DOTween.To(() => _e.transform.position, (x) => _e.transform.position = x, new Vector3(3000, 0, 0), 0f);

//                GetVisualElement((int)_deco).style.translate = new StyleTranslate(new Translate(-3000, 0));
                _isUp = true;
            }
        }


    }

}
