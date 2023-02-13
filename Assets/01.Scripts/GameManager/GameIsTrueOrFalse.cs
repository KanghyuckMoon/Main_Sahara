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

		private void Start()
		{
			GamePlayerManager.Instance.IsPlaying = isTrue;
		}
	}

}
