using System.Collections;
using System.Collections.Generic;
using Buff;
using UnityEngine;
using Module;
using Pool;
using Utill.Pattern;

namespace Skill
{
    public class SkillE_Vayu : SkillFunctions, ISkill
    {
        //����ٰ� ������ ������ �ȴ�. �������� �ӵ�, �� �׷� �͵�
        [SerializeField]
        private AnimationClip animationClip;

        public void Skill(AbMainModule _mainModule)
        {
            UseSkill(_mainModule, animationClip);
        }
    }
}