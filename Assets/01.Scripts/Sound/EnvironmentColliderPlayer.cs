using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    /// <summary>
    /// 브금 재생기
    /// </summary>
    public class EnvironmentColliderPlayer : MonoBehaviour
    {
        [SerializeField, Header("재생할 브금")]
        private AudioEnvironmentType _audioEnvironmentType = AudioEnvironmentType.Count;

        /// <summary>
        /// 지정한 브금을 재생
        /// </summary>
        public void PlayEnvironment()
        {
            SoundManager.Instance.PlayEnvironment(_audioEnvironmentType);
        }


        private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
                PlayEnvironment();
            }
		}

	}
}
