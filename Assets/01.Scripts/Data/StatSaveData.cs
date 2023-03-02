using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using ForTheTest;

namespace Data
{
    [System.Serializable]
    public class StatSaveData
    {
        public int maxHp;
        public int currentHp;
        public int maxMana;
        public int currentMana;
        public int meleeAttack;
        public int rangeAttack;
        public int magicAttack;

        public float speed;
        public float jump;

        public void Copy(StatData statData)
		{
            maxHp = statData.MaxHp;
            currentHp = statData.CurrentHp;
            maxMana = statData.MaxMana;
            currentMana = statData.CurrentMana;
            meleeAttack = statData.MeleeAttack;
            rangeAttack = statData.RangeAttack;
            magicAttack = statData.MagicAttack;
            speed = statData.Speed;
            jump = statData.Jump;
        }
    }
}