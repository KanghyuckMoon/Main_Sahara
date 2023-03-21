using System.Collections;
using System.Collections.Generic;
using UI.Production;
using UnityEngine;
using UnityEngine.UIElements;
using UI.ConstructorManager;

namespace UI.Popup
{
 
    public class EventAlarmPr : IPopup
    {
        private EventAlarmView eventAlarmView;
        private VisualElement parent; 
        

        public EventAlarmPr()
        {
           var _prod = UIConstructorManager.Instance.GetProductionUI(typeof(EventAlarmView));
           this.parent = _prod.Item1;
           this.eventAlarmView = _prod.Item2  as EventAlarmView;
           
           // 애니메이션 
           //eventAlarmView.EventAlarmParent.RemoveFromClassList("inactive_alarm");
         //  eventAlarmView.EventAlarmParent.AddToClassList("active_alarm");
         eventAlarmView.EventAlarmParent.style.scale = new Scale(new Vector2(1f,1f));
         eventAlarmView.EventAlarmParent.style.opacity = 1f; 
        }

        public VisualElement Parent => eventAlarmView.ParentElement; 

        public void Active()
        {
        }

        public void Undo()
        {
            eventAlarmView.ParentElement.RemoveFromHierarchy();
        }

        public void SetData(object _data)
        {
            string _str = _data as string; 
            eventAlarmView.SetNameAndDetail(_str,_str);
        }

        public IEnumerator TimerCo(float _time)
        {
            float _curTime = 0f; 
            while (true)
            {
                _curTime += Time.deltaTime;
                if (_curTime >= _time)
                {
                    // 애니메이션 
                    //popupGetItemView.Parent.RemoveFromClassList("show_getitem_popup");
                    eventAlarmView.EventAlarmParent.style.scale = new Scale(new Vector2(0.1f,0.1f));
                    eventAlarmView.EventAlarmParent.style.opacity = 0f; 
                    //eventAlarmView.EventAlarmParent.AddToClassList("inactive_alarm");
                    yield return new WaitForSecondsRealtime(0.2f);
                    Undo();
                    yield break; 
                }
                yield return null; 
            }
        }
    }   
}
