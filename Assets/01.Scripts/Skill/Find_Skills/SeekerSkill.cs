using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;

namespace Skill
{
    public class SeekerSkill : WeaponSkillFunctions, IWeaponSkill
    {
        public void Skills(AbMainModule _mainModule)
        {
        }

        public HitBoxAction GetHitBoxAction()
        {
            return null; 
        }
    }
    
}
