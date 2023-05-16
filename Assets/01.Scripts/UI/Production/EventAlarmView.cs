using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Production
{
    public class EventAlarmView : AbUI_Base, IConstructorUI
    {
        enum Elements
        {
            event_alarm_view,
            image, 
            clear_icon
        }

        enum Labels
        {
            event_detail_label,
            event_name_label,
            event_category_label, 
        }

        public VisualElement Parent => parentElement;
        public VisualElement EventAlarmParent => GetVisualElement((int)Elements.event_alarm_view); 
        private const string activeTextStr = "active_text"; 
        private const string inactiveTextStr = "inactive_text";

        private const string inactiveClearIconStr = "inactive_ci";
        public EventAlarmView()
        {
 
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

        public void ActiveView(bool _isActive)
        {
            parentElement.AddToClassList("active");
            ActiveScreen(_isActive); 
        }

        public void ActiveTexts()
        {
            GetLabel((int)Labels.event_name_label).RemoveFromClassList(inactiveTextStr);
            GetLabel((int)Labels.event_detail_label).RemoveFromClassList(inactiveTextStr);
            GetLabel((int)Labels.event_category_label).RemoveFromClassList(inactiveTextStr);
          
            GetLabel((int)Labels.event_name_label).AddToClassList(activeTextStr);
            GetLabel((int)Labels.event_detail_label).AddToClassList(activeTextStr);
            GetLabel((int)Labels.event_category_label).AddToClassList(activeTextStr);
        }

        public void AddClearIconCallback(Action _callback)
        {
            GetVisualElement((int)Elements.clear_icon).RegisterCallback<TransitionEndEvent>((x) =>_callback?.Invoke());
        }
        public void ActiveClearIcon()
        {
            GetVisualElement((int)Elements.clear_icon).RemoveFromClassList(inactiveClearIconStr);
        }
        public void SetNameAndDetail(string _name, string _detail, string _state)
        {
            SetEventName(_name);
            SetEventDetail(_detail);
            SetEventState(_state);
        }

        public void SetEventName(string _name)
        {
            GetLabel((int)Labels.event_name_label).text = _name; 
        }
        public void SetEventDetail(string _detail)
        {
            GetLabel((int)Labels.event_detail_label).text = _detail;
        }

        public void SetEventState(string _state)
        {
            GetLabel((int)Labels.event_category_label).text = _state; 
        }

        public void SetImage(Texture2D _image)
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = new StyleBackground(_image);
        }
    }

}
