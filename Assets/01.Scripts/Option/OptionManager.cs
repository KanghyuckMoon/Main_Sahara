using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace Option
{
    public class OptionManager : Singleton<OptionManager>
    {
        public OptionData optionData = new OptionData();

        /// <summary>
        /// �׷��� ���ð��� ������Ʈ 
        /// </summary>
        public void UpdateGraphicsData()
        {
            // �ػ� ������Ʈ 
            Screen.SetResolution(optionData.width,optionData.height,optionData.isFullScreen);
            Application.targetFrameRate = optionData.framerate; 
        }

        /// <summary>
        /// ���� ���ð��� ������Ʈ 
        /// </summary>
        public void UpdateSoundData()
        {
            
        }
    }   
}
