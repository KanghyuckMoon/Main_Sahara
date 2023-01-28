using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Module; 

namespace UI
{
    [Serializable]
    public class BuffPresenter : IUIFollower
    {
        // 인스펙터 참조 변수 
        [SerializeField]
        private BuffContainer buffControlView;
        // private BuffData buffData; 

        private StateData stateData; 
        // 프로퍼티 
        public UIDocument RootUIDocument { get; set; }

        public void Awake()
        {
            buffControlView.InitUIDocument(RootUIDocument);
            buffControlView.Cashing(); 
        }

        public void Start(StateData _stateData)
        {
            buffControlView.Init();
            this.stateData = _stateData; 
        }

        public void UpdateUI()
        {
        //    buffView.
        }

        public void CreateBuffIcon(/*버프 데이터*/)
        {
            buffControlView.CreateBuffIcon();
        }

    }

}
