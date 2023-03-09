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
            quest_detail_label
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

        // ������Ƽ
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
        }

        public override void ActiveScreen(bool _isActive)
        {
            base.ActiveScreen(_isActive);
            _questEntryList.Clear(); 
        }

        /// <summary>
        /// ����Ʈ �̸�, ���� ��Ÿ���� 
        /// </summary>
        /// <param name="_title"></param>
        /// <param name="_detail"></param>
        public void SetTitleAndDetail(string _title,string _detail)
        {
            GetLabel((int)Labels.quest_name_label).text = _title;
            // �ִϸ��̼� ���� 
        //    GetLabel((int)Labels.quest_detail_label).text = _detail;
            UIUtilManager.Instance.AnimateText(GetLabel((int)Labels.quest_detail_label), _detail);
        }

        /// <summary>
        /// ����ƮUI �θ� ���� 
        /// </summary>
        /// <param name="_v"></param>
        public void SetQuestParent(VisualElement _v)
        {
            GetVisualElement((int)Elements.quest_list_panel).Add(_v); 
        }

        /// <summary>
        /// ����Ʈ�� �ʱ�ȭ(����, ���ε�)
        /// </summary>
        public void InitListView()
        {
            Debug.Log("AAA");
            _questDataList = QuestManager.Instance.GetActiveOrClearQuest();
            // �׽�Ʈ
            foreach (var v in _questDataList)
            {
                Debug.Log(v.NameKey);
            }
            ListView _listView = GetListView((int)ListViews.quest_listview);
            
            // ����
            _listView.makeItem = () =>
            {
                (VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(QuestEntryView));

                _v.Item1.userData = _v.Item2 as QuestEntryView;
                Debug.Log("MAKE");
                return _v.Item1;
            };

            // �� ���ε� 
            _listView.bindItem = (_item, _index) =>
            {
                string _name = TextManager.Instance.GetText(_questDataList[_index].NameKey);
                string _detail = TextManager.Instance.GetText(_questDataList[_index].ExplanationKey);
                Debug.Log("BIND");
                _questEntryList.Add((_questDataList[_index], _item));

                (_item.userData as QuestEntryView).SetNameAndDetail(_name, _detail);
            };

            _listView.itemsSource = _questDataList;

            _listView.onSelectionChange += (a) =>
            {
                Debug.Log("onSelectionChange");
                var _selected = _listView.selectedItem as QuestData;
                string _name = TextManager.Instance.GetText(_selected.NameKey);
                string _detail = TextManager.Instance.GetText(_selected.ExplanationKey);

                SetTitleAndDetail(_name, _detail);
            };

        }

        /// <summary>
        /// ��ư �̺�Ʈ �߰� 
        /// </summary>
        private void AddEvents()
        {
            AddRadioBtnChangedEvent((int)RadioButtons.main_button, (x) => 
            {
                // ���θ� Ȱ��ȭ
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
                // ���길 Ȱ��ȭ 
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
        /// QuestState�� ���� ����Ʈ�� Ȱ��ȭ 
        /// </summary>
        /// <param name="_questState"></param>
        public void ActiveQuestByState(QuestState _questState)
        {
            // ���⼭ QuestState�� All �̸� ���� �� Ȱ��ȭ 
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

