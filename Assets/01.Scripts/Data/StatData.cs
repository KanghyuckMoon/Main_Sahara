using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using ForTheTest;

namespace Data
{
    public class StatData : MonoBehaviour, Obserble
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

        private float speed;
        private float jump;

        private PlayerData_TesSO playerdata;

        public void Awake()
        {
            playerdata = AddressablesManager.Instance.GetResource<PlayerData_TesSO>(dataSOPath);
            MaxHp = playerdata.hp;
            MaxMana = playerdata.mana;
            Jump = playerdata.jumpPower;
            Speed = playerdata.speed;
        }

        public void ChargeMana(int addMana)
        {
            CurrentMana = (CurrentMana + addMana) >= MaxMana ? MaxMana : CurrentMana + addMana;
        }

        #region 옵저버 부분
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
        #endregion
    }
}