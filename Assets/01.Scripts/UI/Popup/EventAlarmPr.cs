using System.Collections;
using System.Collections.Generic;
using UI.Production;
using UnityEngine;
using UnityEngine.UIElements;
using UI.ConstructorManager;
using DG.Tweening;

namespace UI.Popup
{
    public class EventAlarmPr : IPopup
    {
        private EventAlarmView eventAlarmView;
        private VisualElement parent;

        public VisualElement Parent => parent;

        public EventAlarmPr()
        {
            var _prod = UIConstructorManager.Instance.GetProductionUI(typeof(EventAlarmView));
            this.parent = _prod.Item1;
            this.eventAlarmView = _prod.Item2 as EventAlarmView;
        }

        public void ActiveTween()
        {
            eventAlarmView.EventAlarmParent.AddToClassList("active_alarm");
        }

        public void InActiveTween()
        {
            eventAlarmView.EventAlarmParent.RemoveFromClassList("active_alarm");
            eventAlarmView.EventAlarmParent.AddToClassList("inactive_alarm");
        }

        public void Undo()
        {
            eventAlarmView.ParentElement.RemoveFromHierarchy();
        }

        public void SetData(object _data)
        {
            (string, string) a = _data is (string, string) ? ((string, string))_data : (null, null); 
            string _str = _data as string;
            eventAlarmView.SetNameAndDetail(a.Item1, a.Item2);
        }

    }
}