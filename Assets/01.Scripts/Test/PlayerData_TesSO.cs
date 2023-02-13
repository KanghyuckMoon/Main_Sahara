using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForTheTest
{
    [CreateAssetMenu(menuName = "SO/PlayerData_TestSO")]
    public class PlayerData_TesSO : ScriptableObject
    {
        [Header("[����] ���� ���ݷ�")] public int meleeAttack;
        [Header("[����] ���Ÿ� ���ݷ�")] public int rangedAttack;
        [Header("[����] ���� ���ݷ�")] public int magicAttack;

        [Header("[����] ����")] public int adDefence;
        [Header("[����] ����")] public int magicDefence;

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