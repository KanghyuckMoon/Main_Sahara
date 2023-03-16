using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;

namespace Skill
{
    public class TestSwordSkill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;
        
        public void Skills(AbMainModule _mainModule)
        {
            TestWeaponFunc(_mainModule, animationClip);
        }
    }
}
