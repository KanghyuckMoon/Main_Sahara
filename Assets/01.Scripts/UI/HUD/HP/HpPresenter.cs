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
    public class HpPresenter :  IUIFollower
    {
        // �ν����� ���� ���� 
        [SerializeField]
        private float _testHp;
        [SerializeField]
        private HpView _hpView;

        // ������Ƽ 
        public UIDocument RootUIDocument { get; set ; }

        private StatData statData;

        public void Awake()
        {
            _hpView.InitUIDocument(RootUIDocument); 
            _hpView.Cashing();
        }

        public void Start(StatData _statData)
        {
            _hpView.Init();
            this.statData = _statData; 
        }
        public void Start(object _data)
        {
            _hpView.Init();
            this.statData = _data as StatData;
        }
        // ������ ������Ʈ�� ��ũ��Ʈ �������� 
        // �Լ� ���÷������� �� ���ͼ� ��Ӵٿ����� ���� 

        [SerializeField]
        private float curHp;

        [SerializeField]
        private float maxHp;
        public void UpdateUI()
        {
            float _hp = (float)statData.CurrentHp / (float)statData.MaxHp;

            curHp = statData.CurrentHp;
            maxHp = statData.MaxHp;
            //    _hpView.SetBarUI(_entityData.hp)  ;
            _hpView.SetBarUI(_hp);
            //_hpView.SetMpText(statData.CurrentHp, statData.MaxHp);

        }

        public void ActiveScreen(bool _isActive)
        {
            _hpView.ActiveScreen(_isActive);
        }


    }

}

