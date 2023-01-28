using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Streaming;
public class GetCount : MonoBehaviour
{
	GUIStyle GUIStyle = new GUIStyle();
	private void Start()
	{
		GUIStyle.fontSize = 100;
	}
	private void OnGUI()
	{
		if (Application.isPlaying)
		{
			GUILayout.Label($"Object : {SceneDataManager.Instance.AllGetObjectCount()}", GUIStyle);
			GUILayout.Label($"ObjectData : {SceneDataManager.Instance.AllGetObjectDataCount()}", GUIStyle);
		}
	}
}
