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
        // �ν����� ���� ���� 
        [SerializeField]
        private BuffContainer buffControlView;
        // private BuffData buffData; 

        private StatData statData;
        private BuffModule buffData;  
        // ������Ƽ 
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

        public void CreateBuffIcon(/*���� ������*/)
        {
            buffControlView.CreateBuffIcon();
        }

       
    }

}
