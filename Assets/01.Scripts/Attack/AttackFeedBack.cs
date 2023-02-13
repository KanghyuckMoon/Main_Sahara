using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Effect;
using TimeManager;
using Utill.Addressable;

namespace Attack
{
    /// <summary>
    /// �̳༮�� ��� ���⿡ �� �༮�̸� ���⼭ ��� �������� ���� �������� ����.
    /// </summary>
    public class AttackFeedBack : MonoBehaviour
    {
        public UnityEvent<Vector3> attackFeedBackEvent;

        private PlayerLandEffectSO effectSO;

        private void Start()
        {
            effectSO = AddressablesManager.Instance.GetResource<PlayerLandEffectSO>("PlayerAttackEffect");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                //other.GetComponent < MainModule>.get(Hp).GetHit( attackEvent?.ReturnValue(ref dmg));
                attackFeedBackEvent?.Invoke(other.ClosestPoint(transform.position));
            }
        }

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
            StartCoroutine(AttackFeedBack_TimeSlow());
        }

        IEnumerator AttackFeedBack_TimeSlow()
        {
            StaticTime.EntierTime = 0.02f;
            yield return new WaitForSecondsRealtime(0.22f);
            StaticTime.EntierTime = 1;
        }
    }
}