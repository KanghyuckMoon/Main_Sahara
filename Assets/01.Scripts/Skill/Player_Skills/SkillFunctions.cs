using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace Skill
{
    public class SkillFunctions : MonoBehaviour
    {
        public string skillAnimationName = "Skill";

        protected void Skill_Test(AbMainModule _mainModule, AnimationClip _animation)
        {
            _mainModule.AnimatorOverrideController[skillAnimationName] = _animation;
            //_mainModule.AnimatorOverrideController.
            //_mainModule.Animator.Play(skillAnimationName);

            _mainModule.Animator.SetBool(skillAnimationName, true);

            Debug.Log("�̰� �׽�Ʈ");
        }
    }
}