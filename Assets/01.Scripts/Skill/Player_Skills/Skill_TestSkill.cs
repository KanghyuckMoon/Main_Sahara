using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace Skill
{
    public class Skill_TestSkill : SkillFunctions, ISkill
    {
        //여기다가 값들을 넣으면 된다. 데미지나 속도, 뭐 그런 것들
        [SerializeField]
        private AnimationClip animationClip;

        public void Skill(AbMainModule _mainModule)
        {
            Skill_Test(_mainModule, animationClip);
            //throw new System.NotImplementedException();
        }
    }
}