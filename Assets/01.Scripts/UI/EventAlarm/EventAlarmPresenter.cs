using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;
using UI.Production;

namespace UI.EventAlarm
{
    public class EventAlarmPresenter : MonoBehaviour,IScreen
    {
        private UIDocument uiDocument; 
        [SerializeField]
        private EventAlarmScreenView alarmScreenView;


        private UIConstructor<EventAlarmView> eventAlarmConstructor; 
        private void OnEnable()
        {
            eventAlarmConstructor = new UIConstructor<EventAlarmView>("EventAlarm");
            alarmScreenView.InitUIDocument(uiDocument);
            alarmScreenView.Cashing(); 
        }

        private void Awake()
        {
            uiDocument = GetComponent<UIDocument>(); 
        }

        [ContextMenu("�̺�Ʈ �˸� �׽�Ʈ")]
        public void TestEventAlarm()
        {
            (VisualElement, AbUI_Base) t = eventAlarmConstructor.CreateUI();
            EventAlarmView e = t.Item2 as EventAlarmView;
            this.alarmScreenView.SetThisParent(t.Item1); 
        }

        public void SetNameAndDetail(string _name, string _detail)
        {
        //    eventAlarmConstructor.UIList
        }
        public bool ActiveView()
        {
            return alarmScreenView.ActiveScreen(); 
        }

        public void ActiveView(bool _isActive)
        {
            alarmScreenView.ActiveView(_isActive);
        }
    }
    
   
}
