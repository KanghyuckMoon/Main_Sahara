using System.Collections;
using System.Collections.Generic;
using Buff;
using UnityEngine;
using Module;

namespace Skill
{
    public class WeaponSkillFunctions : MonoBehaviour
    {
        public string animationName = "WeaponSkill";

        protected void PlaySkillAnimation(AbMainModule _mainModule, AnimationClip _animationClip)
        {
            _mainModule.AnimatorOverrideController[animationName] = _animationClip;
            //_mainModule.Animator.Play(animationName);

            _mainModule.Animator.SetBool(animationName, true);
        }

        protected void GetBuff(AbMainModule _mainModule)
        {
            BuffModule _bufmodule= _mainModule.GetModuleComponent<BuffModule>(ModuleType.Buff); 
            _bufmodule.AddBuff(new ReduceDamage_Buf(_bufmodule)
                .SetValue(10)
                .SetDuration(10)
                .SetPeriod(11)
                .SetSpownObjectName("HealEffect")
                .SetSprite("Demon"), Bufftype.Update);
        }
    }
}