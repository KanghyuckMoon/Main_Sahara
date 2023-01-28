using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using Module; 

namespace UI
{
    [Serializable]
    public class MpPresenter : IUIFollower
    {
        // �ν����� ���� ���� 
        [SerializeField]
        private int _testMp;
        [SerializeField]
        private MpView _mpView;

        // ������Ƽ 
        public UIDocument RootUIDocument { get; set; }

        private StateData stateData; 

        public void Awake()
        {
            _mpView.InitUIDocument(RootUIDocument); 
            _mpView.Cashing();
        }

        public void Start(StateData _stateData)
        {
            _mpView.Init();
            this.stateData = _stateData; 
        }

        // ������ ������Ʈ�� ��ũ��Ʈ �������� 
        // �Լ� ���÷������� �� ���ͼ� ��Ӵٿ����� ���� 
        // 
        public void UpdateUI()
        {
            //    _hpView.SetBarUI(_entityData.hp);
            _mpView.SetBarUI(_testMp);
        }
    }
}

