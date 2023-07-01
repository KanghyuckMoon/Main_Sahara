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
using UI.Base;
    using UnityEngine.PlayerLoop;

    namespace UI.Quest
{
    public class QuestPresenter :AbBaseScreen
    {
        [SerializeField]
        private UIDocument uiDocument;
        [SerializeField]
        private QuestView questView;

        private Dictionary<QuestState, List<QuestEntryView>> questEntryDic = new Dictionary<QuestState, List<QuestEntryView>>();

        private Action onActiveScreenEvt = null; 
        // 프로퍼티 
        public Action OnActiveScreen
        {
            get => onActiveScreenEvt;
            set => onActiveScreenEvt = value;
        }
        public override IUIController UIController { get; set; }

        private void Awake()
        {
            this.uiDocument = GetComponent<UIDocument>(); 
        }

        private void OnEnable()
        {
            questView.InitUIDocument(uiDocument);
            
            questView.Cashing();
            questView.Init();


        }

        private void Start()
        {
            UpdateUI(); 
        }

        [ContextMenu("업데이트")]
        public void UpdateUI()
        {
            questView.InitListView(); 
        }


        [ContextMenu("퀘스트UI 생성")]
        /// <summary>
        /// 특정 QuestData 받아서 퀘스트 생성   
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

                // 부모 설정 
                questView.SetQuestParent(_vQuestEntry);

                // 생성 퀘스트UI에 텍스트 설정 
                string _nameT = TextManager.Instance.GetText(_nameKey);
                string _detailT = TextManager.Instance.GetText(_detailKey);
                string _stateT = Enum.GetName((typeof(QuestState)),_state);
                    ;
                _qEntryView.SetNameAndDetailAndState(_nameT,_detailT,_stateT);

                // 퀘스트 타입별로 나눈 채 딕셔너리 추가 
                this.questView.QuestEntryDic[_state].Add(_qEntryView); 
            }
        }
        
        [ContextMenu("ListView 테스트")]
        public void TestListView()
        {
            this.questView.InitListView(); 
        }

        public override bool ActiveView()
        {
            base.ActiveView(); 
            bool _isActive = questView.ActiveScreen();
            questView.ActiveHeader(_isActive);
            return _isActive; 
        }

        public override void ActiveView(bool _isActive)
        {
            base.ActiveView(_isActive); 
            questView.ActiveScreen(_isActive);
        }

        /*
         * 현재  퀘스트 받아와서 
         * QuestView 의 quest_list_panel의 자식으로 띄우기 
         *  
         */
    }

}
