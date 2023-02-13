using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ComboSO : ScriptableObject
{
	public ComboInputData[] comboInputDatas;
}

[System.Serializable]
public class ComboInputData
{
	public KeyCode[] keyCode;
	public bool isHold;
	public float holdTime;
	public float delay;
}