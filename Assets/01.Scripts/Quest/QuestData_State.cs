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
		Clear, //Ŭ����
	}

	public enum QuestConditionType
	{
		Position, //��ġ
		TargetMonster, //Ÿ�� ����
		MonsterType, //���� ����
		DebugData, //����� ������
		TargetObject, //Ÿ�� ������Ʈ
		MiniGame, //�̴� ����
		Stat, //����
		Inventory, //�κ��丮
		Mission, //�̼�
		TargetNPC, //Ÿ�� NPC
		TargetInteractionItem, //Ÿ�� ��ȣ�ۿ� ������
	}

	public partial class QuestData
	{
	}
}


