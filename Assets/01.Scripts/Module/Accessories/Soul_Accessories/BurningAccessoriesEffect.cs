using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using TimeManager;
using DG.Tweening;
using Pool;
using UnityEngine.Rendering;
using Utill.Addressable;

namespace PassiveItem
{
    public class BurningAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        
        private bool isDelay = false;

        private float delay;
        private float maxDelay = 9f;

        private GameObject head;
        
        public BurningAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            head = mainModule.GetComponentInChildren<Head>().gameObject;
        }
        
        public void ApplyPassiveEffect()
        {
            GameObject fire = ObjectPoolManager.Instance.GetObject("FireEffect_1");
            fire.transform.SetParent(head.transform);
            
            fire.transform.localPosition = Vector3.zero;
            fire.transform.localRotation = Quaternion.identity;

            fire.SetActive(true);
        }
        
        public void UpdateEffect()
        {
        }

        public void ClearPassiveEffect()
        {
            
        }
    }
}