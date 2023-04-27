using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TODSO", menuName = "SO/TODSO")]
public class TODSO : ScriptableObject
{
	#region DEBUG
	//Debug
	[Tooltip("낮과 밤이 실시간으로 변경되는걸 허용합니다")]
	public bool isUpdateTime;
	[Tooltip("무조건 밤 판정이 뜨게 합니다")]
	public bool isOnlyNight;
	#endregion
	
	public bool isNight;
	
	public bool isUpdateOn;

	public void SetIsUpdateOn(bool isUpdateOn)
	{
		this.isUpdateOn = isUpdateOn;
	}
}
