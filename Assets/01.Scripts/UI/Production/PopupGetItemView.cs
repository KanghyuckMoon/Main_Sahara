using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using UI.Base;
using UnityEngine.Android;

namespace UI.Production
{
    public class PopupGetItemView : AbUI_Base
    {
        public class StringData
        {
            public string name;
            public Texture2D sprite;
        }

        enum Elements
        {
            popup_getitem_view,
            image,
            slot,
        }

        enum Labels
        {
            name,
        }

        private const string activeItemStr = "active_item";
        private const string inactiveItemStr = "inactive_item";

        public VisualElement Parent => GetVisualElement((int)Elements.popup_getitem_view);

        public override void Cashing()
        {
            //base.Cashing();
            BindVisualElements(typeof(Elements));
            BindLabels(typeof(Labels));
        }

        /// <summary>
        /// ������ �̹��� Ȱ��ȭ ��Ű�鼭 �ִϸ��̼� ȿ�� 
        /// </summary>
        public void AnimateItem(bool _isActive)
        {
            if (_isActive == true)
            {
                GetVisualElement((int)Elements.slot).RemoveFromClassList(inactiveItemStr);
                GetVisualElement((int)Elements.slot).AddToClassList(activeItemStr);
                return;
            }

            GetVisualElement((int)Elements.slot).RemoveFromClassList(activeItemStr);
            GetVisualElement((int)Elements.slot).AddToClassList(inactiveItemStr);
        }

        /// <summary>
        /// �̹��� �ִϸ��̼� ȿ�� ������ ����� ȿ�� 
        /// </summary>
        public void AddEventAfterImage(Action _callback)
        {
            AddElementEvent<TransitionEndEvent>((int)Elements.slot, _callback);
        }

        public void SetData(StringData _stringData)
        {
            GetLabel((int)Labels.name).text = _stringData.name;
            GetVisualElement((int)Elements.image).style.backgroundImage = _stringData.sprite;
        }
    }
}