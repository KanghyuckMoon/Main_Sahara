using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Module;
using Data;

/// <summary>
/// IUIOwner에 의해 캐싱 초기화 UI업데이트 것을 다른 클래스에 관리 받아라 
/// </summary>
namespace UI
{

    public interface IUIFollower
    {
        public UIDocument RootUIDocument { get; set; }
        public void Awake();
        //public void Start(StateData _stateData);
        public void Start(Data.StatData _statData);
        public void UpdateUI();

    }

}
