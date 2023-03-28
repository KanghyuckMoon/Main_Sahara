using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;
using Pool;
using Utill.Pattern;
using Effect;

public class VFXEffect : MonoBehaviour
{
		[SerializeField]
		private VisualEffect visualEffect;
		[SerializeField]
		public string addressName;
		[SerializeField] 
		private float disableTime = 5f;

		public void OnEnable()
		{
			visualEffect.Stop();
			visualEffect.Play();
			StartCoroutine(Disable());
		}

		private IEnumerator Disable()
		{
			yield return new WaitForSeconds(disableTime);
			ObjectPoolManager.Instance.RegisterObject(addressName, gameObject);
			gameObject.SetActive(false);
		}
}
