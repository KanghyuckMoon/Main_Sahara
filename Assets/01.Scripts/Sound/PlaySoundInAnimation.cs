using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
	public class PlaySoundInAnimation : MonoBehaviour
	{
		/// <summary>
		/// 효과음 재생
		/// </summary>
		public void OnPlayEFF(string _effName)
		{
			SoundManager.Instance.PlayEFF(_effName);
		}
	}
}