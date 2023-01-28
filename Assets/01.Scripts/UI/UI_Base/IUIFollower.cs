using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

/// <summary>
/// IUIOwner�� ���� ĳ�� �ʱ�ȭ UI������Ʈ ���� �ٸ� Ŭ������ ���� �޾ƶ� 
/// </summary>
namespace UI
{

    public interface IUIFollower
    {
        public UIDocument RootUIDocument { get; set; }
        public void Awake();
        public void Start();
        public void UpdateUI();

    }

}
