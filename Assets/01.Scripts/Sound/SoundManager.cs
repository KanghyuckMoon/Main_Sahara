using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utill.Pattern;
using Utill.Addressable;

namespace Sound
{
	/// <summary>
	/// 효과음 및 배경음악을 재생하는 매니저
	/// </summary>
	public class SoundManager : MonoSingleton<SoundManager>
	{
		private AudioMixer _audioMixer;
		private AudioMixerGroup _bgmAudioGroup;
		private AudioMixerGroup _environmentAudioGroup;
		private AudioMixerGroup _effAudioGroup;
		private AudioSource _bgmAudioSourceTrack1 = null;
		private AudioSource _bgmAudioSourceTrack2 = null;
		private AudioSource _environmentAudioSourceTrack1 = null;
		private AudioSource _environmentAudioSourceTrack2 = null;
		private AudioSource _effAudioSource = null;
		private Dictionary<AudioBGMType, AudioClip> _bgmAudioClips = new Dictionary<AudioBGMType, AudioClip>();
		private Dictionary<AudioEnvironmentType, AudioClip> _environmentAudioClips = new Dictionary<AudioEnvironmentType, AudioClip>();
		private AudioBGMType _currentBGMType = AudioBGMType.Count;
		private AudioEnvironmentType _currentEnvironmentType = AudioEnvironmentType.Count;
		private EFFSO _effSO;

		private Coroutine fadeCoroutine;
		private Coroutine fadeCoroutine_Environment;
		private bool isPlayingTrack1 = false;
		private bool isPlayingTrack1_Environment = false;
		private bool _isInit = false; //사운드 매니저 초기화 여부


		/// <summary>
		/// 효과음 출력 함수
		/// </summary>
		/// <param name="audioName"></param>
		public void PlayEFF(string audioName)
		{
			if (!_isInit)
			{
				Init();
			}

			//EffSO에서 효과음을 가져온다
			AudioEffData clip = _effSO.GetEFFClip(audioName);
			_effAudioSource.volume = clip.volume;
			_effAudioSource.PlayOneShot(clip.audioClip);
		}

		/// <summary>
		/// 배경음악 재생
		/// </summary>
		/// <param name="audioBGMType"></param>
		public void PlayBGM(AudioBGMType audioBGMType)
		{
			if (!_isInit)
			{
				Init();
			}

			if (_currentBGMType == audioBGMType)
			{
				return;
			}

			_currentBGMType = audioBGMType;

			SwapTrack(_bgmAudioClips[audioBGMType]);
		}

		/// <summary>
		/// 배경음악 재생
		/// </summary>
		/// <param name="audioBGMType"></param>
		public void PlayEnvironment(AudioEnvironmentType _audioEnvironmentType)
		{
			if (!_isInit)
			{
				Init();
			}

			if (_currentEnvironmentType == _audioEnvironmentType)
			{
				return;
			}

			_currentEnvironmentType = _audioEnvironmentType;

			SwapTrack_Environment(_audioEnvironmentType == AudioEnvironmentType.None ? null : _environmentAudioClips[_audioEnvironmentType]);
		}

		private void Start()
		{
			if (!_isInit)
			{
				if (Instance == this)
				{
					Init();
				}
				else
				{
					Instance.Init();
				}
			}
		}

		/// <summary>
		/// 초기화
		/// </summary>
		private void Init()
		{
			if (_isInit)
			{
				return;
			}
			_isInit = true;

			_effSO = AddressablesManager.Instance.GetResource<EFFSO>("EFFSO");
			_audioMixer = AddressablesManager.Instance.GetResource<AudioMixer>("MainMixer");

			var groups = _audioMixer.FindMatchingGroups(string.Empty);
			_bgmAudioGroup = groups[1];
			_effAudioGroup = groups[2];
			_environmentAudioGroup = groups[3];

			GetBGMAudioSource();
			GetEnvironmentAudioSource();
			GenerateEFFAudioSource();
		}

		/// <summary>
		/// 배경 음악 가져오기
		/// </summary>
		private void GetBGMAudioSource()
		{

			//새로운 오디오 소스 만들기
			GameObject obj = new GameObject("BGM");
			obj.transform.SetParent(transform);
			AudioSource audioSource = obj.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = _bgmAudioGroup;
			audioSource.clip = null;
			audioSource.playOnAwake = true;
			audioSource.loop = true;

			_bgmAudioSourceTrack1 = audioSource;


			AudioSource audioSource2 = obj.AddComponent<AudioSource>();
			audioSource2.outputAudioMixerGroup = _bgmAudioGroup;
			audioSource2.clip = null;
			audioSource2.playOnAwake = true;
			audioSource2.loop = true;

			_bgmAudioSourceTrack2 = audioSource2;

			int count = (int)AudioBGMType.Count;

			//브금들 미리 세팅
			for (int i = 1; i < count; ++i)
			{
				string key = System.Enum.GetName(typeof(AudioBGMType), i);
				AudioClip audioClip = AddressablesManager.Instance.GetResource<AudioClip>(key);
				_bgmAudioClips.Add((AudioBGMType)i, audioClip);
			}
		}


		/// <summary>
		/// 자연음 가져오기
		/// </summary>
		private void GetEnvironmentAudioSource()
		{

			//새로운 오디오 소스 만들기
			GameObject obj = new GameObject("Environment");
			obj.transform.SetParent(transform);
			AudioSource audioSource = obj.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = _environmentAudioGroup;
			audioSource.clip = null;
			audioSource.playOnAwake = true;
			audioSource.loop = true;

			_environmentAudioSourceTrack1 = audioSource;


			AudioSource audioSource2 = obj.AddComponent<AudioSource>();
			audioSource2.outputAudioMixerGroup = _environmentAudioGroup;
			audioSource2.clip = null;
			audioSource2.playOnAwake = true;
			audioSource2.loop = true;

			_environmentAudioSourceTrack2 = audioSource2;

			int count = (int)AudioEnvironmentType.Count;

			//브금들 미리 세팅
			for (int i = 1; i < count; ++i)
			{
				string key = System.Enum.GetName(typeof(AudioEnvironmentType), i);
				AudioClip audioClip = AddressablesManager.Instance.GetResource<AudioClip>(key);
				_environmentAudioClips.Add((AudioEnvironmentType)i, audioClip);
			}
		}

		/// <summary>
		/// 이펙트 오디오 소스들 생성
		/// </summary>
		private void GenerateEFFAudioSource()
		{
			//새로운 오디오 소스 만들기
			GameObject obj = new GameObject("EFF");
			obj.transform.SetParent(transform);
			AudioSource audioSource = obj.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = _effAudioGroup;
			audioSource.clip = null;
			audioSource.playOnAwake = true;
			audioSource.loop = true;

			_effAudioSource = audioSource;
		}

		private void SwapTrack(AudioClip _clip)
		{
			if (fadeCoroutine is not null)
			{
				StopCoroutine(fadeCoroutine);
				fadeCoroutine = null;
			}
			fadeCoroutine = StartCoroutine(FadeTrack(_clip));

			isPlayingTrack1 = !isPlayingTrack1;
		}

		private void SwapTrack_Environment(AudioClip _clip)
		{
			if (fadeCoroutine_Environment is not null)
			{
				StopCoroutine(fadeCoroutine_Environment);
				fadeCoroutine_Environment = null;
			}
			fadeCoroutine_Environment = StartCoroutine(FadeTrack_Environment(_clip));

			isPlayingTrack1_Environment = !isPlayingTrack1_Environment;
		}

		private IEnumerator FadeTrack(AudioClip _clip)
		{
			float timeToFade = 1.25f;
			float timeElapsed = 0;

			float _beforeTrackVolume = 0f;
			float _afterTrackVolume = 0f;

			if (isPlayingTrack1)
			{
				_bgmAudioSourceTrack2.clip = _clip;
				_bgmAudioSourceTrack2.Play();

				while (_beforeTrackVolume < 1)
				{
					_beforeTrackVolume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
					_afterTrackVolume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
					_bgmAudioSourceTrack2.volume = _beforeTrackVolume;
					_bgmAudioSourceTrack1.volume = _afterTrackVolume;
					timeElapsed += Time.deltaTime;
					yield return null;
				}

				_bgmAudioSourceTrack1.Stop();
			}
			else
			{
				_bgmAudioSourceTrack1.clip = _clip;
				_bgmAudioSourceTrack1.Play();

				while (_beforeTrackVolume < 1)
				{
					_beforeTrackVolume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
					_afterTrackVolume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
					_bgmAudioSourceTrack1.volume = _beforeTrackVolume;
					_bgmAudioSourceTrack2.volume = _afterTrackVolume;
					timeElapsed += Time.deltaTime;
					yield return null;
				}

				_bgmAudioSourceTrack2.Stop();
			}
		}
		private IEnumerator FadeTrack_Environment(AudioClip _clip)
		{
			float timeToFade = 1.25f;
			float timeElapsed = 0;

			float _beforeTrackVolume = 0f;
			float _afterTrackVolume = 0f;

			if (isPlayingTrack1)
			{
				_environmentAudioSourceTrack2.clip = _clip;
				_environmentAudioSourceTrack2.Play();

				while (_beforeTrackVolume < 1)
				{
					_beforeTrackVolume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
					_afterTrackVolume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
					_environmentAudioSourceTrack2.volume = _beforeTrackVolume;
					_environmentAudioSourceTrack1.volume = _afterTrackVolume;
					timeElapsed += Time.deltaTime;
					yield return null;
				}

				_environmentAudioSourceTrack1.Stop();
			}
			else
			{
				_environmentAudioSourceTrack1.clip = _clip;
				_environmentAudioSourceTrack1.Play();

				while (_beforeTrackVolume < 1)
				{
					_beforeTrackVolume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
					_afterTrackVolume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
					_environmentAudioSourceTrack1.volume = _beforeTrackVolume;
					_environmentAudioSourceTrack2.volume = _afterTrackVolume;
					timeElapsed += Time.deltaTime;
					yield return null;
				}

				_environmentAudioSourceTrack2.Stop();
			}
		}

	}

}