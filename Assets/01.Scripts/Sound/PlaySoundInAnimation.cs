using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
	public class PlaySoundInAnimation : MonoBehaviour
	{
		/// <summary>
		/// ȿ���� ���
		/// </summary>
		public void OnPlayEFF(string _effName)
		{
			SoundManager.Instance.PlayEFF(_effName);
		}
	}
}