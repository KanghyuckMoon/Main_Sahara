using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Pool;
using Random = UnityEngine.Random;

namespace EnemyComponent
{
    public class RandomEquip : MonoBehaviour
    {
        [SerializeField]
        private List<string> randomEquipList = new List<string>();
        
        [SerializeField]
        private List<string> randomWeaponList = new List<string>();

        [SerializeField] 
        private List<string> dropEquipKey;
        
        [SerializeField] 
        private List<string> dropWeaponKey;
        
        [SerializeField] 
        private AbMainModule mainModule;

        private int equipIndex;
        private int weaponIndex;
        
        private void OnEnable()
        {
            EquipOnRandomList();
        }

        private void EquipOnRandomList()
        {
            var _equipmentModule = mainModule.GetModuleComponent<EquipmentModule>(ModuleType.Equipment);
            equipIndex = Random.Range(0, randomEquipList.Count);
            _equipmentModule.OnEquipItem(randomEquipList[equipIndex]);
            
            
            var _weaponModule = mainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon);
            weaponIndex = Random.Range(0, randomWeaponList.Count);
            _weaponModule.ChangeWeapon(randomWeaponList[weaponIndex], null);
        }

        public void DeadDropItem()
        {
            ItemDrop(dropEquipKey[equipIndex]);
            ItemDrop(dropWeaponKey[weaponIndex]);
        }
        
        
        private void ItemDrop(string _key)
        {
            if (_key is null || _key == "")
            {
                return;
            }
            GameObject _dropObj = ObjectPoolManager.Instance.GetObject(_key);
            _dropObj.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
            _dropObj.SetActive(true);
        }
    }
   
}