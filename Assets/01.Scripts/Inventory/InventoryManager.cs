using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using Utill.Addressable;
using System.Linq;
using Module;
using GameManager;
using Pool;
using UI.Base;
using TimeManager;
using UI.EventManage;

namespace Inventory
{
    public delegate void InventoryEventTransmit(string _sender, string _recipient, object _obj);

    public class InventoryManager : MonoSingleton<InventoryManager>
    {
        public Transform Player
        {
            get
            {
                if (player == null)
                {
                    player = PlayerObj.Player.transform;
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
                        weaponModule = Player?.GetComponentInChildren<AbMainModule>()
                            ?.GetModuleComponent<WeaponModule>(ModuleType.Weapon);
                    }

                    return weaponModule;
                }
            }
        }


        public EquipmentModule PlayerEquipmentModule
        {
            get
            {
                if (Player is null)
                {
                    return null;
                }
                else
                {
                    if (equipmentModule is null)
                    {
                        equipmentModule = Player?.GetComponentInChildren<AbMainModule>()
                            ?.GetModuleComponent<EquipmentModule>(ModuleType.Equipment);
                    }

                    return equipmentModule;
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
                        itemModule = Player?.GetComponentInChildren<AbMainModule>()
                            ?.GetModuleComponent<ItemModule>(ModuleType.Item);
                    }

                    return itemModule;
                }
            }
        }

        public StateModule PlayerStateModule
        {
            get
            {
                if (Player is null)
                {
                    return null;
                }
                else
                {
                    if (stateModule is null)
                    {
                        stateModule = Player?.GetComponentInChildren<AbMainModule>()
                            ?.GetModuleComponent<StateModule>(ModuleType.State);
                    }

                    return stateModule;
                }
            }
        }

        public InventoryEventTransmit InventoryEventTransmit
        {
            get { return inventoryEventTransmit; }
            set { inventoryEventTransmit = value; }
        }

        private Transform player;
        private WeaponModule weaponModule;
        private EquipmentModule equipmentModule;
        private ItemModule itemModule;
        private StateModule stateModule;

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

            if (StaticTime.UITime < 1f)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                //CurrentItemDrop();
            }

            if (PlayerStateModule.CheckState(State.ATTACK, State.SKILL, State.CHARGE))
            {
                return;
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
            EventManager.Instance.TriggerEvent(EventsType.UpdateQuickSlot);
        }

        public List<ItemData> GetWeaponAndConsumptionList()
        {
            if (!isInit)
            {
                Init();
                isInit = true;
            }

            List<ItemData> _itemDataList = inventorySO.itemDataList
                .Where(item => item.itemType == ItemType.Weapon || item.itemType == ItemType.Consumption).ToList();
            return _itemDataList;
        }

        public List<ItemData> GetEquipWeaponList()
        {
            return inventorySO.quickSlot.ToList();
        }

        public List<ItemData> GetEquipItemList()
        {
            return inventorySO.equipments.ToList();
        }

        public List<ItemData> GetEquipSoulList()
        {
            return inventorySO.accessories.ToList();
        }

        public ItemData GetEquipArrow()
        {
            return inventorySO.arrow;
        }


        public List<ItemData> GetWeaponList()
        {
            return GetWhereItem(ItemType.Weapon);
        }

        public List<ItemData> GetConsumptionList()
        {
            return GetWhereItem(ItemType.Consumption);
        }

        public List<ItemData> GetSkillList()
        {
            return GetWhereItem(ItemType.Weapon);
        }

        public List<ItemData> GetEquipmentList()
        {
            return GetWhereItem(ItemType.Equipment);
        }

        public List<ItemData> GetAccessoriesList()
        {
            return GetWhereItem(ItemType.Accessories);
        }

        public List<ItemData> GetMaterialList()
        {
            return GetWhereItem(ItemType.Weapon);
        }

        public List<ItemData> GetValuableList()
        {
            return GetWhereItem(ItemType.Valuable);
        }

        public List<ItemData> GetMarkerList()
        {
            return GetWhereItem(ItemType.Marker);
        }

        public List<ItemData> GetAllItem()
        {
            if (!isInit)
            {
                Init();
                isInit = true;
            }

            return inventorySO.itemDataList;
        }

        public int GetMoney()
        {
            return inventorySO.money;
        }

        public ItemData GetArrow()
        {
            if (inventorySO.arrow == null)
            {
                return null;
            }

            return inventorySO.arrow;
        }

        public int IncreaseMoney(int _money)
        {
            return inventorySO.money += _money;
        }

        private List<ItemData> GetWhereItem(ItemType _itemType)
        {
            if (!isInit)
            {
                Init();
                isInit = true;
            }

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
                        SendEvent("QuestManager", null);
                        SendEvent("PopupUIManager", _itemDatas[i]);
                        _itemDatas[i].count += _count;

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
                        SendEvent("QuestManager", null);
                        SendEvent("PopupUIManager", _itemData);
                        inventorySO.itemDataList.Add(_itemData);

                        break;
                    }
                    /*SendEvent("QuestManager", null);
                    SendEvent("PopupUIManager", _itemData);
                    inventorySO.itemDataList.Add(_itemData);
                    */
                }
            }
            else
            {
                for (int i = 0; i < _count; ++i)
                {
                    ItemData _itemData = ItemData.CopyItemData(allItemDataSO.itemDataDic[_itemKey]);
                    SendEvent("PopupUIManager", _itemData);
                    inventorySO.itemDataList.Add(_itemData);
                }
            }

            SendEvent("QuestManager", null);
            //	PopupUIManager.Instance.CreatePopup<PopupGetItemPr>(PopupType.GetItem, _originItemData);
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

        public bool SetQuickSlotItem(ItemData _itemData, int _index)
        {
            CheckSameEquip(inventorySO.quickSlot, _itemData);

            inventorySO.quickSlot[_index] = _itemData;
            return true;
        }

        public void RemoveQuickSlotItem(int _index)
        {
            inventorySO.quickSlot[_index] = null;
        }

        /// <summary>
        /// 중복 장착 체크 
        /// </summary>
        private void CheckSameEquip(ItemData[] _dataList, ItemData _data)
        {
            for (int i = 0; i < _dataList.Length; i++)
            {
                if (_dataList[i] == null) continue;
                if (_dataList[i].key == _data.key)
                {
                    _dataList[i] = null;
                }
            }
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

        public int GetCurrentQuickSlotIndex()
        {
            return quickSlotIndex;
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

        public bool ItemReduce(string _key, int _count = 1)
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
                    return false;
                    break;
                }
            }

            return true;
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
        public bool EquipEquipment(int index, ItemData _itemData)
        {
            switch (index)
            {
                case 0:
                    if (_itemData.equipmentType != EquipmentType.Head)
                    {
                        return false;
                    }

                    break;
                case 1:
                    if (_itemData.equipmentType != EquipmentType.Armor)
                    {
                        return false;
                    }

                    break;
                case 2:
                    if (_itemData.equipmentType != EquipmentType.Shoes)
                    {
                        return false;
                    }
                    break;

                case 3:
                    if (_itemData.equipmentType != EquipmentType.Pants)
                    {
                        return false;
                    }


                    break;
            }

            CheckSameEquip(inventorySO.equipments, _itemData);
            RemoveEquipment(index);
            inventorySO.equipments[index] = _itemData;

            //장비스탯 처리
            PlayerEquipmentModule.OnEquipItem(_itemData.prefebkey);
            return true;
        }

        public void RemoveEquipment(int _index)
        {
            if (inventorySO.equipments[_index] is not null)
            {
                inventorySO.equipments[_index] = null;
            }
            PlayerEquipmentModule.TakeOffItem(_index);

        }

        //장신구 장착
        public bool EquipAccessories(int _index, ItemData _itemData)
        {
            if (inventorySO.accessories[0] is not null && inventorySO.accessories[0].key == _itemData.key)
            {
                return false;
            }

            if (inventorySO.accessories[1] is not null && inventorySO.accessories[1].key == _itemData.key)
            {
                return false;
            }

            if (inventorySO.accessories[2] is not null && inventorySO.accessories[2].key == _itemData.key)
            {
                return false;
            }
            /*if (inventorySO.accessories[3] is not null && inventorySO.accessories[3].key == _itemData.key)
            {
                return false;
            }*/

            CheckSameAccessories(_itemData);
            RemoveAccessories(_index);
            inventorySO.accessories[_index] = _itemData;

            //장신구스탯 처리
            PlayerItemModule.SetPassiveItem(_itemData.accessoriesItemType);

            return true;
        }

        private void CheckSameAccessories(ItemData _data)
        {
            for (int i = 0; i < inventorySO.accessories.Length; i++)
            {
                if (inventorySO.accessories[i].key == _data.key)
                {
                    ItemData _itemData = inventorySO.accessories[i];
                    PlayerItemModule.RemovePassiveItem(_itemData.accessoriesItemType);
                    inventorySO.accessories[i] = null;
                }
            }
        }

        public void RemoveAccessories(int _index)
        {
            if (inventorySO.accessories[_index] is not null &&
                !string.IsNullOrEmpty(inventorySO.accessories[_index].key))
            {
                ItemData _itemData = inventorySO.accessories[_index];
                PlayerItemModule.RemovePassiveItem(_itemData.accessoriesItemType);
                inventorySO.accessories[_index] = new ItemData();
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
            if (_itemData.dropItemPrefebKey is null || _itemData.dropItemPrefebKey == "")
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

        //Arrow
        public bool EquipArrow(ItemData _arrow)
        {
            if (_arrow.consumptionType is not ConsumptionType.Arrow)
            {
                return false;
            }

            inventorySO.arrow = _arrow;
            //Weapon에 데이터 전달
            weaponModule.SetArrow(_arrow.prefebkey, SpendArrow);
            return true;
        }

        private void SpendArrow()
        {
            inventorySO.arrow.count--;
            if (inventorySO.arrow.count == 0)
            {
                ItemRemove(inventorySO.arrow);
                inventorySO.arrow = null;
                weaponModule.SetArrow(null, null);
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