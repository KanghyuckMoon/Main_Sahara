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
    /// �̳༮�� ��� ���⿡ �� �༮�̸� ���⼭ ��� �������� ���� �������� ����.
    /// </summary>
    public class AttackFeedBack : MonoBehaviour
    {
        public UnityEvent<Vector3, string> attackFeedBackEvent;

        private PlayerLandEffectSO effectSO;

        private void Start()
        {
            effectSO = AddressablesManager.Instance.GetResource<PlayerLandEffectSO>("PlayerAttackEffect");
        }

        public void InvokeEvent(Vector3 closetPoint, string _effectName)
        {
            AddEvent();
            attackFeedBackEvent?.Invoke(closetPoint, _effectName);
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
            attackFeedBackEvent.AddListener((vec, s) => AttackEffect(vec, s));
            attackFeedBackEvent.AddListener((vec, s) => TimeSlow());
        }

        private void AddEvent()
        {
            attackFeedBackEvent.AddListener((vec, s) => AttackEffect(vec, s));
            attackFeedBackEvent.AddListener((vec, s) => TimeSlow());
        }

        private void OnDisable()
        {
            attackFeedBackEvent.RemoveAllListeners();
        }

        private void AttackEffect(Vector3 vec, string _efectName)
        {
            if (_efectName == null || _efectName == "NULL")
            {
                return;
            }
            EffectManager.Instance.SetEffectDefault(_efectName, vec, Quaternion.identity);
        }

        private void TimeSlow()
        {
            StaticCoroutineManager.Instance.InstanceDoCoroutine(AttackFeedBack_TimeSlow());
        }

        IEnumerator AttackFeedBack_TimeSlow()
        {
            StaticTime.EntierTime = 0.02f;
            yield return new WaitForSeconds(0.22f);
            StaticTime.EntierTime = 1;
        }
    }
}