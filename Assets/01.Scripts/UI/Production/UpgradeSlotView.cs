using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;
using DG.Tweening;
using System; 

namespace UI.Production
{
    public class UpgradeSlotView : AbUI_Base
    {
        enum Elements
        {
            image,
            active_mark,
            frame,
        }
    
        enum Labels
        {
            text
        }

        private bool isStackable; 

        // 프로퍼티 
        public bool IsStackable { get => isStackable; set { isStackable = value; ShowVisualElement(GetLabel((int)Labels.text), value); } }
        public VisualElement ActiveMark => GetVisualElement((int)Elements.active_mark);
        public VisualElement Image => GetVisualElement((int)Elements.image);
        public override void Cashing()
        {
            //base.Cashing();
            BindVisualElements(typeof(Elements));
            BindLabels(typeof(Labels));
        }

        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// 클릭시 이벤트 추가  
        /// </summary>
        /// <param name="_callback"></param>
        public void AddClickEvent(Action _callback)
        {
            AddElementEvent<ClickEvent>((int)Elements.image, _callback);
            //parentElement.RegisterCallback<MouseOverEvent>((e) => Debug.Log("ㄲ"));
        }
        // === UI 설정 관련 === //
        public void SetSpriteAndText(Texture2D _sprite, string _count)
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = new StyleBackground(_sprite);
            GetLabel((int)Labels.text).text = _count;
        }

        /// <summary>
        /// 선택시 깜빡깜빡 
        /// </summary>
        public void ActiveSelect()
        {

            //GetVisualElement((int)Elements.active_mark)
        }

        //public void 
    }

}

