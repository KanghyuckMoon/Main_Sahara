using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using UnityEngine.SceneManagement;

namespace Streaming
{
	public class SubSceneReference : MonoBehaviour
	{
#if UNITY_EDITOR
		List<UnityEditor.SceneAsset> sceneAssetList;

		[ContextMenu("AssetToNameList")]
		public void AssetToNameList()
		{
			foreach (var _asset in sceneAssetList)
			{
				sceneNameList.Add(_asset.name);
			}
		}

#endif

		public List<SubSceneObj> SubSceneArray
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
		private List<string> sceneNameList;

		[SerializeField]
		private List<SubSceneObj> subSceneArray;

		[SerializeField]
		private bool isUseStreamingScene = false;


		public void Init()
		{
			subSceneArray.Clear();
			foreach (string _name in sceneNameList)
			{
				GameObject obj = new GameObject();
				SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("InGame"));
				SubSceneObj subSceneObj = obj.AddComponent<SubSceneObj>();
				//LODMaker lodMaker = obj.AddComponent<LODMaker>();
				subSceneObj.SetSceneName(_name);
				obj.name = _name;
				subSceneArray.Add(subSceneObj);
			}
		}
	}
}
