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

    public class StateModule : AbBaseModule, Obserble
    {
        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
                Send();
            }
        }
        public float JumpPower
        {
            get
            {
                return jumpPower;
            }
            set
            {
                jumpPower = value;
                Send();
            }
        }
        public int MaxHp
        {
            get
            {
                return maxHp;
            }
            set
            {
                maxHp = value;
                Send();
            }
        }
        public int AdAttack
        {
            get
            {
                return adAttack;
            }
            set
            {
                adAttack = value;
                Send();
            }
        }
        public int ApAttack
        {
            get
            {
                return apAttack;
            }
            set
            {
                apAttack = value;
                Send();
            }
        }
        public int Mana
        {
            get
            {
                return mana;
            }
            set
            {
                mana = value;
                Send();
            }
        }
        public int MaxMana
        {
            get
            {
                return maxMana;
            }
            set
            {
                maxMana = value;
                Send();
            }
        }
        public int CurrentHp
        {
            get
            {
                return HpModule.CurrentHp;
            }
            set
            {
                HpModule.CurrentHp = value;
                Send();
            }
        }


        public SaveData saveData;
        public List<Observer> Observers
        {
            get
            {
                return observers;
            }
        }



        private List<Observer> observers = new List<Observer>();
        private HpModule HpModule
        {
            get
            {
                hpModule ??= mainModule.GetModuleComponent<HpModule>(ModuleType.Hp);
                return hpModule;
            }
        }

        private HpModule hpModule;

        private int mana;
        private int maxMana;
        private int adAttack;
        private int apAttack;
        private int maxHp;
        private float jumpPower;
        private float speed;
        
        private PlayerData_TesSO playerdata;

        public StateModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Awake()
        {
            playerdata = AddressablesManager.Instance.GetResource<PlayerData_TesSO>(mainModule.dataSOPath);
            saveData = new SaveData();
            maxHp = playerdata.hp;
            jumpPower = playerdata.jumpPower;
            speed = playerdata.speed;
        }
        public override void Start()
        {
            maxMana = playerdata.mana;
            mana = maxMana;
        }

        public void ChargeMana(int addMana)
        {
            mana = (mana + addMana) > maxMana ? maxMana : mana + addMana;
        }

        public void SetAttackDamage(WeaponDataSO weaponDataSO)
        {
            adAttack = weaponDataSO.meleeAttack + weaponDataSO.rangedAttack + playerdata.meleeAttack + playerdata.rangedAttack;
            apAttack = weaponDataSO.magicAttack + playerdata.magicAttack;
        }

        public void SaveData()
        {
            saveData.hp = HpModule.CurrentHp;
            saveData.mana = Mana;
            saveData.position = mainModule.transform.position;
            //saveData.mana = 
        }

        public void LoadData()
        {
            HpModule.CurrentHp = saveData.hp;
            Mana = saveData.mana;
            mainModule.transform.position = saveData.position;
        }

        public void AddObserver(Observer _observer)
        {
            Observers.Add(_observer);
        }

        public void RemoveObserver(Observer _observer)
        {
            Observers.Remove(_observer);
        }

        public void Send()
        {
            foreach (Observer observer in Observers)
            {
                observer.Receive();
            }
        }
    }
}