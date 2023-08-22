using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Module;

public class PlayerMover : EditorWindow
{
	static PlayerMover window;
	private Vector3 movePos;

	[MenuItem("MoonTool/PlayerMover")]
	public static void Open()
	{
		if (window == null)
		{
			window = CreateInstance<PlayerMover>();
		}

		window.Show();
	}

	void OnGUI()
	{
		movePos = EditorGUILayout.Vector3Field("MovePos:", movePos);
		string pasteValue = EditorGUILayout.TextField("Paste Vector3", EditorGUIUtility.systemCopyBuffer);
		if (GUILayout.Button("Paste"))
		{
			movePos = stringToVec(pasteValue);
		}

		if (GUILayout.Button("Move"))
		{
			PlayerObj.Player.transform.position = movePos;
		}
	}
	public Vector3 stringToVec(string s)
	{
		string resultStr = s.Replace("Vector3(", "");
		resultStr = resultStr.Replace(")", "");
		Debug.Log(resultStr);
		string[] temp = resultStr.Split(',');
		return new Vector3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));
	}
}