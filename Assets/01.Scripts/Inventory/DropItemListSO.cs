using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "DropItemListSO", menuName = "SO/DropItemListSO ")]
    public class DropItemListSO : ScriptableObject
    {
        public int dropCount = 1;

        public float[] randomPercentArr;

        public string[] dropItemKeyArr;
    }
}
