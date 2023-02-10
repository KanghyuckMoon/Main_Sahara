using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using ForTheTest;
using Weapon;

namespace Module
{
    public class SaveData
    {
        public int hp;
        public int mana;
        public Vector3 position;
    }

    public class StateModule : AbBaseModule
    {
        public float Speed => playerdata.speed;
        public float JumpPower => playerdata.jumpPower;
        public int MaxHp => playerdata.hp;
        public int AdAttack => adAttack;
        public int ApAttack => apAttack;
        public int mana;
        public int maxMana;

        public SaveData saveData;

        private HpModule hpModule;
        private int adAttack;
        private int apAttack;
        
        private PlayerData_TesSO playerdata;

        public StateModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Awake()
        {
            playerdata = AddressablesManager.Instance.GetResource<PlayerData_TesSO>(mainModule.dataSOPath);
            saveData = new SaveData();
        }
        public override void Start()
        {
            hpModule = mainModule.GetModuleComponent<HpModule>(ModuleType.Hp);
            maxMana = playerdata.mana;
            mana = maxMana;
        }

        public void SetAttackDamage(WeaponDataSO weaponDataSO)
        {
            adAttack = weaponDataSO.meleeAttack + weaponDataSO.rangedAttack + playerdata.meleeAttack + playerdata.rangedAttack;
            apAttack = weaponDataSO.magicAttack + playerdata.magicAttack;
        }

        public void SaveData()
        {
            saveData.hp = hpModule.CurrentHp;
            saveData.mana = mana;
            saveData.position = mainModule.transform.position;
            //saveData.mana = 
        }

        public void LoadData()
        {
            hpModule.CurrentHp = saveData.hp;
            mana = saveData.mana;
            mainModule.transform.position = saveData.position;
        }
    }
}