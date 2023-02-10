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
        private int _testHp;
        [SerializeField]
        private HpView _hpView;

        // ������Ƽ 
        public UIDocument RootUIDocument { get; set ; }

        private StateData stateData;
        private StateModule stateModule;

        public void Awake()
        {
            _hpView.InitUIDocument(RootUIDocument); 
            _hpView.Cashing();
        }

        public void Start(StateData _stateData)
        {
            _hpView.Init();
            this.stateData = _stateData; 
            // _entityData = new EntityData();
        }
        public void Start(StateModule _stateData)
        {
            _hpView.Init();
            this.stateModule = _stateData; 
        }

        // ������ ������Ʈ�� ��ũ��Ʈ �������� 
        // �Լ� ���÷������� �� ���ͼ� ��Ӵٿ����� ���� 
        // 
        public void UpdateUI()
        {
            //    _hpView.SetBarUI(_entityData.hp);
            _hpView.SetBarUI(stateData.currentHp / stateData.maxHp);
        }


    }

}

