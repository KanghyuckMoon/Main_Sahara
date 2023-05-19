using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using TimeManager;
using DG.Tweening;
using Pool;
using UnityEngine.Rendering;
using Weapon;
using Buff;

namespace PassiveItem
{
    public class FlameAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        
        private bool isDelay = false;

        private float delay;
        private float maxDelay = 9f;

        private Volume _volume;
        
        private GameObject flame;

        private bool isOn;
        
        public FlameAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
        }
        
        public void ApplyPassiveEffect()
        {
            
        }

        public void SetFlame()
        {   
            WeaponSpownObject[] _pos = mainModule.GetComponentsInChildren<WeaponSpownObject>();
            foreach (var VARIABLE in _pos)
            {
                if (VARIABLE.weaponHand == WeaponHand.Weapon)
                {
                    flame = ObjectPoolManager.Instance.GetObject("FlameEffect_Weapon");
                    flame.transform.SetParent(VARIABLE.transform, false);

                    flame.transform.localPosition = Vector3.zero;
                    flame.SetActive(true);

                    FlameEffectDmg _fire = flame.GetComponent<FlameEffectDmg>();
                    _fire.enemyLayerName = "Enemy";
                }
            }
        }

        public void UpdateEffect()
        {
            if (isOn) return;
            if (mainModule.IsWeaponExist)
            {
                if (Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    mainModule.PlayAllAnimation("SetFire");
                    
                    //for()
                    //mainModule.Animator.layerCount
                    
                    SetFlame();
                    
                    isOn = true;
                }
            }
        }

        public void ClearPassiveEffect()
        {
            isOn = false;
        }

        public void UpgradeEffect()
        {
            
        }
    }
}