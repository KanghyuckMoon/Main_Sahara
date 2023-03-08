using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace Skill
{
    public class WeaponSkillFunctions : MonoBehaviour
    {
        public string animationName = "WeaponSkill";

        protected void TestWeaponFunc(AbMainModule _mainModule, AnimationClip _animationClip)
        {
            _mainModule.AnimatorOverrideController[animationName] = _animationClip;
            //_mainModule.Animator.Play(animationName);

            _mainModule.Animator.SetBool(animationName, true);
        }
    }
}