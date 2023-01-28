using System.Collections;
using UnityEngine;
using Utill;
using Utill.Pattern;

namespace Utill.Coroutine
{
	/// <summary>
	/// 코루틴 클래스를 모노싱글톤화하여 스태틱메서드에서도 실행할 수 있는 코루틴
	/// </summary>
	public class StaticCoroutineManager : MonoSingleton<StaticCoroutineManager>
	{
		/// <summary>
		/// 스태틱 코루틴 함수
		/// </summary>
		/// <param name="_coroutine"></param>
		public static void DoCoroutine(IEnumerator _coroutine)
		{     
			Instance.StartCoroutine(Instance.Perform(_coroutine));
		}

		private IEnumerator Perform(IEnumerator _coroutine)
		{
			yield return StartCoroutine(_coroutine); 
		}
	}

}