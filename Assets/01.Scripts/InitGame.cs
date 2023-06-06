using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Module;
using Effect;

public class InitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ClassPoolManager.Instance.RegisterObject(new InputModule());
        ClassPoolManager.Instance.RegisterObject(new MoveModule());
        ClassPoolManager.Instance.RegisterObject(new StatModule());
        ClassPoolManager.Instance.RegisterObject(new CameraModule());
        ClassPoolManager.Instance.RegisterObject(new JumpModule());
        ClassPoolManager.Instance.RegisterObject(new HpModule());
        ClassPoolManager.Instance.RegisterObject(new AnimationModule());
        ClassPoolManager.Instance.RegisterObject(new PhysicsModule());
        ClassPoolManager.Instance.RegisterObject(new UIModule());
        ClassPoolManager.Instance.RegisterObject(new AttackModule());
        ClassPoolManager.Instance.RegisterObject(new WeaponModule());
        ClassPoolManager.Instance.RegisterObject(new ItemModule());
        ClassPoolManager.Instance.RegisterObject(new EquipmentModule());
        ClassPoolManager.Instance.RegisterObject( new StateModule());
        ClassPoolManager.Instance.RegisterObject(new SkillModule());
        ClassPoolManager.Instance.RegisterObject(new BuffModule());
        
        EffectManager.Instance.SetEffectDefault("LandingSandVFX", transform.position, Quaternion.identity);
        EffectManager.Instance.SetEffectDefault("LandSandHitEffect", transform.position, Quaternion.identity);
        EffectManager.Instance.SetEffectDefault("BoomSandVFX", transform.position, Quaternion.identity);
        EffectManager.Instance.SetEffectDefault("BoomSandVFX", transform.position, Quaternion.identity);
        EffectManager.Instance.SetEffectDefault("HitWithSw_Effect1", transform.position, Quaternion.identity);
        EffectManager.Instance.SetEffectDefault("HitWithSw_Sound1", transform.position, Quaternion.identity);
    }
}
