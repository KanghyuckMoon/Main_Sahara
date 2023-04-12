using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager;
using Effect;
//using Module;

namespace Weapon
{
    public class Weapon_Sword : BaseWeapon
    {
        /*private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tagName))
            {
                Vector3 pos = other.ClosestPoint(other.transform.position);
                EffectManager.Instance.SetEffectDefault(WeaponDataSO.effectName, pos, Quaternion.identity);
                StopAllCoroutines();
                StartCoroutine(AttackFeedBack_TimeSlow());
            }
        }

        IEnumerator AttackFeedBack_TimeSlow()
        {
            StaticTime.EntierTime = 0.03f;
            yield return new WaitForSecondsRealtime(0.15f);
            StaticTime.EntierTime = 1;
        }*/
    }
}