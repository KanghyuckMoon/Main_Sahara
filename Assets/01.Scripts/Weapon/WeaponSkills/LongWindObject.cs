using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Pool;
using TimeManager;
using Effect;
using Utill.Coroutine;

public class LongWindObject : MonoBehaviour
{
    private void Update()
    {
        Vector3 pos = transform.forward * Time.deltaTime * 15;
        gameObject.transform.position += pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Vector3 pos = other.ClosestPoint(other.transform.position);
            EffectManager.Instance.SetEffectDefault("HitEffect1", pos, Quaternion.identity);
            //StartCoroutine(AttackFeedBack_TimeSlow());
            StaticCoroutineManager.Instance.InstanceDoCoroutine(AttackFeedBack_TimeSlow());

            ObjectPoolManager.Instance.RegisterObject("LongWeapon_Object", gameObject);
            gameObject.SetActive(false);
        }
    }

    IEnumerator AttackFeedBack_TimeSlow()
    {
        StaticTime.EntierTime = 0.02f;
        yield return new WaitForSecondsRealtime(0.2f);
        StaticTime.EntierTime = 1;
    }
}
