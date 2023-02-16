using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Module;
using Data;

namespace UI
{
    [Serializable]
    public class BuffPresenter : IUIFollower
    {
        // 인스펙터 참조 변수
        [SerializeField]
        private BuffContainer buffControlView;
        // private BuffData buffData; 

        private StatData statData; 
        // 프로퍼티 
        public UIDocument RootUIDocument { get; set; }

        public void Awake()
        {
            buffControlView.InitUIDocument(RootUIDocument);
            buffControlView.Cashing(); 
        }

        public void Start(StatData _statData)
        {
            buffControlView.Init();
            this.statData = _statData;
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
