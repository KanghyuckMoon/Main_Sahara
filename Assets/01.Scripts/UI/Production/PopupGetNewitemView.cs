using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Production
{   

    public class PopupGetNewitemView : AbUI_Base
    {
        public class StringData
        {
            public string name;
            public string detail;
            public string state; 
            public Texture2D sprite; 
        }
        enum Elements
        {
            image,
            slot,
            
        }

        enum Labels
        {
            name_label, 
            detail_label, 
            state_label
        }

        private const string activeTextStr = "active_text"; 
        private const string inactiveTextStr = "inactive_text";

        private const string activeItemStr = "active_item";
        private const string inactiveItemStr = "inactive_item";

        public VisualElement Parent => parentElement; 
        public override void Cashing()
        {
            BindVisualElements(typeof(Elements));
            BindLabels(typeof(Labels));
        }


        /// <summary>
        /// 아이템 이미지 활성화 시키면서 애니메이션 효과 
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
        /// 이미지 애니메이션 효과 끝난후 진행될 효과 
        /// </summary>
        public void AddEventAfterImage(Action _callback)
        {
            AddElementEvent<TransitionEndEvent>((int)Elements.slot, _callback);
        }
        public void ActiveTexts()
        {
            GetLabel((int)Labels.name_label).RemoveFromClassList(inactiveTextStr);
            GetLabel((int)Labels.detail_label).RemoveFromClassList(inactiveTextStr);
            GetLabel((int)Labels.state_label).RemoveFromClassList(inactiveTextStr);
          
            GetLabel((int)Labels.name_label).AddToClassList(activeTextStr);
            GetLabel((int)Labels.detail_label).AddToClassList(activeTextStr);
            GetLabel((int)Labels.state_label).AddToClassList(activeTextStr);
        }
        
        public void SetTexts(string _nameStr, string _detailStr, string _stateStr)
        {
            GetLabel((int)Labels.name_label).text = _nameStr; 
            GetLabel((int)Labels.detail_label).text = _detailStr; 
            GetLabel((int)Labels.state_label).text = _stateStr; 
        }

        public void SetImage(Texture2D _image)
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = _image; 
        }
        public void SetImage(Sprite _image)
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = new StyleBackground(_image); 
        }

        public void SetData(StringData _data)
        {
            SetTexts(_data.name,_data.detail,"NEW");
            SetImage(_data.sprite);
        }
    }

}
