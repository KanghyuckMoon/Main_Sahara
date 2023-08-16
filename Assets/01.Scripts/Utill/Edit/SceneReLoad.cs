//#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor;
using System.Diagnostics;

public static class SceneReLoad
{
	[MenuItem("MoonTool/ReLoadCurrentScene")]
	public static void ReLoadCurrentScene()
	{
		string currentSceneName = EditorSceneManager.GetActiveScene().path;
		//UnityEngine.Debug.Log("Current active scene in editor: " + currentSceneName);
		EditorSceneManager.OpenScene(currentSceneName);
	}
}
//#endif