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
		Marker,
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

		public string key; // 아이템 키
		public int count; // 갯수
		public int price; // 가격
		public string nameKey; //이름 키
		public string explanationKey; //설명 키
		public string spriteKey; //스프라이트 키
		public string iconKey; //스프라이트 키 인벤토리 키
		public bool stackble; //겹치기 가능 여부
		public int stackMax = 64; // 최대 겹치기 량
		public ItemType itemType; // 아이템 타입
		public ConsumptionType consumptionType; // 소모품 타입
		public string prefebkey; //프리펩 키 (인게임에서 사용)
		public string modelkey; //모델 프리펩 키 인베놑리에서 사용
		public string animationLayer; // 애니메이션 레이어 (인게임에서 사용)
		public string dropItemPrefebKey;

		//장신구 분류용
		public AccessoriesItemType accessoriesItemType;

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
			_newItemData.price = _itemData.price;
			_newItemData.nameKey = _itemData.nameKey;
			_newItemData.explanationKey = _itemData.explanationKey;
			_newItemData.spriteKey = _itemData.spriteKey;
			_newItemData.iconKey = _itemData.iconKey;
			_newItemData.stackble = _itemData.stackble;
			_newItemData.stackMax = _itemData.stackMax;
			_newItemData.itemType = _itemData.itemType;
			_newItemData.consumptionType = _itemData.consumptionType;
			_newItemData.prefebkey = _itemData.prefebkey;
			_newItemData.modelkey = _itemData.modelkey;
			_newItemData.animationLayer = _itemData.animationLayer;
			_newItemData.dropItemPrefebKey = _itemData.dropItemPrefebKey;
			_newItemData.accessoriesItemType = _itemData.accessoriesItemType;
			_newItemData.equipmentType = _itemData.equipmentType;
			_newItemData.isSlot = _itemData.isSlot;

			return _newItemData;
		}

		public static ItemData CopyItemDataSO(ItemDataSO _itemDataSO)
		{
			ItemData _newItemData = new ItemData();
			_newItemData.key = _itemDataSO.key;
			_newItemData.count = _itemDataSO.count;
			_newItemData.price = _itemDataSO.price;
			_newItemData.nameKey = _itemDataSO.nameKey;
			_newItemData.explanationKey = _itemDataSO.explanationKey;
			_newItemData.spriteKey = _itemDataSO.spriteKey;
			_newItemData.iconKey = _itemDataSO.iconKey;
			_newItemData.stackble = _itemDataSO.stackble;
			_newItemData.stackMax = _itemDataSO.stackMax;
			_newItemData.itemType = _itemDataSO.itemType;
			_newItemData.consumptionType = _itemDataSO.consumptionType;
			_newItemData.prefebkey = _itemDataSO.prefebkey;
			_newItemData.modelkey = _itemDataSO.modelkey;
			_newItemData.animationLayer = _itemDataSO.animationLayer;
			_newItemData.dropItemPrefebKey = _itemDataSO.dropItemPrefebKey;
			_newItemData.accessoriesItemType = _itemDataSO.accessoriesItemType;
			_newItemData.equipmentType = _itemDataSO.equipmentType;
			_newItemData.isSlot = _itemDataSO.isSlot;

			return _newItemData;
		}
	}
}
