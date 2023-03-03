using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using ForTheTest;
using Weapon;
using Data;
using Pool;

namespace Module
{
    public class SaveData
    {
        public int hp;
        public int mana;
        public Vector3 position;
    }

    public class StatModule : AbBaseModule
    {
        public SaveData saveData;

        private HpModule HpModule
        {
            get
            {
                hpModule ??= mainModule.GetModuleComponent<HpModule>(ModuleType.Hp);
                return hpModule;
            }
        }
        private HpModule hpModule;

        private StatData statData;

        public StatModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }
        public StatModule() : base()
        {

        }

        public override void Awake()
        {
            saveData ??= new SaveData();
            statData = mainModule.GetComponent<Data.StatData>();
        }

        public void SetAttackDamage(WeaponDataSO weaponDataSO)
        {
            statData.MeleeAttack = weaponDataSO.meleeAttack;
            statData.RangeAttack = weaponDataSO.rangedAttack;
            statData.MagicAttack = weaponDataSO.magicAttack;
            //statData.Att = weaponDataSO.meleeAttack + weaponDataSO.rangedAttack + playerdata.meleeAttack + playerdata.rangedAttack;
            //apAttack = weaponDataSO.magicAttack + playerdata.magicAttack;
        }

        public void SaveData()
        {
            saveData ??= new SaveData();
            statData ??= mainModule.GetComponent<Data.StatData>();
            saveData.hp = statData.CurrentHp;
            saveData.mana = statData.CurrentMana;
            saveData.position = mainModule.transform.position;
        }

        public void LoadData()
        {
            saveData ??= new SaveData();
            statData ??= mainModule.GetComponent<Data.StatData>();
            statData.CurrentHp = saveData.hp;
            statData.CurrentMana = saveData.mana;
            mainModule.transform.position = saveData.position;
        }

        public override void OnDisable()
        {
            hpModule = null;
            statData = null;
            saveData = null;
            mainModule = null;
            base.OnDisable();
            ClassPoolManager.Instance.RegisterObject<StatModule>("StatModule", this);
        }
    }
}