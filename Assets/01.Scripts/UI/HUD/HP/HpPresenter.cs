using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

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

        // private EntityData _entityData; 

        public void Awake()
        {
            _hpView.InitUIDocument(RootUIDocument); 
            _hpView.Cashing();
        }

        public void Start()
        {
            _hpView.Init(); 
            // _entityData = new EntityData();
        }

        // ������ ������Ʈ�� ��ũ��Ʈ �������� 
        // �Լ� ���÷������� �� ���ͼ� ��Ӵٿ����� ���� 
        // 
        public void UpdateUI()
        {
            //    _hpView.SetBarUI(_entityData.hp);
            _hpView.SetBarUI(_testHp);
        }
    }

}

