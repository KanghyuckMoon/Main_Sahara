using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    /// <summary>
    /// ��� �����
    /// </summary>
    public class EnvironmentColliderPlayer : MonoBehaviour
    {
        [SerializeField, Header("����� ���")]
        private AudioEnvironmentType _audioEnvironmentType = AudioEnvironmentType.Count;

        /// <summary>
        /// ������ ����� ���
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
