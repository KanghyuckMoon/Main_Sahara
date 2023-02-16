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
        // �ν����� ���� ����
        [SerializeField]
        private BuffContainer buffControlView;
        // private BuffData buffData; 

        private StatData statData; 
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
