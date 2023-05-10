using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Effect;
using Utill.Addressable;
using Utill.Pattern;
using Pool;

namespace Module
{
    public class HitModule : AbBaseModule
    {
        private HpModule HpModule
        {
            get
            {
                hpModule ??= mainModule.GetModuleComponent<HpModule>(ModuleType.Hp);
                return hpModule;
            }
        }
        private StatModule StateModule
        {
            get
            {
                stateModule ??= mainModule.GetModuleComponent<StatModule>(ModuleType.Stat);
                return stateModule;
            }
        }
        private AnimationModule AnimationModule
        {
            get
            {
                animationModule ??= mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation); ;
                return animationModule;
            }
        }
        private PlayerLandEffectSO effectSO;
        //private 

        private HpModule hpModule;
        private StatModule stateModule;
        private AnimationModule animationModule;

        private float currentHitDelay = 0;
        private bool isHit = false;

        public int lifeCount = 0;
        public int lifeHealValue = 0;
        
        public HitModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }
        public HitModule() : base()
        {

        }


        public override void Start()
        {
            effectSO = AddressablesManager.Instance.GetResource<PlayerLandEffectSO>("PlayerAttackEffect");
        }

        public void GetHit(int dmg)
        {
            //if (HitDelay())
            //{
                //HitFeedBack();
                
                if (HpModule.GetDamage(dmg) == 0)
                {
                    Dead();
                }
                else
                {
                    Hit();
                }
            //}
        }

        private bool HitDelay()
        {
            bool _isHit = isHit;

            if (_isHit)
            {
                isHit = false;
                currentHitDelay = 0;
            }

            return _isHit;
        }

        private void HitFeedBack()
        {
            //자기자신 타임느리게 하기
            //mainModule.transform.DOShakePosition(0.15f, 0.2f, 180, 160);
        }

        private void Hit()
        {
            //animationModule.animator.St 
            mainModule.IsHit = true;
            AnimationModule.animator.Play("Hit");
            //animationModule.animator.
            AnimationModule.animator.SetBool("Hit", true);
            
        }

        private void Dead()
        {
            if (lifeCount <= 0)
            {
                AnimationModule.animator.Play("Dead");
                mainModule.IsDead = true;
                mainModule.CharacterController.enabled = false;
            }
            else
            {
                hpModule.GetHeal(lifeCount * (int)(hpModule.GetMaxHp() / 0.25f));
                lifeCount = 0;
            }

            //mainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon).currentWeapon.GetComponent<Collider>().enabled = false;
        }

        public override void OnDisable()
        {
            hpModule = null;
            stateModule = null;
            animationModule = null;
            base.OnDisable();
            ClassPoolManager.Instance.RegisterObject<HitModule>("HitModule", this);
        }
    }
}