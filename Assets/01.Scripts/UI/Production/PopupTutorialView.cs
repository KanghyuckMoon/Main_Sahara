using System;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;


namespace UI.Production
{
    public class PopupTutorialView : AbUI_Base
    {
        enum Labels
        {
            title_label, 
            detail_label,
            guide_label, 
        }

        enum Elements
        {
            detail_image
        }

        public enum Buttons
        {
            left_button, 
            right_button 
        }
        
        private Dictionary<Buttons, Action> callbackDic = new Dictionary<Buttons, Action>();
        
        public override void Cashing()
        {
            //base.Cashing();
            Debug.Log(parentElement.name);
            BindLabels(typeof(Labels));
            BindVisualElements(typeof(Elements));
            BindButtons(typeof(Buttons));
        }

        public override void Init()
        {
            base.Init();
            //AddButtonEventToDic(Buttons.left_button, null); 
            //AddButtonEventToDic(Buttons.right_button, null); 
            //AddButtonEvents();  
        }

        public void SetTitle(string _title)
        {
            GetLabel((int)Labels.title_label).text = _title; 
        }
        public void SetDetail(string _detail)   
        {
            GetLabel((int)Labels.detail_label).text = _detail; 
        }
        public void SetDetailImage(Sprite _detailImage)
        {
            GetVisualElement((int)Elements.detail_image).style.backgroundImage = new StyleBackground(_detailImage); 
        }

        public void ActiveGuideLabel(bool _isActive)
        {
            Label _descLabel = GetLabel((int)Labels.guide_label);
            if (_isActive == true)
            {
                _descLabel.text = "F 키를 눌러 창을 닫으세요";
            }
            else
            {
                _descLabel.text = "A, D 키를 통해 페이지를 넘기세요";
            }
            //ShowVisualElement(GetLabel((int)Labels.guide_label),_isActive);
        }
        
        public void AddButtonEvents()
        {
            //AddButtonEvent<ClickEvent>((int)Buttons.graphics_button, callbackDic[Buttons.graphics_button]);
            AddButtonEvent<ClickEvent>((int)Buttons.left_button, callbackDic[Buttons.left_button]);
            AddButtonEvent<ClickEvent>((int)Buttons.right_button, callbackDic[Buttons.right_button]);
        }

        public void RemoveButtonEvents()
        {
            //RemoveButtonEvent<ClickEvent>((int)Buttons.graphics_button, callbackDic[Buttons.graphics_button]);
            RemoveButtonEvent<ClickEvent>((int)Buttons.left_button, callbackDic[Buttons.left_button]);
            RemoveButtonEvent<ClickEvent>((int)Buttons.right_button, callbackDic[Buttons.right_button]);

        }
        
        public void AddButtonEventToDic(Buttons buttonType, Action callback)
        {
            callbackDic[buttonType] = callback;
        }

        public void ActiveButton(bool _isLeft, bool _isActive)
        {
            if (_isLeft == true)
            {
                GetButton((int)Buttons.left_button).style.visibility = _isActive ? Visibility.Visible :Visibility.Hidden;
                //ShowVisualElement(GetButton((int)Buttons.left_button), _isActive);
                return; 
            }
            GetButton((int)Buttons.right_button).style.visibility = _isActive ? Visibility.Visible :Visibility.Hidden;
            //ShowVisualElement(GetButton((int)Buttons.right_button), _isActive);

        }


    }
}