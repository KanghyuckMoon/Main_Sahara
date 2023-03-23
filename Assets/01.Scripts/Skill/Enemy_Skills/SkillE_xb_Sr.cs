using System.Collections;
using System.Collections.Generic;
using Buff;
using UnityEngine;
using Module;
using Pool;
using Utill.Pattern;

namespace Skill
{
    public class SkillE_xb_Sr : SkillFunctions, ISkill
    {
        //여기다가 값들을 넣으면 된다. 데미지나 속도, 뭐 그런 것들
        [SerializeField]
        private AnimationClip animationClip;

        [SerializeField] private string effectName;

        public void Skill(AbMainModule _mainModule)
        {
            Skill_Test(_mainModule, animationClip);
        }
    }
}