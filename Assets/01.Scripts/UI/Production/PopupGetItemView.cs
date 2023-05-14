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

        }

        enum Labels
        {
            name,
        }

        public VisualElement Parent => GetVisualElement((int)Elements.popup_getitem_view);

        public override void Cashing()
        {
            //base.Cashing();
            BindVisualElements(typeof(Elements));
            BindLabels(typeof(Labels));
        }

        public void SetData(StringData _stringData)
        {
            GetLabel((int)Labels.name).text = _stringData.name;
            GetVisualElement((int)Elements.image).style.backgroundImage = _stringData.sprite;
        }
    }
}