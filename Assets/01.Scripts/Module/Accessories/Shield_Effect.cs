using System;
using System.Collections;
using System.Collections.Generic;
using Module;
using Pool;
using UnityEngine;

namespace PassiveItem
{
    public class Shield_Effect : MonoBehaviour
    {
        private string name = "Shield_Prefab";
        
        private void OnEnable()
        {
            StartCoroutine(ShieldEffect());
        }

        IEnumerator ShieldEffect()
        {
            
            yield return new WaitForSeconds(7f);

            GetComponentInParent<AbMainModule>().IsCanHit = false;
            ObjectPoolManager.Instance.RegisterObject(name, gameObject);
            gameObject.SetActive(false);
        }
    }
}
