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
        float _masterVolume, _effVolue, _envirVolue, _bgmVolume; 
        _audioMixer.GetFloat("MasterVolume",out _masterVolume);
        masterAudioSlider.value = SetVolume(_masterVolume); 
        this.masterAudioSlider.UpdateUI(); 

        _audioMixer.GetFloat("EFFVolume",out  _effVolue);
        effAudioSlider.value = SetVolume(_effVolue);
        effSlider.UpdateUI(); 
        
        _audioMixer.GetFloat("EnvironmentVolume",out _envirVolue);
        environmentAudioSlider.value = SetVolume(_envirVolue); 
        envirnmentSlider.UpdateUI(); 

        _audioMixer.GetFloat("BGMVolume",out _bgmVolume);
        bgmAudioSlider.value = SetVolume(_bgmVolume); 
        bgmSlider.UpdateUI(); 
    }
    
    //===============/
    public float OnVolumeSliderChanged(int uiVolume)
    {
        // 0 ~ 100 범위의 값을 -40 ~ 0 범위로 매핑
        float volume = MapUIToVolumeRange(uiVolume);

        return volume; 
        // 여기에서 실제 볼륨 조절 로직 수행
        // 예: AudioMixer.SetFloat("VolumeParameter", volume);
    }

    // 0 ~ 100 범위의 값을 -40 ~ 0 범위로 매핑하는 함수
    float MapUIToVolumeRange(int uiVolume)
    {
        // 0 ~ 100 범위의 값을 0 ~ 1 범위로 매핑
        float normalizedUIVolume = uiVolume / 100f;

        // 0 ~ 1 범위의 값을 -40 ~ 0 범위로 매핑
        float volume = Mathf.Lerp(-40f, 0f, normalizedUIVolume);
        return volume;
    }
    
    //===============/
    // -40 ~ 0 범위의 볼륨 값을 0 ~ 1 범위로 매핑하는 함수
    float MapVolumeToUnityRange(float volume)
    {
        return Mathf.InverseLerp(-40f, 0f, volume);
    }
    // 0 ~ 1 범위의 값을 0 ~ 100 범위로 매핑하는 함수
    float MapValueToUIRange(float value)
    {
        return value * 100f;
    }
    // 볼륨 값을 -40 ~ 0 범위로 설정
    int SetVolume(float volume)
    {
        // 볼륨 값을 -40 ~ 0 범위에서 0 ~ 1 범위로 매핑
        float unityVolume = MapVolumeToUnityRange(volume);

        // 여기에서 실제 볼륨 조절 로직 수행
        // 예: AudioMixer.SetFloat("VolumeParameter", unityVolume);

        // UI에도 볼륨 값을 업데이트
        return (int)UpdateVolumeUI(unityVolume);
    }
    //===============/

    // UI에 볼륨 값을 업데이트
    float UpdateVolumeUI(float unityVolume)
    {
        // 0 ~ 1 범위의 값을 0 ~ 100 범위로 매핑하여 UI에 설정
        float uiVolume = MapValueToUIRange(unityVolume);
        return uiVolume;
    }

    //===============/

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
        _audioMixer.SetFloat("MasterVolume", OnVolumeSliderChanged(masterAudioSlider.Slider.value));
        _audioMixer.SetFloat("BGMVolume", OnVolumeSliderChanged(bgmSlider.Slider.value));
        _audioMixer.SetFloat("EFFVolume", OnVolumeSliderChanged(effSlider.Slider.value));
        _audioMixer.SetFloat("EnvironmentVolume", OnVolumeSliderChanged(envirnmentSlider.Slider.value));

        OptionManager.Instance.optionData.masterVolume = masterAudioSlider.Slider.value;
        OptionManager.Instance.optionData.bgmVolume = bgmSlider.Slider.value;
        OptionManager.Instance.optionData.effVolume = effSlider.Slider.value;
        OptionManager.Instance.optionData.envirVolume = envirnmentSlider.Slider.value;
    }
}
