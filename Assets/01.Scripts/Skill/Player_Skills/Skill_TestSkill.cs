using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace Skill
{
    public class Skill_TestSkill : SkillFunctions, ISkill
    {
        //����ٰ� ������ ������ �ȴ�. �������� �ӵ�, �� �׷� �͵�
        [SerializeField]
        private AnimationClip animationClip;

        public void Skill(AbMainModule _mainModule)
        {
            Skill_Test(_mainModule, animationClip);
            //throw new System.NotImplementedException();
        }
    }
}