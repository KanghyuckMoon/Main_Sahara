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
        [Header("휘두를 때 이펙트 이름")] public string attackEffectName;
        [Header("강하게 휘두를 때 이펙트 이름")] public string strongAttackEffectName;

        [Space]
        [Header("차징이 가능한가?")] public bool canCharge;

        [Space]
        [Header("스킬 마나 사용량")] public int manaConsumed;
        [Header("스킬 데미지")] public int skillDamage;

        [Space]
        [Header("일반 공격 애니메이션")] public AnimationClip attackAnimation;
        [Header("강한 공격 애니메이션")] public AnimationClip strongAttackAnimation;
        [Header("차징 준비 애니메이션")] public AnimationClip readyAttackAnimation;
        [Header("차징 애니메이션")] public AnimationClip chargeAttackAnimation;

        [TextArea]
        [Header("무긴 설명")]
        public string weaponExplanation;
    }
}