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
        private HpModule hpModule;
        private StateModule stateModule;
        private AnimationModule animationModule;

        private PlayerLandEffectSO effectSO;
        //private 

        public HitModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            hpModule = mainModule.GetModuleComponent<HpModule>(ModuleType.Hp);
            stateModule = mainModule.GetModuleComponent<StateModule>(ModuleType.State);
            animationModule = mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation);

            effectSO = AddressablesManager.Instance.GetResource<PlayerLandEffectSO>("PlayerAttackEffect");
        }

        public void GetHit(int dmg)
        {
            HitFeedBack();
            hpModule.GetDamage(dmg);
            if (hpModule.CurrentHp == 0)
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
            animationModule.animator.Play("Hit", -1, 0);
        }

        private void Dead()
        {
            animationModule.animator.Play("Dead", -1, 0);
            mainModule.isDead = true;
            mainModule.characterController.enabled = false;

            mainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon).currentWeapon.GetComponent<Collider>().enabled = false;
        }
    }
}