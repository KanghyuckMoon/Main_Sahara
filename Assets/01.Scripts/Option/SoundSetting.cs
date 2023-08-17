using System.Collections;
using System.Collections.Generic;
using Option;
using UnityEngine;
using UnityEngine.Audio;
//using UnityEngine.UI;
using UnityEngine.UIElements; 

public class SoundSetting : MonoBehaviour
{
    AudioMixer _audioMixer;
    Slider _bgmAudioSlider;
    Slider _effAudioSlider;

    /// <summary>
    /// 슬라이더 받아오오기 
    /// </summary>
    /// <param name="bgmAudioSlider"></param>
    /// <param name="effAudioSlider"></param>
    public void InitSlider(Slider bgmAudioSlider, Slider effAudioSlider)
    {
        this._bgmAudioSlider = bgmAudioSlider;
        this._effAudioSlider = effAudioSlider; 
    }
    public void SetBgmAudio(float bgmValue)
    {
        _audioMixer.SetFloat("BGMVolume",bgmValue);
    }

    public void SetEffAudio(float effValue)
    {
        _audioMixer.SetFloat("EFFVolume", effValue);
    }

    /// <summary>
    /// 소리 설정 적용
    /// </summary>
    public void ApplySettingSound()
    {
        _audioMixer.SetFloat("BGMVolume", _bgmAudioSlider.value);
        _audioMixer.SetFloat("EFFVolume", _effAudioSlider.value);

        OptionManager.Instance.optionData.bgmVolume = _bgmAudioSlider.value;
        OptionManager.Instance.optionData.effVolume = _effAudioSlider.value;
    }
}
