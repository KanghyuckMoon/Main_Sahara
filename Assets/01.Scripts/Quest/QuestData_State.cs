using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
	public enum QuestState
	{
		Disable, //��Ȱ��ȭ
		Discoverable, //�߰� ����
		Active, //Ȱ��ȭ
		Achievable, //�޼�����
		Clear, //Ŭ����
	}

	public enum QuestConditionType
	{
		Position, //��ġ
		TargetMonster, //Ÿ�� ����
		TargetObject, //Ÿ�� ������Ʈ
		Inventory, //�κ��丮
		Time, //�ð�
		/// <summary>
		/// ���α׷��Ӱ� ���۾��ϴ� ����
		/// </summary>
		Handwork, //���۾�
	}

	public enum QuestCategory
	{
		Main,
		Sub
	}

	public partial class QuestData
	{
	}
}


