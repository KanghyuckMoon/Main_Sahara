using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TODSO", menuName = "SO/TODSO")]
public class TODSO : ScriptableObject
{
	[Tooltip("���� ���� �ǽð����� ����Ǵ°� ����մϴ�")]
	public bool isUpdateTime;

	[Tooltip("������ �� ������ �߰� �մϴ�")]
	public bool isOnlyNight;

}
