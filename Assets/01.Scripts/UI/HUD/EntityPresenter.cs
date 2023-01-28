using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

namespace UI
{
    public class EntityPresenter : MonoBehaviour, IUIOwner 
    {
        [SerializeField]
        private UIDocument uiDocument; 

        [SerializeField]
        private HpPresenter hpPresenter;
        [SerializeField]
        private MpPresenter mpPresenter;
        [SerializeField]
        private BuffPresenter buffPresenter; 

        private List<IUIFollower> _presenterList = new List<IUIFollower>();

        public UIDocument Root { get; set; }

        public UIDocument RootUIDocument => uiDocument;

        public List<IUIFollower> PresenterList => _presenterList; 

        public void Awake()
        {
            ContructPresenters();
            AwakePresenters(); 
        }
        public void Start()
        {
            StartPresenters(); 
        }

        [ContextMenu("테스트")]
        public void UpdateUI()
        {
            foreach(var p in _presenterList)
            {
                p.UpdateUI(); 
            }
        }

        /// <summary>
        /// Presenter 생성
        /// </summary>
        private void ContructPresenters()
        {
            _presenterList.Add(hpPresenter);
            _presenterList.Add(mpPresenter);
            _presenterList.Add(buffPresenter);
        }

        private void AwakePresenters()
        {
            foreach (var p in _presenterList)
            {
                p.RootUIDocument = RootUIDocument; 
                p.Awake(); 
            }
        }
        private void StartPresenters()
        {
            foreach (var p in _presenterList)
            {
                p.Start();
            }
        }

        [ContextMenu("버프 아이콘 생성")]
       public void TestCreateBuffIcon()
        {
            buffPresenter.CreateBuffIcon(); 
        }
    }

}

