using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using TimeManager;
using DG.Tweening;
using UnityEngine.Rendering;

namespace PassiveItem
{
    public class HearLoudAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        
        private bool isDelay = false;

        private float delay;
        private float maxDelay = 9f;

        private Volume _volume;
        
        public HearLoudAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            _volume = mainModule.GetComponent<BodyRotation>().volume;
        }
        
        public void ApplyPassiveEffect()
        {
            
        }
        
        public void UpdateEffect()
        {
            if (Input.GetMouseButtonDown(2) && !isDelay)
            {
                DOTween.To(() => 1f, (x) => StaticTime.EnemyTime = x, 0.1f, 0.2f);
                DOTween.To(() => 0f, (x) => _volume.weight = x, 1f, 0.2f);
                isDelay = true;

                delay = maxDelay;
            }
            
            if (isDelay)
            {
                delay -= Time.deltaTime;
                if (delay <= 0)
                {
                    DOTween.To(() => 0.1f, (x) => StaticTime.EnemyTime = x, 1f, 0.2f);
                    DOTween.To(() => 1f, (x) => _volume.weight = x, 0f, 0.2f);
                    isDelay = false;
                }
            }
        }

        public void ClearPassiveEffect()
        {
            
        }
    }
}