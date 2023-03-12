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
        // 인스펙터 참조 변수 
        [SerializeField]
        private int _testMp;
        [SerializeField]
        private MpView _mpView;

        // 프로퍼티 
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
        // 선택한 오브젝트의 스크립트 가져오고 
        // 함수 리플렉션으로 쭉 빼와서 드롭다운으로 설정 
        // 
        public void UpdateUI()
        {
            //    _hpView.SetBarUI(_entityData.hp);
            _mpView.SetBarUI((float)statData.CurrentMana/ statData.MaxMana);
        }
    }
}

