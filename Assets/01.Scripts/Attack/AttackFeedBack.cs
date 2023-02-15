using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Effect;
using TimeManager;
using Utill.Addressable;
using Utill.Coroutine;

namespace Attack
{
    /// <summary>
    /// 이녀석은 모든 무기에 들어갈 녀석이며 여기서 모든 공격했을 때의 설정들이 들어간다.
    /// </summary>
    public class AttackFeedBack : MonoBehaviour
    {
        public UnityEvent<Vector3> attackFeedBackEvent;

        private PlayerLandEffectSO effectSO;

        private void Start()
        {
            effectSO = AddressablesManager.Instance.GetResource<PlayerLandEffectSO>("PlayerAttackEffect");
        }

        public void InvokeEvent(Vector3 closetPoint)
        {
            attackFeedBackEvent?.Invoke(closetPoint);
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    string tagName = gameObject.tag == "Player" ? "Enemy" : "Player";
        //    if (other.CompareTag(tagName))
        //    {
        //        //other.GetComponent < MainModule>.get(Hp).GetHit( attackEvent?.ReturnValue(ref dmg));
        //        attackFeedBackEvent?.Invoke(other.ClosestPoint(transform.position));
        //    }
        //}

        private void OnEnable()
        {
            attackFeedBackEvent.AddListener((vec) => AttackEffect(vec));
            attackFeedBackEvent.AddListener((vec) => TimeSlow(vec));
        }

        private void OnDisable()
        {
            attackFeedBackEvent.RemoveAllListeners();
        }

        private void AttackEffect(Vector3 vec)
        {
            EffectManager.Instance.SetEffectDefault("HitEffect1", vec, Quaternion.identity);
        }

        private void TimeSlow(Vector3 vec)
        {
            StaticCoroutineManager.Instance.InstanceDoCoroutine(AttackFeedBack_TimeSlow());
        }

        IEnumerator AttackFeedBack_TimeSlow()
        {
            StaticTime.EntierTime = 0.02f;
            yield return new WaitForSecondsRealtime(0.22f);
            StaticTime.EntierTime = 1;
        }
    }
}