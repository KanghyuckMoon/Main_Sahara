#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneObjectSortEditor : EditorWindow
{
	static SceneObjectSortEditor exampleWindow;

	[MenuItem("MoonTool/SceneObjectSortEditor")]
	static void Open()
	{
		if (exampleWindow == null)
		{
			exampleWindow = CreateInstance<SceneObjectSortEditor>();
		}

		exampleWindow.Show();
	}

	private void OnGUI()
	{
		GUILayout.Label("Select objects and click the button to divide their positions by 100.", EditorStyles.boldLabel);

		if (GUILayout.Button("Divide Positions by 100"))
		{
			DividePositionsBy100();
		}
	}

	private void DividePositionsBy100()
	{
		Transform[] selectedTransforms = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.ExcludePrefab);

		foreach (Transform selectedTransform in selectedTransforms)
		{
			//Undo.RecordObject(selectedTransform, "Divide Position by 100");
			int x = Mathf.RoundToInt(selectedTransform.position.x / 100);
			int y = Mathf.RoundToInt(selectedTransform.position.y / 100);
			int z = Mathf.RoundToInt(selectedTransform.position.z / 100);
			if(y >= 41)
			{
				y = 40;
			}
			string sceneName = $"Map({x},{y},{z})";
			try
			{
				EditorSceneManager.MoveGameObjectToScene(selectedTransform.gameObject, EditorSceneManager.GetSceneByName(sceneName));
			}
			catch
			{
				Debug.Log($"{sceneName}");
			}
			EditorUtility.SetDirty(selectedTransform);
		}
	}
}
#endif