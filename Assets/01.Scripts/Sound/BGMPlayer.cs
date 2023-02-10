using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    /// <summary>
    /// 브금 재생기
    /// </summary>
    public class BGMPlayer : MonoBehaviour
    {
        [SerializeField, Header("시작할 때 브금을 재생할지 여부")]
        private bool _isPlayerOnAwake = false;
        [SerializeField, Header("재생할 브금")]
        private AudioBGMType _audioBGMType = AudioBGMType.Count;

        /// <summary>
        /// 지정한 브금을 재생
        /// </summary>
        public void PlayBGM()
        {
            SoundManager.Instance.PlayBGM(_audioBGMType);
        }

        private void Start()
        {
            if (_isPlayerOnAwake)
            {
                PlayBGM();
            }
        }

    }
}
