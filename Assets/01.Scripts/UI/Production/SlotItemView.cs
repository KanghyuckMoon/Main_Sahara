using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using UnityEngine.UIElements;
using System;


namespace UI.Production
{
    public class SlotItemUIData
    {
        public int count;
        public Texture2D sprite;
    }

    public class SlotItemView : AbUI_Base
    {
        enum Elements
        {
            image,
            item
        }
        enum Labels
        {
            text
        }

        private bool isStackable; // 셀수 있냐 
        private Manipulator curManipulator;

        // 프로퍼티 
        public VisualElement Item => GetVisualElement((int)Elements.item);
        public bool IsStackable { get => isStackable; set { isStackable = value; ShowVisualElement(GetLabel((int)Labels.text), value); } }
        public Texture2D ItemSprite => GetVisualElement((int)Elements.image).style.backgroundImage.value.texture;
        public int ItemCount => int.Parse(GetLabel((int)Labels.text).text);
        public Rect SlotWorldBound => Item.worldBound;   
        public SlotItemView()
        {

        }
        public SlotItemView(VisualElement _parent)
        {
            InitUIParent(_parent);
            Cashing();
            Init();
        }
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
        
        public void AddManipulator(MouseManipulator _manipulator)
        {
            this.curManipulator = _manipulator; 
            GetVisualElement((int)Elements.item).AddManipulator(_manipulator);
        }

        public void RemoveCurManipulator()
        {
            if (curManipulator == null) return; 
            GetVisualElement((int)Elements.item).AddManipulator(curManipulator);
            this.curManipulator = null;
        }

        private Action mouseOverEvt = null;
        private Action mouseOutEvt = null;
        private Action clickEvt = null; 
        public void AddClickEvent(Action _callback)
        {
            this.clickEvt = _callback;
            AddElementEvent<ClickEvent>((int)Elements.image, _callback);
        }
        public void AddHoverEvent(Action _callback)
        {
            this.mouseOverEvt = _callback;
            AddElementEvent<MouseOverEvent>((int)Elements.image, _callback); 
        }
        public void AddOutEvent(Action _callback)
        {
            this.mouseOutEvt = _callback;
            AddElementEvent<MouseOutEvent>((int)Elements.image, _callback);
        }

        public void RemoveEvent()
        {
            RemoveElementEvent<ClickEvent>((int)Elements.image, clickEvt);
            RemoveElementEvent<MouseOverEvent>((int)Elements.image, mouseOverEvt);
            RemoveElementEvent<MouseOutEvent>((int)Elements.image, mouseOutEvt);
        }
        public void RemoveView()
        {
            parentElement.RemoveFromHierarchy();
        }
        
        // === UI 설정 관련 === //
        public void ClearUI()
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = null;
            GetLabel((int)Labels.text).text = ""; 
        }

        public void SetSpriteAndText(Texture2D _sprite, int _count)
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = new StyleBackground(_sprite);
            GetLabel((int)Labels.text).text = _count.ToString();
        }
        public void SetSprite(Texture2D _sprite)
        {
            Debug.Log(_sprite.name);
            GetVisualElement((int)Elements.image).style.backgroundImage = new StyleBackground(_sprite);
        }

        public void SetText(int _count)
        {
            GetLabel((int)Labels.text).text = _count.ToString();
        }

    }

}
