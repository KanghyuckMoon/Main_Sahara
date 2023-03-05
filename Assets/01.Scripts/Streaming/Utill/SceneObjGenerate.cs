using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Streaming
{
	public class SceneObjGenerate : MonoBehaviour
	{
#if UNITY_EDITOR
		[SerializeField]
		private GUISkin guiSkin;
		[SerializeField]
		private List<SceneAsset> sceneAssetList = new List<SceneAsset>();

		/// <summary>
		/// 씬에셋을 넣는대로 서브씬오브젝트를 생성
		/// </summary>
		[ContextMenu("Generate")]
		public void Generate()
		{
			foreach (var _sceneAsset in sceneAssetList)
			{
				GameObject _obj = new GameObject();
				_obj.transform.position = StreamingUtill.StringToVector3(_sceneAsset.name);
				SubSceneObj _loadSceneObj = _obj.AddComponent<SubSceneObj>();
				_loadSceneObj.SetSceneAsset(_sceneAsset);
				_loadSceneObj.GUISkin = guiSkin;
				_loadSceneObj.OnValidate();
			}
		}
#endif


	}

}