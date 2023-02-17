using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Module;

namespace CondinedModule
{
    public class TestEnemy : AbMainModule, IEnemy
    {
        public string aiSOAddress = "TestEnemySO";
		public string AIAddress => aiSOAddress;

		private void Awake()
        {
            StopOrNot = 1;
            CanMove = true;

            moduleComponentsDic = new();
            CharacterController = GetComponent<CharacterController>();
            //footRotate = GetComponentInParent<csHomebrewIK>();
            AddModule(ModuleType.Input, new AIModule(this));
            AddModule(ModuleType.Move, new MoveModule(this));
            AddModule(ModuleType.State, new StatModule(this));
            //AddModule(ModuleType.Camera, new CameraModule(this));
            AddModule(ModuleType.Jump, new JumpModule(this));
            AddModule(ModuleType.Hp, new HpModule(this));
            AddModule(ModuleType.Animation, new AnimationModule(this));
            AddModule(ModuleType.Physics, new PhysicsModule(this));
            AddModule(ModuleType.UI, new UIModule(this, "HudUI"));
            AddModule(ModuleType.Attack, new AttackModule(this));
            AddModule(ModuleType.Weapon, new WeaponModule(this));
            AddModule(ModuleType.Hit, new HitModule(this));

            RaycastTarget = transform.Find("RayCastPoint");
        }
    }
}
