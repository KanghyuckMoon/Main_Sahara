using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace Buff
{
    public class FlameEffectDmg : MonoBehaviour
    {
        public string enemyLayerName;

        private void Update()
        {
            Collider[] colliders =
                Physics.OverlapSphere(transform.position, 1f);
            
            
            foreach (Collider col in colliders)
            {
                var _mainModule = col.GetComponent<AbMainModule>();
                //Debug.LogError(_mainModule);
                
                if (col.tag != enemyLayerName) continue;
                if (_mainModule.isFlameOn) continue;
                
                var _buffModule = _mainModule.GetModuleComponent<BuffModule>(ModuleType.Buff);
                if (_buffModule is null) return;
                _buffModule.AddBuff(
                    new Healing_Buf(_buffModule).SetValue(10).SetDuration(10).SetPeriod(2)
                        .SetSpownObjectName("HealEffect").SetSprite("Demon"), BuffType.Update);
            }
        }

        /*private void OnTriggerEnter(Collider other)
        {
            var _mainModule = other.GetComponent<AbMainModule>();
            Debug.LogError(_mainModule);
                
            if (other.name != enemyLayerName) return;
            if (_mainModule.isFlameOn) return;
                
            var _buffModule = _mainModule.GetModuleComponent<BuffModule>(ModuleType.Buff);
            _mainModule.isFlameOn = true;
            _buffModule.AddBuff(
                new Healing_Buf(_buffModule).SetValue(10).SetDuration(10).SetPeriod(2)
                    .SetSpownObjectName("HealEffect").SetSprite("Demon"), BuffType.Update);
        }*/
    }
}
