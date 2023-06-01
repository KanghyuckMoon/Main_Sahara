using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Pool;
using Random = UnityEngine.Random;
using Effect;

namespace EnemyComponent
{
    public class FixedEquip : MonoBehaviour
    {
        [SerializeField]
        private List<string> equipList = new List<string>();

        [SerializeField] private string weapon;

        [SerializeField] private AccessoriesItemType soul;
        
        [SerializeField] 
        private List<string> dropEquipKey;
        
        [SerializeField] 
        private List<string> dropWeaponKey;
        
        [SerializeField] 
        private AbMainModule mainModule;

        [SerializeField] private string effectAddress;
        
        private int equipIndex;
        private int weaponIndex;
        
        private void OnEnable()
        {
            EquipOnRandomList();
        }

        private void EquipOnRandomList()
        {
            var _equipmentModule = mainModule.GetModuleComponent<EquipmentModule>(ModuleType.Equipment);
            foreach (var str in equipList)
            {
                _equipmentModule.OnEquipItem(str);
            }

            var _weaponModule = mainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon);
            _weaponModule.ChangeWeapon(weapon, null);
            
            var _itemModule = mainModule.GetModuleComponent<ItemModule>(ModuleType.Item);
            _itemModule.SetPassiveItem(soul);
        }

        public void DeadDropItem()
        {
            EffectManager.Instance.SetEffectDefault(effectAddress, transform.position, Quaternion.identity);
            foreach (var str in dropEquipKey)
            {
                ItemDrop(str);
            }
            foreach (var str in dropWeaponKey)
            {
                ItemDrop(str);
            }
        }
        
        
        private void ItemDrop(string _key)
        {
            if (_key is null || _key == "")
            {
                return;
            }
            GameObject _dropObj = ObjectPoolManager.Instance.GetObject(_key);
            _dropObj.transform.position = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
            _dropObj.SetActive(true);
        }
    }
   
}