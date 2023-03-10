using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.SeralizableDictionary;

namespace InputSystem
{
	public enum InputType
	{
		Down,
		Ing,
		Up,
		None,
	}

	[System.Serializable]
	public class StringInputData : SerializableDictionary<string, InputData> { };

	[CreateAssetMenu(fileName = "InputSO", menuName = "SO/InputSO")]
	public class InputSO : ScriptableObject
	{
		public List<InputData> inputDataList = new List<InputData>();
		public StringInputData keyCodeDic = new StringInputData();

		[ContextMenu("InputDataListToDic")]
		public void InputDataListToDic()
		{
			keyCodeDic.Clear();
			foreach (var _obj in inputDataList)
			{
				keyCodeDic.Add(_obj.key, _obj);
			}
#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty(this);
#endif
		}
	}

	[System.Serializable]
	public class InputData
	{
		public string key;
		public KeyCode keyCode;
		public InputType inputType;
	}
}
