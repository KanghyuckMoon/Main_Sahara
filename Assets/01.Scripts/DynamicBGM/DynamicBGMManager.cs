using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound;
using Utill.Pattern;

namespace DynamicBGM
{
    public class DynamicBGMManager : MonoSingleton<DynamicBGMManager>
    {
        private int enemyCount = 0;
        private AudioBGMType audioBGMType = AudioBGMType.Field_Desert;

        public void AddEnemyCount()
		{
            ++enemyCount;
            SetBGM();
        }

        public void RemoveEnemyCount()
        {
            --enemyCount;
            SetBGM();
        }

        public void SetBGMType(AudioBGMType _audioBGMType)
		{
            audioBGMType = _audioBGMType;
            SetBGM();
        }

        private void SetBGM()
		{
            if (enemyCount > 0)
            {
                SoundManager.Instance.PlayBGM(AudioBGMType.Field_Battle);
            }
            else
			{
                SoundManager.Instance.PlayBGM(audioBGMType);
			}
		}

    }
}
