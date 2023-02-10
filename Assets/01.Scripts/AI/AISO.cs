using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AISO", menuName = "SO/AISO")]
public class AISO : ScriptableObject
{
	public string aiAddress = "TestEnemy";
	public float ferDistance = 10f;

	//의심게이지
	public float maxSuspicionGauge = 100f;

	//시야각
	[Range(0f, 360f)]
	public float ViewAngle = 0f;
	public float ViewRadius = 1f;

	//공격각
	[Range(0f, 360f)]
	public float AttackAngle = 0f;
	public float AttackRadius = 1f;

	//의심각
	[Range(0f, 360f)]
	public float SuspicionAngle = 0f;
	public float SuspicionRadius = 1f;

	//레이캐스트
	public LayerMask TargetMask;
	public LayerMask ObstacleMask;

	//기즈모 색
	public Color gizmoColor_discorver;
	public Color gizmoColor_fer;
	public Color gizmoColor_attack;
}
