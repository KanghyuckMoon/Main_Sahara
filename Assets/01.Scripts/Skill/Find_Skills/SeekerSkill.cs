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
            StateModule a = _mainModule.GetModuleComponent<StateModule>(ModuleType.State);

            a.RemoveState(State.SKILL);
            return;
        }

        public HitBoxAction GetHitBoxAction()
        {
            return null; 
        }
    }
    
}
