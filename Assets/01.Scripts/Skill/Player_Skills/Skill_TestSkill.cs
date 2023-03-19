using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Pool;
using Utill.Pattern;

namespace Skill
{
    public class Skill_TestSkill : SkillFunctions, ISkill
    {
        //����ٰ� ������ ������ �ȴ�. �������� �ӵ�, �� �׷� �͵�
        [SerializeField]
        private AnimationClip animationClip;

        [SerializeField] private string effectName;

        public void Skill(AbMainModule _mainModule)
        {
            GameObject _effect = ObjectPoolManager.Instance.GetObject(effectName);
            Vector3 _currentPos = _mainModule.transform.position;
            _effect.transform.position = _currentPos + new Vector3(0, 0.1f, 0);
            _effect.SetActive((true));
            
            Skill_Test(_mainModule, animationClip);
            //throw new System.NotImplementedException();
        }
    }
}