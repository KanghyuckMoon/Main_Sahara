using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sound
{
    /// <summary>
    /// 효과음 재생기
    /// </summary>
    public class EFFPlayer : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("effName"), Header("효과음 이름")] 
        private string _effName;

		private void OnEnable()
		{
            PlayEFF();
        }

        /// <summary>
        /// 효과음 재생
        /// </summary>
		public void PlayEFF()
        {
            SoundManager.Instance.PlayEFF(_effName);
        }
    }

}