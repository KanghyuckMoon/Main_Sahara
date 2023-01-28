using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
	public class GetPoolCount : MonoBehaviour
	{
		GUIStyle GUIStyle = new GUIStyle();
		[SerializeField]
		private float width = 100f;
		[SerializeField]
		private float height = 100f;
		[SerializeField]
		private float height2 = 200f;

		private void Start()
		{
			GUIStyle.fontSize = 100;
		}
		private void OnGUI()
		{
			if (Application.isPlaying)
			{
				Rect _position = new Rect(width, height, Screen.width, Screen.height);
				Rect _position2 = new Rect(width, height2, Screen.width, Screen.height);
				GUI.Label(_position, $"Object : {ObjectPoolManager.Instance.GetAllCount()}", GUIStyle);
				GUI.Label(_position2, $"Class : {ClassPoolManager.Instance.GetAllCount()}", GUIStyle);
			}
		}
	}

}