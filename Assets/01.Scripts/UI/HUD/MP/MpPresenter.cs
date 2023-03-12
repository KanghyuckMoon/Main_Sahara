using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using Module;
using Data;

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

        private StatData statData; 
        public void Awake()
        {
            _mpView.InitUIDocument(RootUIDocument); 
            _mpView.Cashing();
        }

        public void Start(StatData _statData)
        {
            _mpView.Init();
            this.statData = _statData;
        }
        public void Start(object _data)
        {
            _mpView.Init();
            this.statData = _data as StatData;
        }
        // ������ ������Ʈ�� ��ũ��Ʈ �������� 
        // �Լ� ���÷������� �� ���ͼ� ��Ӵٿ����� ���� 
        // 
        public void UpdateUI()
        {
            //    _hpView.SetBarUI(_entityData.hp);
            _mpView.SetBarUI((float)statData.CurrentMana/ statData.MaxMana);
        }
    }
}

