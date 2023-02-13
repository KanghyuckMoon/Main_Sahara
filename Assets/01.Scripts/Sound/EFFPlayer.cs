using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sound
{
    /// <summary>
    /// ȿ���� �����
    /// </summary>
    public class EFFPlayer : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("effName"), Header("ȿ���� �̸�")] 
        private string _effName;

		private void OnEnable()
		{
            PlayEFF();
        }

        /// <summary>
        /// ȿ���� ���
        /// </summary>
		public void PlayEFF()
        {
            SoundManager.Instance.PlayEFF(_effName);
        }
    }

}