using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using TimeManager;
using DG.Tweening;
using Pool;
using UnityEngine.Rendering;

namespace PassiveItem
{
    public class ShieldAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        
        private bool isDelay = false;

        private float delay;
        private float maxDelay = 9f;

        private GameObject obj;
        
        public ShieldAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
        }

        public void ApplyPassiveEffect()
        {
            if (mainModule.name != "Player") return;
            
            obj = ObjectPoolManager.Instance.GetObject("Shield_Prefab");
            obj.transform.SetParent(mainModule.transform);
            obj.transform.position = new Vector3(0, 0.8f, 0);
            obj.SetActive(true);
        }

        public void UpdateEffect()
        {
            
        }

        public void ClearPassiveEffect()
        {
            
        }

        public void UpgradeEffect()
        {
            
        }
    }
}