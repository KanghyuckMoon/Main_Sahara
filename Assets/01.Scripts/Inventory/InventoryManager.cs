using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using Utill.Addressable;
using System.Linq;
using Module;
using GameManager;
using Pool;

namespace Inventory
{
	public delegate void InventoryEventTransmit(string _sender, string _recipient, object _obj);
	public class InventoryManager : MonoSingleton<InventoryManager>
	{
		public Transform Player
		{
			get
			{
				if (player is null)
				{
					player = GameObject.FindGameObjectWithTag("Player")?.transform;
				}

				return player;
			}
		}

		public WeaponModule PlayerWeaponModule
		{
			get
			{
				if (Player is null)
				{
					return null;
				}
				else
				{
					if (weaponModule is null)
					{
						weaponModule = Player?.GetComponentInChildren<AbMainModule>()?.GetModuleComponent<WeaponModule>(ModuleType.Weapon);
					}
					return weaponModule;
				}
			}
		}
		public ItemModule PlayerItemModule
		{
			get
			{
				if (Player is null)
				{
					return null;
				}
				else
				{
					if (itemModule is null)
					{
						itemModule = Player?.GetComponentInChildren<AbMainModule>()?.GetModuleComponent<ItemModule>(ModuleType.Item);
					}
					return itemModule;
				}
			}
		}
		public InventoryEventTransmit InventoryEventTransmit
		{
			get
			{
				return inventoryEventTransmit;
			}
			set
			{
				inventoryEventTransmit = value;
			}
		}

		private Transform player;
		private WeaponModule weaponModule;
		private ItemModule itemModule;

		private AllItemDataSO allItemDataSO;
		private InventorySO inventorySO;
		private bool isInit;
		private int quickSlotIndex = 0;

		private InventoryEventTransmit inventoryEventTransmit = default;

		private void Start()
		{
			Init();
		}

		public void SendEvent(string _recipient, object _obj)
		{
			InventoryEventTransmit?.Invoke("InventoryManager", _recipient, _obj);
		}

		public void ReceiveEvent(string _sender, object _obj)
		{

		}

		public void Update()
		{
			if (!GameManager.GamePlayerManager.Instance.IsPlaying)
			{
				player = null;
				weaponModule = null;
				itemModule = null;
				return;
			}

			if (Input.GetKeyDown(KeyCode.G))
			{
				CurrentItemDrop();
			}

			float wheel = Input.GetAxisRaw("Mouse ScrollWheel");
			if (wheel >= 0.1f)
			{
				quickSlotIndex++;
				if (quickSlotIndex > 4)
				{
					quickSlotIndex = 0;
				}
				ChangeWeapon();
			}
			else if (wheel <= -0.1f)
			{
				quickSlotIndex--;
				if (quickSlotIndex < 0)
				{
					quickSlotIndex = 4;
				}
				ChangeWeapon();
			}

			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				quickSlotIndex = 0;
				ChangeWeapon();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				quickSlotIndex = 1;
				ChangeWeapon();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				quickSlotIndex = 2;
				ChangeWeapon();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				quickSlotIndex = 3;
				ChangeWeapon();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				quickSlotIndex = 4;
				ChangeWeapon();
			}
		}

		private void ChangeWeapon()
		{
			var value = GetQuickSlotItemKey();
			PlayerWeaponModule?.ChangeWeapon(value.Item1, value.Item2);
		}

		public List<ItemData> GetWeaponAndConsumptionList()
		{
			if (!isInit)
			{
				Init();
				isInit = true;
			}
			List<ItemData> _itemDataList = inventorySO.itemDataList.Where(item => item.itemType == ItemType.Weapon || item.itemType == ItemType.Consumption).ToList();
			return _itemDataList;
		}
		public List<ItemData> GetWeaponList()
		{
			return GetWhereItem(ItemType.Weapon);
		}
		public List<ItemData> GetConsumptionList()
		{
			return GetWhereItem(ItemType.Weapon);
		}
		public List<ItemData> GetSkillList()
		{
			return GetWhereItem(ItemType.Weapon);
		}
		public List<ItemData> GetEquipmentList()
		{
			return GetWhereItem(ItemType.Weapon);
		}
		public List<ItemData> GetAccessoriesList()
		{
			return GetWhereItem(ItemType.Weapon);
		}
		public List<ItemData> GetMaterialList()
		{
			return GetWhereItem(ItemType.Weapon);
		}
		public List<ItemData> GetValuableList()
		{
			return GetWhereItem(ItemType.Valuable);
		}

		public List<ItemData> GetAllItem()
		{
			return inventorySO.itemDataList;
		}

		public int GetMoney()
		{
			return inventorySO.money;
		}
		public int IncreaseMoney(int _money)
		{
			return inventorySO.money += _money;
		}

		private List<ItemData> GetWhereItem(ItemType _itemType)
		{
			List<ItemData> _itemDataList = inventorySO.itemDataList.Where(item => item.itemType == _itemType).ToList();
			return _itemDataList;
		}

		public void AddItem(string _itemKey, int _count = 1)
		{
			if (!isInit)
			{
				Init();
				isInit = true;
			}

			ItemData _originItemData = allItemDataSO.GetItemData(_itemKey);
			if (_originItemData.stackble)
			{
				List<ItemData> _itemDatas = inventorySO.itemDataList.FindAll(x => x.key == _itemKey);
				for (int i = 0; i < _itemDatas.Count; ++i)
				{
					if (_itemDatas[i].IsStackble)
					{
						_itemDatas[i].count += _count;
						SendEvent("QuestManager", null);
						return;
					}
				}

				while (_count > 0)
				{
					ItemData _itemData = ItemData.CopyItemData(allItemDataSO.itemDataDic[_itemKey]);
					_itemData.count = _count;
					if (_itemData.stackMax < _count)
					{
						_itemData.count = _itemData.stackMax;
						_count -= _itemData.stackMax;
					}
					else
					{
						inventorySO.itemDataList.Add(_itemData);
						break;
					}

					inventorySO.itemDataList.Add(_itemData);
				}
			}
			else
			{
				for (int i = 0; i < _count; ++i)
				{
					ItemData _itemData = ItemData.CopyItemData(allItemDataSO.itemDataDic[_itemKey]);
					inventorySO.itemDataList.Add(_itemData);
				}
			}
			SendEvent("QuestManager", null);
		}

		public ItemData GetItem(string _itemKey)
		{
			if (!isInit)
			{
				Init();
				isInit = true;
			}

			ItemData itemData = inventorySO.itemDataList.Find(x => x.key == _itemKey);
			if (itemData is not null)
			{
				return itemData;
			}
			return null;
		}

		public void SetQuickSlotItem(ItemData _itemData, int _index)
		{
			inventorySO.quickSlot[_index] = _itemData;
		}

		[ContextMenu("TestSetQuickSlotItem")]
		public void TestSetQuickSlotItem()
		{
			for (int i = 0; i < 5; ++i)
			{
				if (inventorySO.itemDataList.Count > i && inventorySO.itemDataList[i] is not null)
				{
					inventorySO.quickSlot[i] = inventorySO.itemDataList[i];
				}
				else
				{
					inventorySO.quickSlot[i] = null;
				}
			}
		}

		public (string, string) GetQuickSlotItemKey()
		{
			ItemData _itemData = GetCurrentQuickSlotItem();
			if (_itemData is not null)
			{
				return (_itemData.prefebkey, _itemData.animationLayer);
			}
			return (null, null);
		}

		public ItemData GetCurrentQuickSlotItem()
		{
			if (inventorySO.quickSlot[quickSlotIndex] is not null)
			{
				return inventorySO.quickSlot[quickSlotIndex];
			}
			return null;
		}
		public ItemData GetQuickSlotItem(int _index)
		{
			return inventorySO.quickSlot[_index];
		}

		public bool ItemCheck(string _key, int _count)
		{
			int _allCount = 0;

			List<ItemData> _itemList = inventorySO.itemDataList.Where(item => item.key == _key).ToList();

			for (int i = 0; i < _itemList.Count; ++i)
			{
				_allCount += _itemList[i].count;
			}

			if (_allCount >= _count)
			{
				return true;
			}
			return false;
		}

		public void ItemReduce(string _key, int _count)
		{
			List<ItemData> _itemList = inventorySO.itemDataList.Where(item => item.key == _key).ToList();

			for (int i = 0; i < _itemList.Count; ++i)
			{
				int _reduceCount = _itemList[i].count;
				_itemList[i].count -= _count;
				_count -= _reduceCount;
				if (_itemList[i].count <= 0)
				{
					ItemRemove(_itemList[i]);
				}

				if (_count <= 0)
				{
					break;
				}
			}
		}

		public void ItemRemove(int index)
		{
			inventorySO.itemDataList[index] = null;
			inventorySO.itemDataList.RemoveAt(index);
		}
		public void ItemRemove(ItemData itemData)
		{
			inventorySO.itemDataList.Remove(itemData);
			itemData = null;
		}
		public ItemData GetItemIndex(int _index)
		{
			return inventorySO.itemDataList[_index];
		}

		//장비 장착
		public void EquipEquipment(int index, ItemData _itemData)
		{
			switch(index)
			{
				case 0:
					if(_itemData.equipmentType != EquipmentType.Head)
					{
						return;
					}
					break;
				case 1:
					if (_itemData.equipmentType != EquipmentType.Armor)
					{
						return;
					}
					break;
				case 2:
					if (_itemData.equipmentType != EquipmentType.Pants)
					{
						return;
					}
					break;
				case 3:
					if (_itemData.equipmentType != EquipmentType.Shoes)
					{
						return;
					}
					break;
			}
			RemoveEquipment(index);
			inventorySO.equipments[index] = _itemData;

			//장비스탯 처리

			return;
		}
		public void RemoveEquipment(int _index)
		{
			if (inventorySO.equipments[_index] is not null)
			{
				inventorySO.equipments[_index] = null;
			}
		}

		//장신구 장착
		public void EquipAccessories(int _index, ItemData _itemData)
		{
			if (inventorySO.accessories[0] is not null && inventorySO.accessories[0].key == _itemData.key)
			{
				return;
			}
			if (inventorySO.accessories[1] is not null && inventorySO.accessories[1].key == _itemData.key)
			{
				return;
			}
			if (inventorySO.accessories[2] is not null && inventorySO.accessories[2].key == _itemData.key)
			{
				return;
			}
			if (inventorySO.accessories[3] is not null && inventorySO.accessories[3].key == _itemData.key)
			{
				return;
			}
			RemoveAccessories(_index);
			inventorySO.accessories[_index] = _itemData;

			//장신구스탯 처리
			PlayerItemModule.SetPassiveItem(_itemData.accessoriesItemType);

			return;
		}

		public void RemoveAccessories(int _index)
		{
			if (inventorySO.accessories[_index] is not null)
			{
				ItemData _itemData = inventorySO.accessories[_index];
				PlayerItemModule.RemovePassiveItem(_itemData.accessoriesItemType);
				inventorySO.accessories[_index] = null;
			}
		}

		//스킬 장착
		public void EquipSkill(int _index, ItemData _itemData)
		{
			if (inventorySO.skills[0] is not null && inventorySO.skills[0].key == _itemData.key)
			{
				return;
			}
			if (inventorySO.skills[1] is not null && inventorySO.skills[1].key == _itemData.key)
			{
				return;
			}
			RemoveSkill(_index);
			inventorySO.skills[_index] = _itemData;

			//스킬 추가 처리

			return;
		}

		public void CurrentItemDrop()
		{
			if (inventorySO.quickSlot[quickSlotIndex] is null)
			{
				return;
			}
			DropItem(inventorySO.quickSlot[quickSlotIndex]);
			inventorySO.quickSlot[quickSlotIndex] = null;
			ChangeWeapon();
		}

		public void DropItem(ItemData _itemData)
		{
			if(_itemData.dropItemPrefebKey is null || _itemData.dropItemPrefebKey == "")
            {
				return;
            }
			GameObject _dropObj = ObjectPoolManager.Instance.GetObject(_itemData.dropItemPrefebKey);
			_dropObj.transform.position = new Vector3(Player.position.x, Player.position.y + 2f, Player.position.z);
			_dropObj.SetActive(true);
		}

		public void RemoveSkill(int _index)
		{
			if (inventorySO.skills[_index] is not null)
			{
				inventorySO.skills[_index] = null;
			}
		}

		private void Init()
		{
			allItemDataSO = AddressablesManager.Instance.GetResource<AllItemDataSO>("AllItemDataSO");
			inventorySO = AddressablesManager.Instance.GetResource<InventorySO>("InventorySO");

			allItemDataSO.ItemDataGenerate();
		}
	}

}