using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(menuName = "SO/WeaponDataSO")]
    public class WeaponDataSO : ScriptableObject
    {
        [Header("[����] ���� ���ݷ�")] public int meleeAttack;
        [Header("[����] ���Ÿ� ���ݷ�")] public int rangedAttack;
        [Header("[����] ���� ���ݷ�")] public int magicAttack;

        [Space]
        [Header("���� �ӵ�")] public float attackSpeed;

        [Space]
        [Header("����Ʈ �̸�")] public string effectName;

        [Space]
        [Header("��ų ���� ��뷮")] public int manaConsumed;

        [TextArea]
        [Header("���� ����")]
        public string weaponExplanation;
    }
}