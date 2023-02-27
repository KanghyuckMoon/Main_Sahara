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
        
        public Animator Animator
		{
            get
            {
				try
				{
                    animator ??= mainModule.GetComponentInChildren<Animator>();
				}
				catch
				{
                    animator = mainModule.GetComponentInChildren<Animator>();
				}
                return animator;
            }
			set
			{
                animator = value;
            }
		}
        private int swordLayerIndex;

        public AnimationModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        private void SettingAnimatorSpeed()
        {
            Animator.speed = StaticTime.EntierTime;
        }

        public override void Awake()
        {
            swordLayerIndex = Animator.GetLayerIndex("Sword");
            
            StaticTime.Instance.AddObserver(this);
        }

        public override void FixedUpdate()
        {
            //animator.SetFloat("MoveSpeed", mainModule.moveSpeed);
            //animator.SetFloat("Jump", mainModule.objDir.y);
            Animator.SetBool("IsGround", mainModule.isGround);
            Animator.SetBool("JumpBuf", mainModule.IsJumpBuf);
            Animator.SetBool("WeaponExist", mainModule.IsWeaponExist);

            Animator.SetFloat("MoveX", mainModule.ObjDir.x);
            Animator.SetFloat("MoveY", mainModule.ObjDir.y);
            Animator.SetBool("Charge", mainModule.IsCharging);
        }

		public override void OnDestroy()
		{
			base.OnDestroy();
            animator = null;
        }

		public void Receive()
        {
            SettingAnimatorSpeed();
        }
    }
}