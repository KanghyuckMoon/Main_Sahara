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
        //여기다가 값들을 넣으면 된다. 데미지나 속도, 뭐 그런 것들
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