using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory; 

namespace UI.Upgrade
{

    [CreateAssetMenu(menuName = "SO/UI/FinalItemDataListSO")]
    public class FinalItemDataListSO : ScriptableObject
    {
        [Header("대장장이UI에 표시될 최종 아이템 리스트")]
        public List<ItemDataSO> itemList = new List<ItemDataSO>(); 
    }
}

