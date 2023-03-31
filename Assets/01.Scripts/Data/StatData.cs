using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using ForTheTest;
using PlasticGui.Configuration;

namespace Data
{
    public class StatData : MonoBehaviour, IObserble
    {
        [SerializeField] private string dataSOPath;

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
        public int CurrentHp
        {
            get
            {
                return currentHp;
            }
            set
            {
                currentHp = value;
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
        public int CurrentMana
        {
            get
            {
                return currentMana;
            }
            set
            {
                currentMana = value;
                Send();
            }
        }
        public int MeleeAttack
        {
            get
            {
                return meleeAttack;
            }
            set
            {
                meleeAttack = value;
                Send();
            }
        }
        public int RangeAttack
        {
            get
            {
                return rangeAttack;
            }
            set
            {
                rangeAttack = value;
                Send();
            }
        }
        public int MagicAttack
        {
            get
            {
                return magicAttack;
            }
            set
            {
                magicAttack = value;
                Send();
            }
        }
        public int ManaRegen
        {
            get
            {
                return manaRegen;
            }
            set
            {
                manaRegen = value;
                Send();
            }
        }
        public int HealthRegen
        {
            get
            {
                return healthRegen;
            }
            set
            {
                healthRegen = value;
                Send();
            }
        }
        public int PhysicalResistance
        {
            get
            {
                return physicalResistance;
            }
            set
            {
                physicalResistance = value;
                Send();
            }
        }
        public int MagicResistance
        {
            get
            {
                return magicResistance;
            }
            set
            {
                magicResistance = value;
                Send();
            }
        }
        public float ManaChange
        {
            get
            {
                return manaChange;
            }
            set
            {
                manaChange = value;
            }
        }
        public float WalkSpeed
        {
            get
            {
                return walkSpeed;
            }
            set
            {
                walkSpeed = value;
                Send();
            }
        }
        public float RunSpeed
        {
            get
            {
                return runSpeed;
            }
            set
            {
                runSpeed = value;
                Send();
            }
        }
        public float Jump
        {
            get
            {
                return jump;
            }
            set
            {
                jump = value;
                Send();
            }
        }

        public List<Observer> Observers
        {
            get
            {
                return observers;
            }
        }

        private List<Observer> observers = new List<Observer>();

        private int maxHp;
        private int currentHp;
        private int maxMana;
        private int currentMana;
        private int meleeAttack;
        private int rangeAttack;
        private int magicAttack;

        private int physicalResistance;
        private int magicResistance;

        private int healthRegen;
        private int manaRegen;
        private float manaChange;

        private float walkSpeed;
        private float runSpeed;
        private float jump;

        private CreatureDataSO playerdata;

        public void Awake()
		{
			playerdata = AddressablesManager.Instance.GetResource<CreatureDataSO>(dataSOPath);
			MaxHp = playerdata.hp;
			CurrentHp = playerdata.hp;
			MaxMana = playerdata.mp;
            CurrentMana = playerdata.mp;
			Jump = playerdata.jumpScale;
            PhysicalResistance = playerdata.physicalResistance;
            MagicResistance = playerdata.magicResistance;
            MeleeAttack = playerdata.physicalMeleeAttack;
            RangeAttack = playerdata.physicalRangedAttack;
            MagicAttack = playerdata.magicAttack;
            HealthRegen = playerdata.healthRegen;
            ManaRegen = playerdata.manaRegen;
			WalkSpeed = playerdata.walkingSpeed;
			RunSpeed = playerdata.runSpeed;
			//try
			//{
			//}
			//catch
			//{
            //    AddressablesManager.Instance.GetResourceAsync<CreatureDataSO>(dataSOPath, AsyncSetPlayerData);
            //}
		}
        private void AsyncSetPlayerData(CreatureDataSO _creatureDataSO)
        {
            playerdata = _creatureDataSO;
            MaxHp = playerdata.hp;
            MaxMana = playerdata.mp;
            Jump = playerdata.jumpScale;
            WalkSpeed = playerdata.walkingSpeed;
            RunSpeed = playerdata.runSpeed;
        }
        public void ChargeMana(int addMana)
        {
            CurrentMana = (CurrentMana + addMana) >= MaxMana ? MaxMana : CurrentMana + addMana;
        }

        public int ChangeMana(int _mana)
        {
            int changed = (int)(_mana * (manaChange / 100));

            return changed;
        }
        public void LoadSaveData(StatSaveData _statSaveData)
        {
            maxHp = _statSaveData.maxHp;
            currentHp = _statSaveData.currentHp;
            maxMana = _statSaveData.maxMana;
            currentMana = _statSaveData.currentMana;
            meleeAttack = _statSaveData.meleeAttack;
            rangeAttack = _statSaveData.rangeAttack;
            magicAttack = _statSaveData.magicAttack;
            physicalResistance = _statSaveData.physicalResistance;
            magicResistance = _statSaveData.magicResistance;
            healthRegen = _statSaveData.healthRegen;
            manaRegen = _statSaveData.manaRegen;
            walkSpeed = _statSaveData.walkSpeed;
            runSpeed = _statSaveData.runSpeed;
            jump = _statSaveData.jump;
            Send();
        }

        public int CalculateDamage(int _physicalResistance, int _magicResistance)
        {
            var pDamage = (MeleeAttack + RangeAttack) - _physicalResistance;
            var mDamage = (MagicAttack) - _magicResistance;

            return Mathf.Max(pDamage + mDamage, 10);
        }

        #region 옵저버 부분
        public void AddObserver(Observer _observer)
        {
            Observers.Add(_observer);
            _observer.Receive();
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
		public void OnDisable()
		{
            Send();
            observers.Clear();
        }
        public void OnDestroy()
        {
            Send();
            observers.Clear();
        }
        #endregion
    }
}