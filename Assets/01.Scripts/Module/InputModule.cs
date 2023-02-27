using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Module
{
    public partial class InputModule : AbBaseModule
    {
        private PlayerInput playerInput;

        private AttackModule AttackModule
        {
            get
            {
                attackModule ??= mainModule.GetModuleComponent<AttackModule>(ModuleType.Attack);
                return attackModule;
            }
        }
        private StateModule StateModule
        {
            get
            {
                stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
                return stateModule;
            }
        }

        private AttackModule attackModule;
        private StateModule stateModule;

        public InputModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            playerInput = mainModule.gameObject.GetComponentInParent<PlayerInput>();
        }

        public override void FixedUpdate()
        {
        }

        public override void Awake()
        {
        }
    }
}
