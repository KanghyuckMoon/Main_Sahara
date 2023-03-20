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

        private StatData statData;
        private bool isLoad = false;

        public StatModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }
        public StatModule() : base()
        {
        }

        public override void Awake()
        {
            saveData ??= new SaveData();
            statData = mainModule.StatData;
        }

		public override void Start()
		{
			base.Start();
            if (!isLoad)
            {
                statData.CurrentHp = statData.MaxHp;
            }
        }

		public void SetAttackDamage(WeaponDataSO weaponDataSO)
        {
            statData.MeleeAttack = weaponDataSO.meleeAttack + mainModule.StatData.MeleeAttack;
            statData.RangeAttack = weaponDataSO.rangedAttack + mainModule.StatData.RangeAttack;
            statData.MagicAttack = weaponDataSO.magicAttack + mainModule.StatData.MagicAttack;
            //statData.Att = weaponDataSO.meleeAttack + weaponDataSO.rangedAttack + playerdata.meleeAttack + playerdata.rangedAttack;
            //apAttack = weaponDataSO.magicAttack + playerdata.magicAttack;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void SaveData()
        {
            saveData ??= new SaveData();
            statData ??= mainModule.GetComponent<StatData>();
            saveData.hp = statData.CurrentHp;
            saveData.mana = statData.CurrentMana;
            saveData.position = mainModule.transform.position;
        }

        public void LoadData()
        {
            saveData ??= new SaveData();
            statData ??= mainModule.StatData;
            statData.CurrentHp = saveData.hp;
            statData.CurrentMana = saveData.mana;
            mainModule.transform.position = saveData.position;
            isLoad = true;
        }

        public override void OnDisable()
        {
            statData = null;
            saveData = null;
            mainModule = null;
            base.OnDisable();
            ClassPoolManager.Instance.RegisterObject<StatModule>("StatModule", this);
        }
    }
}