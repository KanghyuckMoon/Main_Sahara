using System.Collections;
using System.Collections.Generic;
using Buff;
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

            BuffModule _buffModule = _mainModule.GetModuleComponent<BuffModule>(ModuleType.Buff);
            
            _buffModule.AddBuff(new Healing_Buf(_buffModule)
                .SetDuration(10)
                .SetPeriod(2)
                .SetValue(5)
                .SetSprite("HealEffect_Icon")
                .SetSpownObjectName("HealEffect"), BuffType.Update);
            
            Skill_Test(_mainModule, animationClip);
            //throw new System.NotImplementedException();
        }
    }
}