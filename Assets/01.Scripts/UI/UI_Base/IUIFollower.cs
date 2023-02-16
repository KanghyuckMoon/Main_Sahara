using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Module;
using Data;

/// <summary>
/// IUIOwner�� ���� ĳ�� �ʱ�ȭ UI������Ʈ ���� �ٸ� Ŭ������ ���� �޾ƶ� 
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
