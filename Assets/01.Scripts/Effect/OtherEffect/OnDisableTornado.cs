using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{
    public class OnDisableTornado : MonoBehaviour
    {
        [SerializeField] private string disableEffectAddress;
        
        private void TornadoDisable()
        {
            EffectManager.Instance.SetEffectDefault(disableEffectAddress, transform.position, Quaternion.identity);
        }
        
        public void OnDisable()
        {
            TornadoDisable();
        }
    }   
}
