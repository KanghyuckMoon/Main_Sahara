using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Option
{
    
public class GraphicsSetting : MonoBehaviour
{
    public bool IsFoolScreen
    {
        get
        {
            return _isFoolScreen;
        }
        set
        {
            _isFoolScreen = value;
        }
    }

    public int Width
    {
        get
        {
            return _width;
        }
        set
        {
            _width = value;
        }
    }

    public int Height
    {
        get
        {
            return _height;
        }
        set
        {
            _height = value;
        }
    }

    public int Framerate
    {
        get
        {
            return framerate; 
        }
        set
        {
            framerate = value; 
        }
    }

    public int Fps
    {
        get
        {
            return fps; 
        }
        set
        {
            fps = value;   
        } 
    }

    private bool _isFoolScreen = true;
    private int _width = 1920;
    private int _height = 1080;

    private int framerate = 144;
    private int antialiacing = 0;
    private int fps;

    public void ChangeFps(int _fps)
    {
        fps = _fps; 
        Application.targetFrameRate = fps; 
    }
    /// <summary>
    /// 퀄리티 레벨 변경
    /// </summary>
    /// <param name="index"></param>
    public void ChangeGrapicSetting(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
    }

    /// <summary>
    /// 전체화면 여부 변경
    /// </summary>
    public void ChangeFoolScreen()
    {
        IsFoolScreen = !IsFoolScreen;
    }

    /// <summary>
    /// 화면 해상도 변경
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public void ChangeSize(int width, int height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// 화면 설정 적용
    /// </summary>
    public void ApplySettingScreen()
    {
        Screen.SetResolution(Width, Height, IsFoolScreen);
        OptionManager.Instance.optionData.width = Width;
        OptionManager.Instance.optionData.height = Height;
        OptionManager.Instance.optionData.isFullScreen = IsFoolScreen;
        Application.targetFrameRate = framerate;

        QualitySettings.antiAliasing = antialiacing; 
    }

    #region gpt 
    
    [SerializeField]
    private Resolution[] resolutions;
    List<string> resolutionOptions = new List<string>();
    public int currentResolutionIndex = 0;
    public int shadowQualityIndex = 0;
    public int isFullScreen = 0;  
    

    private void Start()
    {
        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionOptions.Add(resolutions[i].width + " x " + resolutions[i].height);
            
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        shadowQualityIndex = (int)QualitySettings.shadows;
    }

    /// <summary>
    /// 해상도 설정 
    /// </summary>
    /// <param name="resolutionIndex"></param>
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// 텍스쳐 
    /// </summary>
    /// <param name="qualityIndex"></param>
    public void SetTextureQuality(int qualityIndex)
    {
        QualitySettings.globalTextureMipmapLimit = qualityIndex;
    }

    public void SetShadowQuality(int qualityIndex)
    {
        switch (qualityIndex)
        {
            case 0: 
                QualitySettings.shadows = ShadowQuality.Disable;
                break; 
            case 1: 
                QualitySettings.shadows = ShadowQuality.HardOnly;
                break; 
            case 2: 
                QualitySettings.shadows = ShadowQuality.All;
                break; 
        }
    }

    public void SetVSync(int vsyncIndex)
    {
        QualitySettings.vSyncCount = vsyncIndex;
    }

    public void SetAntiAliasing(int aaIndex)
    {
        QualitySettings.antiAliasing = aaIndex;
    }

    public void SetFullscreen(int isFullscreen)
    {
        Screen.fullScreen = isFullscreen == 0 ? false : true;
    }
    

    #endregion
}

}