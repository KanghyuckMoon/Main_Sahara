using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(menuName = "SO/WeaponDataSO")]
    public class WeaponDataSO : ScriptableObject
    {
        [Header("[물리] 근접 공격력")] public int meleeAttack;
        [Header("[물리] 원거리 공격력")] public int rangedAttack;
        [Header("[마법] 마법 공격력")] public int magicAttack;

        [Space]
        [Header("공격 속도")] public float attackSpeed;

        [Space]
        [Header("이펙트 이름")] public string effectName;

        [TextArea]
        [Header("무긴 설명")]
        public string weaponExplanation;
    }
}