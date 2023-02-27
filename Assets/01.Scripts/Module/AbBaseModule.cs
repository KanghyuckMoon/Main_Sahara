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
        Stat,
        State,
        Jump,
        Hp,
        Attack,
        Animation,
        Physics,
        UI,
        Talk,
        Weapon,
        Hit,
        Shop,
        Item,
        Equipment,
        None
    }

    public abstract class AbBaseModule
    {
        public AbMainModule MainModule => mainModule;

        protected AbMainModule mainModule;

        public AbBaseModule(AbMainModule _mainModule)
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
        public virtual void OnTriggerEnter(Collider other) { }
        public virtual void OnDrawGizmos() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
        public virtual void OnDestroy() { }
    }
}