using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using Module;
using Module.Talk;
using Module.Shop;

namespace CondinedModule
{
	public class TestTalkNPC : AbMainModule
    {
        public string textSOAddress;
        public string shopSOAddress;

        private void Awake()
        {
			//StopOrNot = 1;
			//canMove = true;

			moduleComponentsDic = new();
			//characterController = GetComponentInParent<CharacterController>();

			//AddModule(ModuleType.Input, new InputModule(this));
			//AddModule(ModuleType.Move, new MoveModule(this));
			//AddModule(ModuleType.State, new StateModule(this));
			//AddModule(ModuleType.Camera, new CameraModule(this));
			//AddModule(ModuleType.Jump, new JumpModule(this));
			//AddModule(ModuleType.Hp, new HpModule(this));
			//AddModule(ModuleType.Animation, new AnimationModule(this));
			//AddModule(ModuleType.Pysics, new PysicsModule(this));
			//AddModule(ModuleType.UI, new UIModule(this));

			//raycastTarget = transform.parent.Find("RayCastPoint");
			AddModule(ModuleType.Talk, new TalkModule(this, textSOAddress));
			AddModule(ModuleType.Shop, new ShopModule(this, shopSOAddress));
        }
    }

}