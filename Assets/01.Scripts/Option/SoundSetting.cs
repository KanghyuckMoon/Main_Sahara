using System;
using System.Collections;
using System.Collections.Generic;
using Option;
using UI.Base;
using UnityEngine;
using UnityEngine.Audio;
//using UnityEngine.UI;
using UnityEngine.UIElements;
using Utill.Addressable;

/*
 *
 * UI 변경 -> 받아와서 데이터 변경
 * 이때 UI 를 미리 가져와야 한다.
 * 동적 생성시 UI 어케 가져와 ->
 * 생성 UI를 저장해두고 
 * OptionType을 Key 로 해서 가져오기  
 * ----
 * 원래 하려던 방식 :
 * 생성한 UI에 함수를 달아준다. <- 이건데
 * UI에서 값을 변경하여 그걸 데이터로 가느게
 * 
 * Sound, Graphic   
 */
public class SoundSetting : MonoBehaviour
{
    AudioMixer _audioMixer;

    ToolkitSlider masterAudioSlider; 
    private ToolkitSlider bgmSlider;
    private ToolkitSlider effSlider; 
    private ToolkitSlider envirnmentSlider; 

    
   // Slider 가져오고 라벨도 가져와 
   
    
    private void Awake()
    {
        _audioMixer = AddressablesManager.Instance.GetResource<AudioMixer>("MainMixer");
    }

    /// <summary>
    /// 슬라이더 받아오오기 
    /// </summary>
    /// <param name="bgmAudioSlider"></param>
    /// <param name="effAudioSlider"></param>
    public void InitSlider( SliderInt masterAudioSlider,SliderInt bgmAudioSlider, SliderInt effAudioSlider, SliderInt environmentAudioSlider,
                                            Label masterLabel,Label bgmLabel, Label effLabel, Label envirLabel)
    {
        // 목표가 쨋든 가져와서 등록하는거 
        // OptionType으로 구분해서 OptionBtnEntryPr 가져오고 
        // 거기서 Slider, Text 가져와
        this.masterAudioSlider = new ToolkitSlider(masterAudioSlider, masterLabel);
        this.bgmSlider = new ToolkitSlider(bgmAudioSlider, bgmLabel);
        this.effSlider = new ToolkitSlider(effAudioSlider, effLabel);
        this.envirnmentSlider = new ToolkitSlider(environmentAudioSlider, envirLabel);

        // 처음 값 설정 
        _audioMixer.GetFloat("MasterVolume",out float _masterValue);
        masterAudioSlider.value = (int)_masterValue; 
        _audioMixer.GetFloat("EFFVolume",out float _effValue);
        masterAudioSlider.value = (int)_effValue; 
        _audioMixer.GetFloat("EnvironmentVolume",out float _environmentValue);
        masterAudioSlider.value = (int)_environmentValue; 
        _audioMixer.GetFloat("BGMVolume",out float _bgmValue);
        masterAudioSlider.value = (int)_bgmValue; 
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
    /// 환경음 설정
    /// </summary>
    /// <param name="effValue"></param>
    public void SetEnvironmentAudio(float effValue)
    {
        _audioMixer.SetFloat("EnvironmentVolume", effValue);
    }

    /// <summary>
    /// 소리 설정 적용
    /// </summary>
    public void ApplySettingSound()
    {
        _audioMixer.SetFloat("MasterVolume", masterAudioSlider.Slider.value);
        _audioMixer.SetFloat("BGMVolume", bgmSlider.Slider.value);
        _audioMixer.SetFloat("EFFVolume", effSlider.Slider.value);
        _audioMixer.SetFloat("EnvironmentVolume", envirnmentSlider.Slider.value);

        OptionManager.Instance.optionData.masterVolume = masterAudioSlider.Slider.value;
        OptionManager.Instance.optionData.bgmVolume = bgmSlider.Slider.value;
        OptionManager.Instance.optionData.effVolume = effSlider.Slider.value;
        OptionManager.Instance.optionData.envirVolume = envirnmentSlider.Slider.value;
    }
}
