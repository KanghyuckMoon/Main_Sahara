using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using TimeManager;

namespace Module
{
    public class AnimationModule : AbBaseModule, Observer
    {
        public Animator animator;
        private int swordLayerIndex;

        public AnimationModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        private void SettingAnimatorSpeed()
        {
            animator.speed = StaticTime.EntierTime;
        }

        public override void Awake()
        {
            animator = mainModule.GetComponent<Animator>();
            swordLayerIndex = animator.GetLayerIndex("GreatSword");
            
            StaticTime.Instance.AddObserver(this);
        }

        public override void Update()
        {
            animator.SetBool("Attack", mainModule.attacking);
            animator.SetBool("StrongAttack", mainModule.strongAttacking);
        }

        public override void FixedUpdate()
        {
            //animator.SetFloat("MoveSpeed", mainModule.moveSpeed);
            //animator.SetFloat("Jump", mainModule.objDir.y);
            animator.SetBool("IsGround", mainModule.isGround);
            animator.SetBool("JumpBuf", mainModule.isJumpBuf);
            animator.SetBool("WeaponExist", mainModule.isWeaponExist);
        }

        public void Receive()
        {
            SettingAnimatorSpeed();
        }
    }
}