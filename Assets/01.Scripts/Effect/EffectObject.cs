using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Pool;

namespace Effect
{

    public class EffectObject : MonoBehaviour
    {
        [SerializeField]
        private string address;
        [SerializeField]
        private float disableDelay = 1f;
        [SerializeField]
        private bool isInvokeDisable;

        private Coroutine coroutine;


        private void OnEnable()
        {
            if (isInvokeDisable)
			{
                if (coroutine is not null)
				{
                    StopCoroutine(coroutine);
                    coroutine = null;
				}

                coroutine = StartCoroutine(SetActiveFalse());
			}
        }

		private void OnDisable()
        {
            if (!isInvokeDisable)
            {
                Invoke("ReParent", 0.1f);
            }
        }

        private IEnumerator SetActiveFalse()
		{
            yield return new WaitForSeconds(disableDelay);
            gameObject.SetActive(false);
            ReParent();

        }

        private void ReParent()
        {
            ObjectPoolManager.Instance.RegisterObject(address, gameObject);
        }

    }

}