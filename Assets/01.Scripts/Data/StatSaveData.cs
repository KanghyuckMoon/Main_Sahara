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
        
        public int physicalResistance;
        public int magicResistance;
        
        public int healthRegen;
        public int manaRegen;

        public float walkSpeed;
        public float runSpeed;
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

            healthRegen = statData.HealthRegen;
            manaRegen = statData.ManaRegen;
            
            //physicalResistance = statData.
            walkSpeed = statData.WalkSpeed;
            runSpeed = statData.RunSpeed;
            jump = statData.Jump;
        }
    }
}