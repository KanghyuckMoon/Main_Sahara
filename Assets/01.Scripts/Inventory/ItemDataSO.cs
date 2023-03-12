using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace Inventory
{
	[CreateAssetMenu(fileName = "ItemDataSO", menuName = "SO/ItemDataSO")]
	public class ItemDataSO : ScriptableObject
	{
		public string key;
		public int count;
		public int price; // ����
		public string nameKey; //�̸� Ű
		public string explanationKey; //���� Ű
		public string spriteKey; //��������Ʈ Ű
		public bool stackble;
		public int stackMax = 64;
		public ItemType itemType;
		public ConsumptionType consumptionType; // �Ҹ�ǰ Ÿ��
		public string prefebkey;
		public string animationLayer;
		public string dropItemPrefebKey;

		//��ű� �з���
		public AccessoriesItemType accessoriesItemType;

		//������ ����
		[Header("��� ���� �뵵")]
		public EquipmentType equipmentType;

		//������ ���Կ� ǥ�ÿ뵵
		[Header("������ ���Կ� ǥ�ÿ뵵")]
		public bool isSlot = true;
	}
}
