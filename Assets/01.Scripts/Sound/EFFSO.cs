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
	/// ȿ���� SO
	/// </summary>
	[CreateAssetMenu(fileName = "EFFSO", menuName = "SO/EFFSO")]
	public class EFFSO : ScriptableObject
	{
		public AudioClip[] _effaudioClips;
		public StringAudioEffData _audioDictionary = new StringAudioEffData();

	/// <summary>
	/// ȿ���� Ŭ���� �����´�
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

			//���� ������ �Ҹ��� ������ �ʱ�ȭ�� �ٽ� �ѹ� �Ѵ�.
			SetEFFClips();

			if (_audioDictionary.TryGetValue(name, out clip))
			{
				return clip;
			}

			//ȿ���� ����
			Debug.LogError("���� �̸��Դϴ�");

			return null;
		}

		/// <summary>
		/// ���� ȿ���� Ŭ���� ��ųʸ��� ������
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