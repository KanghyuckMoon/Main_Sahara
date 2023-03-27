using System;
using System.Collections;
using System.Collections.Generic;
using UI.Production;
using UnityEngine;
using UnityEngine.UIElements;
using UI.ConstructorManager;
using DG.Tweening;
using Quest;
using Utill.Addressable;
using GoogleSpreadSheet;

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
            QuestData _questData = _data as QuestData;
            string _name = TextManager.Instance.GetText(_questData.NameKey);
            string _stateStr = GetQuestStateStr(_questData.QuestState);
            Texture2D _categoryImg = GetQuestCategoryStr(_questData.QuestCategory);
            
            eventAlarmView.SetNameAndDetail(_name, _stateStr);
            eventAlarmView.SetImage(_categoryImg);
            
            /*
            (string, string) a = _data is (string, string) ? ((string, string))_data : (null, null); 
            string _str = _data as string;
            eventAlarmView.SetNameAndDetail(a.Item1, a.Item2);
            */

        }
        
        private string GetQuestStateStr(QuestState _questState)
        {
            string _str = string.Empty;
            switch (_questState)
            {
                case QuestState.Disable:
                    break;
                case QuestState.Discoverable:
                    break;
                case QuestState.Active:
                    _str = "퀘스트 활성화";
;                    break;
                case QuestState.Achievable:
                    break;
                case QuestState.Clear:
                    _str = "퀘스트 클리어";
                    break;
            }
            return _str; 
        }
        
        private Texture2D GetQuestCategoryStr(QuestCategory _questCategory)
        {
            Texture2D _img = null;
            switch (_questCategory)
            {
                case QuestCategory.Main:
                    _img = AddressablesManager.Instance.GetResource<Texture2D>("MainQuestLogo");
                    break;
                case QuestCategory.Sub:
                    _img = AddressablesManager.Instance.GetResource<Texture2D>("SubQuestLogo");
                    break;
            }
            return _img; 
        }
    }
}