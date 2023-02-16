using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace CondinedModule
{

    public class EnemyModule : AbMainModule
    {
        private void Awake()
        {
            StopOrNot = 1;
            CanMove = true;

            moduleComponentsDic = new();
            CharacterController = GetComponent<CharacterController>();

            AddModule(ModuleType.State, new StatModule(this));
            AddModule(ModuleType.Hp, new HpModule(this));
            AddModule(ModuleType.Physics, new PhysicsModule(this));
            AddModule(ModuleType.Move, new MoveModule(this));
            AddModule(ModuleType.Animation, new AnimationModule(this));
            AddModule(ModuleType.Jump, new JumpModule(this));
            AddModule(ModuleType.Hit, new HitModule(this));

            RaycastTarget = transform.Find("RayCastPoint");
        }
    }
}