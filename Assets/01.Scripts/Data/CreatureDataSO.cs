using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/CreatureDataSO")]
    public class CreatureDataSO : ScriptableObject
    {
        [Header("체력")] public int hp;
        [Header("마나")] public int mp;

        [Space]
        [Header("물리_근접공격력")] public int physicalMeleeAttack;
        [Header("물리_원거리공격력")] public int physicalRangedAttack;
        [Header("마법_공격력")] public int magicAttack;

        [Space]
        [Header("물리저항력")] public int physicalResistance;
        [Header("마법저항력")] public int magicResistance;

        [Space]
        [Header("걷는 속도")] public float walkingSpeed;
        [Header("뛰는 속도")] public float runSpeed;
        [Header("점프 높이")] public float jumpScale;

        [Space]
        [Header("넉백 배율")] public float knockBackScale;
        [Header("무게")] public float weight;
        
        [Space]
        [Header("공격속도")] public float animationSpeed;
        
        [Space]
        [Header("초당 체력 재생량")] public int healthRegen;
        [Header("공격시 마나 회복량")] public int manaRegen;

        [Space]
        [Header("활 차지 시간")] public float chargingTime;

        [Space]
        [Header("면역들")] 
        public bool physicalKnockBack;
        public bool magicKnockBack;
        public bool ignoringPhysicalDamage;
        public bool ignoringMagicDamage;
    }
}