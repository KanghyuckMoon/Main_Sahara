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
        /// 그래픽 세팅관련 업데이트 
        /// </summary>
        public void UpdateGraphicsData()
        {
            // 해상도 업데이트 
            Screen.SetResolution(optionData.width,optionData.height,optionData.isFullScreen);
            Application.targetFrameRate = optionData.framerate; 
        }

        /// <summary>
        /// 사운드 세팅관련 업데이트 
        /// </summary>
        public void UpdateSoundData()
        {
            
        }
    }   
}
