using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForTheTest
{
    [CreateAssetMenu(menuName = "SO/PlayerData_TestSO")]
    public class PlayerData_TesSO : ScriptableObject
    {
        [Header("[물리] 근접 공격력")] public int meleeAttack;
        [Header("[물리] 원거리 공격력")] public int rangedAttack;
        [Header("[마법] 마법 공격력")] public int magicAttack;

        [Header("[물리] 방어력")] public int adDefence;
        [Header("[마법] 방어력")] public int magicDefence;

        [Space]
        [Space]
        [Space]
        [Space]
        public float speed;
        public float jumpPower;
        public int hp;
        public int mana;
    }
}