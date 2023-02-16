using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    /// <summary>
    /// ��� �����
    /// </summary>
    public class EnvironmentPlayer : MonoBehaviour
    {
        [SerializeField, Header("������ �� ����� ������� ����")]
        private bool _isPlayerOnAwake = false;
        [SerializeField, Header("����� ���")]
        private AudioEnvironmentType _audioEnvironmentType = AudioEnvironmentType.Count;

        /// <summary>
        /// ������ ����� ���
        /// </summary>
        public void PlayEnvironment()
        {
            SoundManager.Instance.PlayEnvironment(_audioEnvironmentType);
        }

        private void Start()
        {
            if (_isPlayerOnAwake)
            {
                PlayEnvironment();
            }
        }

    }
}
