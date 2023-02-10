using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utill.SeralizableDictionary;

namespace Sound
{
	[System.Serializable]
	public class StringAudioEffData : SerializableDictionary<string, AudioEffData> { }

	/// <summary>
	/// 효과음 SO
	/// </summary>
	[CreateAssetMenu(fileName = "EFFSO", menuName = "SO/EFFSO")]
	public class EFFSO : ScriptableObject
	{
		public AudioClip[] _effaudioClips;
		public StringAudioEffData _audioDictionary = new StringAudioEffData();

	/// <summary>
	/// 효과음 클립을 가져온다
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public AudioEffData GetEFFClip(string name)
		{
			AudioEffData clip = null;

			if (_audioDictionary.TryGetValue(name, out clip))
			{
				return clip;
			}

			//만약 지정한 소리가 없으면 초기화를 다시 한번 한다.
			SetEFFClips();

			if (_audioDictionary.TryGetValue(name, out clip))
			{
				return clip;
			}

			//효과음 없음
			Debug.LogError("없는 이름입니다");

			return null;
		}

		/// <summary>
		/// 현재 효과음 클립을 딕셔너리로 저장함
		/// </summary>
		[ContextMenu("SetEFFClips")]
		private void SetEFFClips()
		{
			_audioDictionary.Clear();
			foreach (var clip in _effaudioClips)
			{
				_audioDictionary.Add(clip.name, new AudioEffData(clip));
			}
		}

	}

	[System.Serializable]
	public class AudioEffData
	{
		public AudioEffData(AudioClip _clip)
		{
			audioClip = _clip;
		}

		public AudioClip audioClip;
		public float volume = 1f;
	}
}