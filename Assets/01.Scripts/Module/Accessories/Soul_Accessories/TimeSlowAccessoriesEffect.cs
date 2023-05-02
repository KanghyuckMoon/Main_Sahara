using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using TimeManager;
using DG.Tweening;
using UnityEngine.Rendering;

namespace PassiveItem
{
    public class TimeSlowAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        
        private bool isDelay = false;

        private float delay;
        private float maxDelay = 9f;

        private Volume _volume;
        
        private GameObject dashEffect;
        
        public TimeSlowAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            dashEffect = mainModule.GetComponent<BodyRotation>()?.dashEffect;
            dashEffect?.SetActive(false);
            _volume = mainModule.GetComponent<BodyRotation>()?.volume;
        }
        
        public void ApplyPassiveEffect()
        {
            
        }
        
        public void UpdateEffect()
        {
            if (Input.GetMouseButtonDown(2) && !isDelay)
            {
                DOTween.To(() => 1f, (x) => StaticTime.EnemyTime = x, 0.1f, 0.4f);
                DOTween.To(() => 1f, (x) => StaticTime.PlayerTime = x, 0.85f, 0.4f);
                DOTween.To(() => 0f, (x) => _volume.weight = x, 1f, 0.4f);
                dashEffect?.SetActive(true);
                isDelay = true;

                delay = maxDelay;
            }
            
            if (isDelay)
            {
                delay -= Time.deltaTime;
                if (delay <= 0)
                {
                    DOTween.To(() => 0.1f, (x) => StaticTime.EnemyTime = x, 1f, 0.4f);
                    DOTween.To(() => 0.85f, (x) => StaticTime.PlayerTime = x, 1f, 0.4f);
                    DOTween.To(() => 1f, (x) => _volume.weight = x, 0f, 0.4f);
                    dashEffect?.SetActive(false);
                    isDelay = false;
                }
            }
        }

        public void ClearPassiveEffect()
        {
            
        }

        public void UpgradeEffect()
        {
            
        }
    }
}