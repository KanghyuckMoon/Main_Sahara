using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public enum ModuleType
    {
        Main,
        Input,
        Move,
        Camera,
        State,
        Jump,
        Hp,
        Attack,
        Animation,
        Pysics,
        UI,
        None
    }

    public abstract class AbBaseModule
    {
        public MainModule MainModule => mainModule;

        protected MainModule mainModule;

        public AbBaseModule(MainModule _mainModule)
        {
            mainModule = _mainModule;
            Awake();
        }

        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void LateUpdate() { }
        public virtual void OnCollisionEnter(Collision collision) { }
    }
}