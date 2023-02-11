using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Quest;
using UI.ConstructorManager;
using Utill.Pattern;
using UI.Production;
using GoogleSpreadSheet;
using Utill.SeralizableDictionary;
using System; 

namespace UI.Quest
{
    public class QuestPresenter : MonoBehaviour, IScreen
    {
        [SerializeField]
        private UIDocument uiDocument;
        [SerializeField]
        private QuestView questView;

        private Dictionary<QuestState, List<QuestEntryView>> questEntryDic = new Dictionary<QuestState, List<QuestEntryView>>(); 
        private void Awake()
        {
            this.uiDocument = GetComponent<UIDocument>(); 
        }

        private void OnEnable()
        {
            questView.InitUIDocument(uiDocument);
            
            questView.Cashing();
           // questView.Init();
        }

        private void Start()
        {
            questView.InitListView(); 
        }

        [ContextMenu("����ƮUI ����")]
        /// <summary>
        /// Ư�� QuestData �޾Ƽ� ����Ʈ ����   
        /// </summary>
        public void ActiveQuest()
        {
            List<QuestData> _list = QuestManager.Instance.GetActiveOrClearQuest();
            foreach(var q in _list)
            {
                string _nameKey = q.NameKey;
                string _detailKey = q.ExplanationKey;
                QuestState _state = q.QuestState;

                (VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(QuestEntryView));
                QuestEntryView _qEntryView = _v.Item2 as QuestEntryView;
                VisualElement _vQuestEntry = _v.Item1;

                // �θ� ���� 
                questView.SetQuestParent(_vQuestEntry);

                // ���� ����ƮUI�� �ؽ�Ʈ ���� 
                string _nameT = TextManager.Instance.GetText(_nameKey);
                string _detailT = TextManager.Instance.GetText(_detailKey);
                _qEntryView.SetNameAndDetail(_nameT,_detailT);

                // ����Ʈ Ÿ�Ժ��� ���� ä ��ųʸ� �߰� 
                this.questView.QuestEntryDic[_state].Add(_qEntryView); 
            }
        }
        
        [ContextMenu("ListView �׽�Ʈ")]
        public void TestListView()
        {
            this.questView.InitListView(); 
        }

        public bool ActiveView()
        {
            return questView.ActiveScreen(); 
        }

        public void ActiveView(bool _isActive)
        {
            questView.ActiveScreen(_isActive);
        }

        /*
         * ����  ����Ʈ �޾ƿͼ� 
         * QuestView �� quest_list_panel�� �ڽ����� ���� 
         *  
         */
    }

}
