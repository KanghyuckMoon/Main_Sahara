using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Module
{
    public partial class InputModule : AbBaseModule
    {
        PlayerInput playerInput;

        public InputModule(MainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            playerInput = mainModule.gameObject.GetComponentInParent<PlayerInput>();
        }
    }
}
