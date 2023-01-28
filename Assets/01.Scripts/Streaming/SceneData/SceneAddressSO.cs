using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Streaming
{
	[CreateAssetMenu(fileName = "SceneAddressSO", menuName = "SO/SceneAddressSO")]
	public class SceneAddressSO : ScriptableObject
	{
		public List<string> sceneAddressList = new List<string>();

#if UNITY_EDITOR
		[SerializeField]
		private List<SceneAsset> _sceneAssetList = new List<SceneAsset>();

		/// <summary>
		/// �������� �ִ´�� �����������Ʈ�� ����
		/// </summary>
		[ContextMenu("SceneAssetListToAddressList")]
		public void SceneAssetListToAddressList()
		{
			foreach (var _sceneAsset in _sceneAssetList)
			{
				sceneAddressList.Add(_sceneAsset.name);
			}
		}
#endif

	}

}