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
        //����ٰ� ������ ������ �ȴ�. �������� �ӵ�, �� �׷� �͵�
        [SerializeField]
        private AnimationClip animationClip;

        [SerializeField] private string effectName;

        public void Skill(AbMainModule _mainModule)
        {
            Skill_Test(_mainModule, animationClip);
        }
    }
}