using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace Inventory
{
	public enum ItemType
	{
		Weapon,
		Consumption,
		Skill,
		Equipment,
		Accessories,
		Material,
		Valuable,
		None,
	}
	public enum ConsumptionType
	{
		None,
		Posion = 1,
		Arrow = 2,
		Others,
	}

	public enum EquipmentType
	{
		None,
		Head,
		Armor,
		Pants,
		Shoes,
	}

	[System.Serializable]
	public class ItemData
	{
		public bool IsStackble
		{
			get
			{
				if (stackble && count < stackMax)
				{
					return true;
				}
				return false;
			}
		}

		public string key; // ������ Ű
		public int count; // ����
		public int price; // ����
		public string nameKey; //�̸� Ű
		public string explanationKey; //���� Ű
		public string spriteKey; //��������Ʈ Ű
		public bool stackble; //��ġ�� ���� ����
		public int stackMax = 64; // �ִ� ��ġ�� ��
		public ItemType itemType; // ������ Ÿ��
		public ConsumptionType consumptionType; // �Ҹ�ǰ Ÿ��
		public string prefebkey; //������ Ű (�ΰ��ӿ��� ���)
		public string animationLayer; // �ִϸ��̼� ���̾� (�ΰ��ӿ��� ���)
		public string dropItemPrefebKey;

		//��ű� �з���
		public AccessoriesItemType accessoriesItemType;

		//������ ����
		[Header("��� ���� �뵵")]
		public EquipmentType equipmentType;

		//������ ���Կ� ǥ�ÿ뵵
		[Header("������ ���Կ� ǥ�ÿ뵵")]
		public bool isSlot = true;

		public static ItemData CopyItemData(ItemData _itemData)
		{
			ItemData _newItemData = new ItemData();
			_newItemData.key = _itemData.key;
			_newItemData.count = _itemData.count;
			_newItemData.nameKey = _itemData.nameKey;
			_newItemData.explanationKey = _itemData.explanationKey;
			_newItemData.spriteKey = _itemData.spriteKey;
			_newItemData.stackble = _itemData.stackble;
			_newItemData.stackMax = _itemData.stackMax;
			_newItemData.itemType = _itemData.itemType;
			_newItemData.consumptionType = _itemData.consumptionType;
			_newItemData.prefebkey = _itemData.prefebkey;
			_newItemData.animationLayer = _itemData.animationLayer;
			_newItemData.dropItemPrefebKey = _itemData.dropItemPrefebKey;

			return _newItemData;
		}

		public static ItemData CopyItemDataSO(ItemDataSO _itemDataSO)
		{
			ItemData _newItemData = new ItemData();
			_newItemData.key = _itemDataSO.key;
			_newItemData.count = _itemDataSO.count;
			_newItemData.nameKey = _itemDataSO.nameKey;
			_newItemData.explanationKey = _itemDataSO.explanationKey;
			_newItemData.spriteKey = _itemDataSO.spriteKey;
			_newItemData.stackble = _itemDataSO.stackble;
			_newItemData.stackMax = _itemDataSO.stackMax;
			_newItemData.itemType = _itemDataSO.itemType;
			_newItemData.consumptionType = _itemDataSO.consumptionType;
			_newItemData.prefebkey = _itemDataSO.prefebkey;
			_newItemData.animationLayer = _itemDataSO.animationLayer;
			_newItemData.dropItemPrefebKey = _itemDataSO.dropItemPrefebKey;

			return _newItemData;
		}
	}
}
