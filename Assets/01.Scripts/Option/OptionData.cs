using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

namespace Option
{
    [System.Serializable]
    public class OptionData
    {
        //Sound
        public float masterVolume = 1f; 
        public float bgmVolume = 1f;
        public float effVolume = 1f;
        public float envirVolume = 1f;
        
        //Grapic
        public int grapicQulityIndex;
        public int width;
        public int height;
        public bool isFullScreen;

        public int framerate; //�ֻ��� 
        public int shadowQuality; // �׸��� ǰ�� 0~3
        public int textureQuality; // �ؽ��� ǰ��  0~2
        
        
        public List<InputData> inputDataList = new List<InputData>();
    }   
}
