using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory; 

namespace UI.Upgrade
{

    [CreateAssetMenu(menuName = "SO/UI/FinalItemDataListSO")]
    public class FinalItemDataListSO : ScriptableObject
    {
        [Header("��������UI�� ǥ�õ� ���� ������ ����Ʈ")]
        public List<ItemDataSO> itemList = new List<ItemDataSO>(); 
    }
}

