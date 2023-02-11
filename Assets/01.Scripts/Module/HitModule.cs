using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Effect;
using Utill.Addressable;
using Utill.Pattern;

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
        private StateModule StateModule
        {
            get
            {
                stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
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
        private StateModule stateModule;
        private AnimationModule animationModule;

        public HitModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            effectSO = AddressablesManager.Instance.GetResource<PlayerLandEffectSO>("PlayerAttackEffect");
        }

        public void GetHit(int dmg)
        {
            HitFeedBack();
            HpModule.GetDamage(dmg);
            if (HpModule.CurrentHp == 0)
            {
                Dead();
            }
            else
            {
                Hit();
            }
        }

        private void HitFeedBack()
        {
            //자기자신 타임느리게 하기
            mainModule.transform.DOShakePosition(0.15f, 0.2f, 180, 160);
        }

        private void Hit()
        {
            //animationModule.animator.St 
            mainModule.isHit = true;
            AnimationModule.animator.Play("Hit", 0, 0);
            AnimationModule.animator.Play("Hit", 2, 0);
        }

        private void Dead()
        {
            AnimationModule.animator.Play("Dead");
            mainModule.isDead = true;
            mainModule.characterController.enabled = false;

            mainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon).currentWeapon.GetComponent<Collider>().enabled = false;
        }
    }
}