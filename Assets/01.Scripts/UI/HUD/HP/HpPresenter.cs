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
        // 인스펙터 참조 변수 
        [SerializeField]
        private int _testHp;
        [SerializeField]
        private HpView _hpView;

        // 프로퍼티 
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

        // 선택한 오브젝트의 스크립트 가져오고 
        // 함수 리플렉션으로 쭉 빼와서 드롭다운으로 설정 
        // 
        public void UpdateUI()
        {
            //    _hpView.SetBarUI(_entityData.hp);
            _hpView.SetBarUI(_testHp);
        }
    }

}

