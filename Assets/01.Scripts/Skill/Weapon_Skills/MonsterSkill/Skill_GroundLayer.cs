using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Streaming;
using Pool;

namespace Skill
{

    public class Skill_GroundLayer : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;
        
        public void Skills(AbMainModule _mainModule)
        {
            PlaySkillAnimation(_mainModule, animationClip);
            _mainModule.GravityScale = -9.8f;
            Animator _animator = _mainModule.GetComponent<Animator>();
            _animator.SetLayerWeight(0, 0f);
            _animator.SetLayerWeight(1, 1f);
        }
    }   
}
