using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AISO", menuName = "SO/AISO")]
public class AISO : ScriptableObject
{
	public string aiAddress = "TestEnemy";
	public float ferDistance = 10f;
	public bool isFirstAttack = true;

	//�ǽɰ�����
	public float maxSuspicionGauge = 100f;

	//�þ߰�
	[Range(0f, 360f)]
	public float ViewAngle = 0f;
	public float ViewRadius = 1f;

	//���ݰ�
	[Range(0f, 360f)]
	public float AttackAngle = 0f;
	public float AttackRadius = 1f;

	//�ǽɰ�
	[Range(0f, 360f)]
	public float SuspicionAngle = 0f;
	public float SuspicionRadius = 1f;
	
	//Around
	public float AroundRadius = 1f;

	//����ĳ��Ʈ
	public LayerMask TargetMask;
	public LayerMask ObstacleMask;
	public LayerMask GroundMask;

	//����� ��
	public Color gizmoColor_discorver;
	public Color gizmoColor_fer;
	public Color gizmoColor_attack;
}
