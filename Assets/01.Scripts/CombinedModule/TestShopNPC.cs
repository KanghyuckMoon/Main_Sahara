using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using Module;
using Module.Talk;
using Module.Shop;
using Cinemachine;
using Talk;

namespace CondinedModule
{
	public class TestShopNPC : TalkNPC
    {
        public string shopSOAddress;


        protected override void ModuleAdd()
        {
	        base.ModuleAdd();
            AddModuleWithPool<ShopModule>(ModuleType.Shop, "ShopModule", shopSOAddress);
        }
    }

}