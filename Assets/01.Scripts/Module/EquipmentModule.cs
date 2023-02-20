using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EquipmentSystem;

namespace Module {
    public class EquipmentModule : AbBaseModule
    {
        //public Dictionary<>
        CharacterEquipment characterEquipment = new CharacterEquipment();

        public EquipmentModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            //mainModule.visualObject
        }

        public void SetCharacterBone()
        {
            for(int i=0;i< mainModule.visualObject.transform.childCount; i++) 
            {
                //characterEquipment.characterBons.Add();
            }
        }

        public void SetEquipmentItem()
        {

        }
    }
}