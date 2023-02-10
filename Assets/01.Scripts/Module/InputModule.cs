using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Module
{
    public partial class InputModule : AbBaseModule
    {
        PlayerInput playerInput;

        public InputModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            playerInput = mainModule.gameObject.GetComponentInParent<PlayerInput>();
            //playerInput.
            //playerInput.actions["Move"].performed += OnMove;
            //playerInput.actions["Camera"].performed += OnLook;
            //playerInput.actions["LockOn"].performed += OnLock;
            //playerInput.actions["Jump"].performed += OnJump;
        }

        public override void FixedUpdate()
        {
        }

        public override void Awake()
        {
        }
    }
}
