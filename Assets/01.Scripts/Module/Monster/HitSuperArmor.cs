using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
	public class HitSuperArmor : MonoBehaviour
	{
		[SerializeField]
		private Material superArmorMat;

		[SerializeField]
		private Material originArmorMat;

		[SerializeField]
		private Renderer renderer;

		private Coroutine coroutine;

		public void HitToSuperArmor()
		{
			if(coroutine != null)
			{
				StopCoroutine(coroutine);
			}
			coroutine = StartCoroutine(SuperArmor());
		}

		private IEnumerator SuperArmor()
		{
			renderer.material = superArmorMat;
			yield return new WaitForSeconds(0.1f);
			renderer.material = originArmorMat;

		}
	}
}