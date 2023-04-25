using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Quest;
using UI.Production;
using System;
using UI.ConstructorManager;
using Utill.Pattern;
using GoogleSpreadSheet;
using System.Linq;
using UI.UtilManager;
using UI.Base;

namespace UI.Quest
{
    [System.Serializable]
    public class QuestView : AbUI_Base
    {
        enum ListViews
        {
            quest_listview
        }
        enum Labels
        {
            quest_name_label, 
            quest_detail_label,
            quest_state_label
        }

        enum Elements
        {
            quest_list_panel,
            quest_select
        }

        enum RadioGroups
        {
            select_group
        }


        enum RadioButtons
        {
            main_button,
            sub_button 
        }

        private Dictionary<QuestState, List<QuestEntryView>> questEntryDic = new Dictionary<QuestState, List<QuestEntryView>>();
        private List<QuestData> _questDataList = new List<QuestData>();
        private List<(QuestData,VisualElement)> _questEntryList = new List<(QuestData,VisualElement)>(); 

        // 프로퍼티
        public Dictionary<QuestState, List<QuestEntryView>> QuestEntryDic => questEntryDic;

        public override void Cashing()
        {
            base.Cashing();
            BindLabels(typeof(QuestView.Labels));
            BindRadioButtons(typeof(QuestView.RadioButtons));
            BindListViews(typeof(QuestView.ListViews));
            Bind<RadioButtonGroup>(typeof(QuestView.RadioGroups));
        }

        public override void Init()
        {
            base.Init();
            InitQuestDic(); 
            AddEvents();
            InitListView();
            SendEvent(); 
        }

        public void SendEvent()
        {
            UIUtil.SendEvent(GetRadioButton((int)RadioButtons.main_button));
        }

        public override void ActiveScreen(bool _isActive)
        {
            base.ActiveScreen(_isActive);
            _questEntryList.Clear(); 
        }

        /// <summary>
        /// 퀘스트 이름, 설명 나타내기 
        /// </summary>
        /// <param name="_title"></param>
        /// <param name="_detail"></param>
        public void SetTitleAndDetail(string _title,string _detail)
        {
            GetLabel((int)Labels.quest_name_label).text = _title;
            // 애니메이션 ㄱㄱ 
        //    GetLabel((int)Labels.quest_detail_label).text = _detail;
            UIUtilManager.Instance.AnimateText(GetLabel((int)Labels.quest_detail_label), _detail);
        }

        /// <summary>
        /// 퀘스트UI 부모 설정 
        /// </summary>
        /// <param name="_v"></param>
        public void SetQuestParent(VisualElement _v)
        {
            GetVisualElement((int)Elements.quest_list_panel).Add(_v); 
        }

        /// <summary>
        /// 리스트뷰 초기화(생성, 바인딩)
        /// </summary>
        public void InitListView()
        {
            Debug.Log("AAA");
            _questDataList = QuestManager.Instance.GetActiveOrClearQuest();
            // 테스트
            foreach (var v in _questDataList)
            {
                Debug.Log(v.NameKey);
            }
            ListView _listView = GetListView((int)ListViews.quest_listview);
            
            // 생성
            _listView.makeItem = () =>
            {
                (VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(QuestEntryView));

                _v.Item1.ElementAt(0).userData = _v.Item2 as QuestEntryView;
                Debug.Log("MAKE");
                return _v.Item1.ElementAt(0);
            };

            // 값 바인딩 
            _listView.bindItem = (_item, _index) =>
            {
                string _name = TextManager.Instance.GetText(_questDataList[_index].NameKey);
                string _detail = TextManager.Instance.GetText(_questDataList[_index].ExplanationKey);
                string _state = Enum.GetName(typeof(QuestState), _questDataList[_index].QuestState);
                Debug.Log("BIND");
                _questEntryList.Add((_questDataList[_index], _item));

                (_item.userData as QuestEntryView).SetNameAndDetailAndState(_name, _detail,_state);
            };

            _listView.itemsSource = _questDataList;

            _listView.onSelectionChange += (a) =>
            {
                Debug.Log("onSelectionChange");
                var _selected = _listView.selectedItem as QuestData;
                string _name = TextManager.Instance.GetText(_selected.NameKey);
                string _detail = TextManager.Instance.GetText(_selected.ExplanationKey);

                SetTitleAndDetail(_name, _detail);
                UIUtilManager.Instance.AnimateText(GetLabel((int)Labels.quest_state_label),  Enum.GetName(typeof(QuestState), _selected.QuestState));
            
            };

        }

        /// <summary>
        /// 버튼 이벤트 추가 
        /// </summary>
        private void AddEvents()
        {
            AddRadioBtnChangedEvent((int)RadioButtons.main_button, (x) => 
            {
                // 메인만 활성화
                //DiactiveAllQuest(); 
                List<VisualElement> _list = _questEntryList.Where((x) => x.Item1.QuestCategory == QuestCategory.Main).Select((x) => x.Item2).ToList();
                foreach(var _v in _list)
                {
                    ShowVisualElement(_v, x); 
                }
                float _fV = x ? 1.05f : 1f;
                GetRadioButton((int)RadioButtons.main_button).style.scale = new StyleScale(new Scale(new Vector3(_fV, _fV, 0)));
            });
            
            AddRadioBtnChangedEvent((int)RadioButtons.sub_button, (x) =>
            {
                // 서브만 활성화 
                //DiactiveAllQuest();
                List<VisualElement> _list = _questEntryList.Where((x) => x.Item1.QuestCategory == QuestCategory.Sub).Select((x) => x.Item2).ToList();
                foreach (var _v in _list)
                {
                    ShowVisualElement(_v, x);
                }
                float _fV = x ? 1.05f : 1f; 
                GetRadioButton((int)RadioButtons.sub_button).style.scale =new StyleScale(new Scale(new Vector3(_fV, _fV, 0))); 
            });
        }


        private void InitQuestDic()
        {
            this.questEntryDic.Clear();
            foreach (QuestState _qState in Enum.GetValues(typeof(QuestState)))
            {
                this.questEntryDic.Add(_qState, new List<QuestEntryView>());
            }
        }
  
        /// <summary>
        /// QuestState와 같은 퀘스트만 활성화 
        /// </summary>
        /// <param name="_questState"></param>
        public void ActiveQuestByState(QuestState _questState)
        {
            // 여기서 QuestState가 All 이면 전부 다 활성화 
            foreach (QuestState _qState in Enum.GetValues(typeof(QuestState)))
            {
                if (_qState == _questState) continue;

                if (questEntryDic.TryGetValue(_questState, out List<QuestEntryView> questEntryViewList) == true)
                {
                    foreach (var _questEntryView in questEntryViewList)
                    {
                        _questEntryView.ActiveScreen(false);
                    }
                }
            }

        }

       

    }

}

