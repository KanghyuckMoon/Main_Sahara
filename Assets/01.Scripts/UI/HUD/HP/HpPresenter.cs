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
        // 인스펙터 참조 변수 
        [SerializeField]
        private float _testHp;
        [SerializeField]
        private HpView _hpView;

        // 프로퍼티 
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
        // 선택한 오브젝트의 스크립트 가져오고 
        // 함수 리플렉션으로 쭉 빼와서 드롭다운으로 설정 

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

