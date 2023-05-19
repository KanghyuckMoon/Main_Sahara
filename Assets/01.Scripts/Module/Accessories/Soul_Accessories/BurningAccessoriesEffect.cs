using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using TimeManager;
using DG.Tweening;
using Pool;
using UnityEngine.Rendering;
using Utill.Addressable;
using Buff;
using EquipmentSystem;

namespace PassiveItem
{
    public class BurningAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        
        private bool isDelay = false;

        private float delay;
        private float maxDelay = 9f;
        private GameObject gameObject;

        //private GameObject head;
        
        public BurningAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            if (mainModule.name != "Player") return;
            //head = mainModule.GetComponentInChildren<EquipPosition>().gameObject;
        }
        
        public void ApplyPassiveEffect()
        {
            if (mainModule.name != "Player") return;
            gameObject = ObjectPoolManager.Instance.GetObject("BurnningEffect");
            gameObject.transform.SetParent(mainModule.transform, false);
            
            gameObject.transform.localPosition = Vector3.up;
            gameObject.transform.localRotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

            gameObject.SetActive(true);
            FlameEffectDmg _fire = gameObject.GetComponent<FlameEffectDmg>();
            _fire.enemyLayerName = "Enemy";
        }
        
        public void UpdateEffect()
        {
        }

        public void ClearPassiveEffect()
        {
            gameObject.SetActive(false);
            ObjectPoolManager.Instance.RegisterObject("BurnningEffect", gameObject);
        }

        public void UpgradeEffect()
        {
            
        }
    }
}