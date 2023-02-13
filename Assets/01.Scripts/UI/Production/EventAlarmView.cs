using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base; 

namespace UI.Production
{
    public class EventAlarmView : AbUI_Base, IConstructorUI
    {
        enum Elements
        {

        }

        enum Labels
        {
            event_detail_label,
            event_name_label
        }

        public EventAlarmView()
        {
 
        }
        public override void Cashing()
        {
            //base.Cashing();
            BindLabels(typeof(Labels)); 
        }

        public override void Init()
        {
            base.Init();
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
    }

}
