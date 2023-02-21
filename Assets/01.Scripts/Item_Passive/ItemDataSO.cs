using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PassiveItem
{
    [CreateAssetMenu(menuName = "SO/ItemDataSO")]
    public class ItemDataSO : ScriptableObject
    {
        public int atk;
        public int magic;
        public int atkDef;
        public int magicDef;

        public int hp;
    }
}