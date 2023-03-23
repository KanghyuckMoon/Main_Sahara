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
		public int price; // 가격
		public string nameKey; //이름 키
		public string explanationKey; //설명 키
		public string spriteKey; //스프라이트 키
		public bool stackble;
		public int stackMax = 64;
		public ItemType itemType;
		public ConsumptionType consumptionType; // 소모품 타입
		public string prefebkey;
		public string animationLayer;
		public string dropItemPrefebKey;

		//장신구 분류용
		public AccessoriesItemType accessoriesItemType;

		//아이템 장비용
		[Header("장비 슬롯 용도")]
		public EquipmentType equipmentType;

		//아이템 슬롯에 표시용도
		[Header("아이템 슬롯에 표시용도")]
		public bool isSlot = true;

		public void Copy(ItemDataSO _itemDataSO)
		{
			key = _itemDataSO.key;
			count = _itemDataSO.count;
			price = _itemDataSO.price;
			nameKey = _itemDataSO.nameKey;
			explanationKey = _itemDataSO.explanationKey;
			spriteKey = _itemDataSO.spriteKey;
			stackble = _itemDataSO.stackble;
			stackMax = _itemDataSO.stackMax;
			itemType = _itemDataSO.itemType;
			consumptionType = _itemDataSO.consumptionType;
			prefebkey = _itemDataSO.prefebkey;
			animationLayer = _itemDataSO.animationLayer;
			dropItemPrefebKey = _itemDataSO.dropItemPrefebKey;
			accessoriesItemType = _itemDataSO.accessoriesItemType;
			equipmentType = _itemDataSO.equipmentType;
			isSlot = _itemDataSO.isSlot;
		}
	}
}
