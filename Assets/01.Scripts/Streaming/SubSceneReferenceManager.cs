using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace Streaming
{
	public class SubSceneReferenceManager : MonoSingleton<SubSceneReferenceManager>
	{
		public SubSceneObj[] SubSceneArray
		{
			get
			{
				return subSceneArray;
			}
			set
			{
				subSceneArray = value;
			}
		}

		[SerializeField]
		private SubSceneObj[] subSceneArray;

		[SerializeField]
		private bool isUseStreamingScene = false;


		private void Start()
		{
			if (subSceneArray == null )
			{
				subSceneArray = FindObjectsOfType<SubSceneObj>();
			}
			else if (subSceneArray.Length > 0 && subSceneArray[0] == null)
			{
				subSceneArray = FindObjectsOfType<SubSceneObj>();
			}
		}
	}
}
