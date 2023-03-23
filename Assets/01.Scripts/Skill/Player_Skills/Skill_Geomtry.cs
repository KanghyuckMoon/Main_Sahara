using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Pool;
using Utill.Pattern;

namespace Skill
{
    public class Skill_Geomtry : SkillFunctions, ISkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        [SerializeField] private string effectName;

        public void Skill(AbMainModule _mainModule)
        {
            Skill_Test(_mainModule, animationClip);
        }
    }
}
