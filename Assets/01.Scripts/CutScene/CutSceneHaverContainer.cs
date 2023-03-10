using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module.Talk;

namespace CutScene
{
	public class CutSceneHaverContainer : MonoBehaviour, ITalkWithCutScene
	{
		[SerializeField]
		private List<CutSceneHaver> cutSceneHaverList = new List<CutSceneHaver>();
		private Dictionary<string, CutSceneHaver> cutSceneHaverDic = new Dictionary<string, CutSceneHaver>();

		private void Start()
		{
			foreach (var _obj in cutSceneHaverList)
			{
				cutSceneHaverDic.Add(_obj.Key, _obj);
			}
		}

		public void PlayCutScene(string _key)
		{
			cutSceneHaverDic[_key].PlayCutScene();
		}
	}
}
