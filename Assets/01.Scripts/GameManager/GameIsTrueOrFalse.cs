using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace GameManager
{
	public class GameIsTrueOrFalse : MonoBehaviour
	{
		[SerializeField]
		private bool isTrue;

		private void Awake()
		{
			GamePlayerManager.Instance.IsPlaying = isTrue;
		}
	}

}
