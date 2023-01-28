using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using ForTheTest;

namespace Module
{
    public class StateModule : AbBaseModule
    {
        //public Player

        //private float 
        PlayerData_TesSO playerdata;

        public float Speed => playerdata.speed;
        public float JumpPower => playerdata.jumpPower;
        public int MaxHp => playerdata.hp;

        //private float speed;
        //private float jumpPower;
        //private int maxHp;

        public StateModule(MainModule _mainModule) : base(_mainModule)
        {
        }

        public override void Awake()
        {
            playerdata = AddressablesManager.Instance.GetResource<PlayerData_TesSO>("PlayerData_SO_Test");
            //speed = _playerdata.speed;
            //jumpPower = _playerdata.jumpPower;
            //maxHp = _playerdata.hp;
        }

        public override void FixedUpdate()
        {
        }

        public override void Start()
        {
        }

        public override void Update()
        {
        }
    }
}