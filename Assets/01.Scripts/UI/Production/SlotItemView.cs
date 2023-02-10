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

        private int index; 
        private bool isStackable; // 셀수 있냐 

        // 프로퍼티 
        public VisualElement Item => GetVisualElement((int)Elements.item);
        public bool IsStackable { get => isStackable; set { isStackable = value; ShowVisualElement(GetLabel((int)Labels.text), value); } }
        public Texture2D ItemSprite => GetVisualElement((int)Elements.image).style.backgroundImage.value.texture;
        public int ItemCount => int.Parse(GetLabel((int)Labels.text).text);
        public int Index => index; 

        public SlotItemView()
        {

        }
        public SlotItemView(VisualElement _parent, int _index)
        {
            InitUIParent(_parent);
            Cashing();
            Init();
            this.index = _index; 
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

        public void AddDragger(VisualElement _target, Action startCallback)
        {
            GetVisualElement((int)Elements.item).AddManipulator(new Dragger(_target, startCallback));
        }
        public void AddDropper(Action _dropCallback)
        {
            GetVisualElement((int)Elements.item).AddManipulator(new Dropper((x) =>
            {
                ShowVisualElement(parentElement, false);
                _dropCallback?.Invoke();
            }));
        }
        private void EndDrag(Vector2 _v)
        {
            Debug.Log("끝 " + _v);
        }

        public void RemoveView()
        {
            parentElement.RemoveFromHierarchy();
        }
        public void SetStackable(bool _isStackable)
        {
            ShowVisualElement(GetVisualElement((int)Labels.text), _isStackable);
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
