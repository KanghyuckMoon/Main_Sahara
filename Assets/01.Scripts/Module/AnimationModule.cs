using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using TimeManager;
using Pool;

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

        public AnimationModule(AbMainModule _mainModule)
        {

        }
        public AnimationModule() : base()
        {

        }

		public override void Init(AbMainModule _mainModule, params string[] _parameters)
		{
			base.Init(_mainModule, _parameters);
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

            //Debug.LogError(mainModule.ObjRotation.eulerAngles.x);

            Animator.SetFloat("BodyRotation", RotateWeight(mainModule.ObjRotation.eulerAngles.x));
        }

        private float RotateWeight(float _rotate)
        {
            float _angle = _rotate - 360;



            if (_angle < -100)
                return _rotate;

            else
                return _angle;
        }


		public void Receive()
        {
            SettingAnimatorSpeed();
        }
        public override void OnDisable()
        {
            animator = null;
            mainModule = null;
            StaticTime.Instance.RemoveObserver(this);
            base.OnDisable();
            ClassPoolManager.Instance.RegisterObject<AnimationModule>("AnimationModule", this);
        }
        public override void OnDestroy()
        {
            animator = null;
            mainModule = null;
            StaticTime.Instance.RemoveObserver(this);
            base.OnDestroy();
            ClassPoolManager.Instance.RegisterObject<AnimationModule>("AnimationModule", this);
        }
    }
}