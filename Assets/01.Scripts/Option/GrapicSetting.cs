using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Option
{
    
public class GrapicSetting : MonoBehaviour
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

    private bool _isFoolScreen = true;
    private int _width = 1920;
    private int _height = 1080;

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
    }
}

}