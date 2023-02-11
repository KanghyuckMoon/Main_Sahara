using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using Module; 

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

        private StateModule stateModule;

        public void Awake()
        {
            _hpView.InitUIDocument(RootUIDocument); 
            _hpView.Cashing();
        }

        public void Start(StateModule _stateData)
        {
            _hpView.Init();
            this.stateModule = _stateData; 
        }

        // ������ ������Ʈ�� ��ũ��Ʈ �������� 
        // �Լ� ���÷������� �� ���ͼ� ��Ӵٿ����� ���� 
        

        public void UpdateUI()
        {
            float _hp = (float)stateModule.CurrentHp / (float)stateModule.MaxHp;

            //    _hpView.SetBarUI(_entityData.hp)  ;
            _hpView.SetBarUI(_hp);
        }

        public void Test1()
        {
            _hpView.SetBarUI(_testHp);

        }

        public void Test2()
        {
       //     _hpView.ActiveScreen(); 
        _hpView.Test(_testHp);

        }

        public void Test3()
        {
                 _hpView.ActiveScreen(); 
            //_hpView.Test(_testHp);

        }
    }

}

