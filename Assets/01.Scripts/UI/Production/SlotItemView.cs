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
            item,
            select,
            slot_border, 
            //frame
        }
        enum Labels
        {
            text
        }   

        private bool isStackable; // 셀수 있냐 
        private List<Manipulator> manipulatorList = new List<Manipulator>(); 
        private Manipulator curManipulator;
        private const string selectStr = "active_select";
        private const string activeBorderStr = "active_slot_border";
        
        // 프로퍼티 
        public VisualElement Item => GetVisualElement((int)Elements.item);
        public bool IsStackable { get => isStackable; set { isStackable = value; ShowVisualElement(GetLabel((int)Labels.text), value); } }
        public Texture2D ItemSprite => GetVisualElement((int)Elements.image).style.backgroundImage.value.texture;
        public int ItemCount => int.Parse(GetLabel((int)Labels.text).text);
        public Rect SlotWorldBound => Item.worldBound;
        public VisualElement Select => GetVisualElement((int)Elements.select);
       
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
            //this.curManipulator = _manipulator; 
            manipulatorList.Add(_manipulator);
            GetVisualElement((int)Elements.item).AddManipulator(_manipulator);
        }

        public void ClearManipulator()
        {
            //if (curManipulator == null) return; 
            //GetVisualElement((int)Elements.item).AddManipulator(curManipulator);
            //this.curManipulator = null;

            foreach (var _manipulator in manipulatorList)
            {
                GetVisualElement((int)Elements.item).RemoveManipulator(_manipulator);
            }
            manipulatorList.Clear();
        }

        private Action mouseOverEvt = null;
        private Action mouseOutEvt = null;
        private Action clickEvt = null; 
        private Action doubleClickEvt = null; 
        
        public void AddClickEvent(Action _callback)
        {
            this.clickEvt = _callback;
            AddElementEvent<ClickEvent>((int)Elements.image, _callback);
        }

        public void AddDoubleClickEvent(Action _callback)
        {
            this.doubleClickEvt = _callback;
            //GetVisualElement((int)Elements.)
        }
        public void AddHoverEvent(Action _callback)
        {
            this.mouseOverEvt = _callback;
            AddElementEvent<PointerOverEvent>((int)Elements.image, _callback); 
        }
        public void AddOutEvent(Action _callback)
        {
            this.mouseOutEvt = _callback;
            AddElementEvent<PointerOutEvent>((int)Elements.image, _callback);
        }

        public void RemoveEvent()
        {
            RemoveElementEvent<ClickEvent>((int)Elements.image, clickEvt);
            RemoveElementEvent<PointerOverEvent>((int)Elements.image, mouseOverEvt);
            RemoveElementEvent<PointerOutEvent>((int)Elements.image, mouseOutEvt);
        }
        public void RemoveView()
        {
            parentElement.RemoveFromHierarchy();
        }
        
        // === UI 설정 관련 === //
        public void ActiveBorder(bool _isActive)
        {
            if (_isActive == true)
            {
                GetVisualElement((int)Elements.slot_border).AddToClassList(activeBorderStr);
                return; 
            }
            GetVisualElement((int)Elements.slot_border).RemoveFromClassList(activeBorderStr);
        }
        public void ClearUI()
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = null;
            GetLabel((int)Labels.text).text = ""; 
        }

        public void SelectSlot(bool _isSelect)
        {
            if (_isSelect == true)
            {
                Select.AddToClassList(selectStr);
            }
            else
            {
                Select.RemoveFromClassList(selectStr);
            }
        }
        
        public void SetSpriteAndText(Texture2D _sprite, int _count)
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = new StyleBackground(_sprite);
            if(_count <=1) // 하나만 있으면 표시 X
            {
                ShowVisualElement(GetLabel((int)Labels.text), false);
                return; 
            }
            ShowVisualElement(GetLabel((int)Labels.text), true);
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
