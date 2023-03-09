using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Module;
using Data;
using Buff;

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
        private BuffModule buffData;  
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

        public void Start(object _data)
        {
            buffData = _data as AbBuffEffect;
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
