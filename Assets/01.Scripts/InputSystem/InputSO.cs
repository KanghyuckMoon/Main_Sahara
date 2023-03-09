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
		public StringInputData keyCodeDic = new StringInputData();
	}

	[System.Serializable]
	public class InputData
	{
		public KeyCode keyCode;
		public InputType inputType;
	}
}
