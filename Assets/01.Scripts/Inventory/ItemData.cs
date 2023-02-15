using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		public string key; // 아이템 키
		public int count; // 갯수
		public int price; // 가격
		public string nameKey; //이름 키
		public string explanationKey; //설명 키
		public string spriteKey; //스프라이트 키
		public bool stackble; //겹치기 가능 여부
		public int stackMax = 64; // 최대 겹치기 량
		public ItemType itemType; // 아이템 타입
		public string prefebkey; //프리펩 키 (인게임에서 사용)
		public string animationLayer; // 애니메이션 레이어 (인게임에서 사용)

		//아이템 장비용
		[Header("장비 슬롯 용도")]
		public EquipmentType equipmentType;

		//아이템 슬롯에 표시용도
		[Header("아이템 슬롯에 표시용도")]
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
			_newItemData.prefebkey = _itemData.prefebkey;
			_newItemData.animationLayer = _itemData.animationLayer;

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
			_newItemData.prefebkey = _itemDataSO.prefebkey;
			_newItemData.animationLayer = _itemDataSO.animationLayer;

			return _newItemData;
		}
	}
}
