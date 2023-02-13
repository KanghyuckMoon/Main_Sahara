using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound;

namespace DynamicBGM
{
	public class DynamicBGMPlayer : MonoBehaviour
    {
        [SerializeField, Header("재생할 브금")]
        private AudioBGMType _audioBGMType = AudioBGMType.Count;

        /// <summary>
        /// 지정한 브금을 재생
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
