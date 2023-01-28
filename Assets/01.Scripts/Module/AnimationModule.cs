using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public class AnimationModule : AbBaseModule
    {
        public Animator animator;
        public AnimationModule(MainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Awake()
        {
            animator = mainModule.GetComponentInParent<Animator>();
        }


        public override void FixedUpdate()
        {
            //animator.SetFloat("MoveSpeed", mainModule.moveSpeed);
            //animator.SetFloat("Jump", mainModule.objDir.y);
            animator.SetBool("IsGround", mainModule.isGround);
        }
    }
}