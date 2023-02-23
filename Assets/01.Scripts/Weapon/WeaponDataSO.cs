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
        [Header("�ֵθ� �� ����Ʈ �̸�")] public string attackEffectName;
        [Header("���ϰ� �ֵθ� �� ����Ʈ �̸�")] public string strongAttackEffectName;

        [Space]
        [Header("��¡�� �����Ѱ�?")] public bool canCharge;

        [Space]
        [Header("��ų ���� ��뷮")] public int manaConsumed;
        [Header("��ų ������")] public int skillDamage;

        [Space]
        [Header("�Ϲ� ���� �ִϸ��̼�")] public AnimationClip attackAnimation;
        [Header("���� ���� �ִϸ��̼�")] public AnimationClip strongAttackAnimation;
        [Header("��¡ �غ� �ִϸ��̼�")] public AnimationClip readyAttackAnimation;
        [Header("��¡ �ִϸ��̼�")] public AnimationClip chargeAttackAnimation;

        [TextArea]
        [Header("���� ����")]
        public string weaponExplanation;
    }
}