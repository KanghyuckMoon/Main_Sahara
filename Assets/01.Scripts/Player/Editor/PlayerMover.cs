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
		if (GUILayout.Button("Move"))
		{
			PlayerObj.Player.transform.position = movePos;
		}
	}
}