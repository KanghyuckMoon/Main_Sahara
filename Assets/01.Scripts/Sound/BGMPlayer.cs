using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    /// <summary>
    /// ��� �����
    /// </summary>
    public class BGMPlayer : MonoBehaviour
    {
        [SerializeField, Header("������ �� ����� ������� ����")]
        private bool _isPlayerOnAwake = false;
        [SerializeField, Header("����� ���")]
        private AudioBGMType _audioBGMType = AudioBGMType.Count;

        /// <summary>
        /// ������ ����� ���
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
