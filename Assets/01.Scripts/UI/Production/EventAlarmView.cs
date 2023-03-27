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
        }

        enum Labels
        {
            event_detail_label,
            event_name_label
        }

        public VisualElement Parent => parentElement;
        public VisualElement EventAlarmParent => GetVisualElement((int)Elements.event_alarm_view); 
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

        public void SetNameAndDetail(string _name, string _detail)
        {
            SetEventName(_name);
            SetEventDetail(_detail); 
        }

        public void SetEventName(string _name)
        {
            GetLabel((int)Labels.event_name_label).text = _name; 
        }
        public void SetEventDetail(string _detail)
        {
            GetLabel((int)Labels.event_detail_label).text = _detail;
        }

        public void SetImage(Texture2D _image)
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = new StyleBackground(_image);
        }
    }

}
