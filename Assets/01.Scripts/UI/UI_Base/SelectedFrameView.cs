using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

namespace UI.Base
{
    public class SelectedFrameView : AbUI_Base
    {
        enum Elements
        {
            active_mark
        }

        private VisualElement parent; 
        public SelectedFrameView(VisualElement _parent)
        {
            this.parent = _parent;
            InitUIParent(_parent);
            Cashing();
            Init(); 
        }

        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
        }

        public override void Init()
        {
            base.Init();
        }
        // 애니메이션 와리가리 

        /// <summary>
        /// 애니메이션 루프 시작 
        /// </summary>
        public void StartAnimateLoop()
        {
            GetVisualElement((int)Elements.active_mark).RegisterCallback<TransitionEndEvent>((x) => SetScale());
        }

        public void StopAnimateLoop()
        {
            GetVisualElement((int)Elements.active_mark).UnregisterCallback<TransitionEndEvent>((x) => SetScale());
        }

        private bool isScaleUp = false; 
        private void SetScale()
        {
            GetVisualElement((int)Elements.active_mark).transform.scale = isScaleUp ? new Vector2(1.5f, 1.5f) : new Vector2(1f, 1f); 
        }
    }

}

