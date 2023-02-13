using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound;

namespace DynamicBGM
{
	public class DynamicBGMPlayer : MonoBehaviour
    {
        [SerializeField, Header("����� ���")]
        private AudioBGMType _audioBGMType = AudioBGMType.Count;

        /// <summary>
        /// ������ ����� ���
        /// </summary>
        public void PlayBGM()
        {
            DynamicBGMManager.Instance.SetBGMType(_audioBGMType);
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
