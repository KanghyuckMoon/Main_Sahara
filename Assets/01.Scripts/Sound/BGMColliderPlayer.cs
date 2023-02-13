using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    /// <summary>
    /// ��� �����
    /// </summary>
    public class BGMColliderPlayer : MonoBehaviour
    {
        [SerializeField, Header("����� ���")]
        private AudioBGMType _audioBGMType = AudioBGMType.Count;

        /// <summary>
        /// ������ ����� ���
        /// </summary>
        public void PlayBGM()
        {
            SoundManager.Instance.PlayBGM(_audioBGMType);
        }

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
                PlayBGM();

            }
		}

	}
}
