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
        ClassPoolManager.Instance.RegisterObject("InputModule", new InputModule());
        ClassPoolManager.Instance.RegisterObject("MoveModule", new MoveModule());
        ClassPoolManager.Instance.RegisterObject("StatModule", new StatModule());
        ClassPoolManager.Instance.RegisterObject("CameraModule", new CameraModule());
        ClassPoolManager.Instance.RegisterObject("JumpModule", new JumpModule());
        ClassPoolManager.Instance.RegisterObject("HpModule", new HpModule());
        ClassPoolManager.Instance.RegisterObject("AnimationModule", new AnimationModule());
        ClassPoolManager.Instance.RegisterObject("PhysicsModule", new PhysicsModule());
        ClassPoolManager.Instance.RegisterObject("UIModule", new UIModule());
        ClassPoolManager.Instance.RegisterObject("AttackModule", new AttackModule());
        ClassPoolManager.Instance.RegisterObject("WeaponModule", new WeaponModule());
        ClassPoolManager.Instance.RegisterObject("ItemModule", new ItemModule());
        ClassPoolManager.Instance.RegisterObject("EquipmentModule", new EquipmentModule());
        ClassPoolManager.Instance.RegisterObject("StateModule", new StateModule());
        ClassPoolManager.Instance.RegisterObject("SkillModule", new SkillModule());
        ClassPoolManager.Instance.RegisterObject("BuffModule", new BuffModule());
        
        EffectManager.Instance.SetEffectDefault("LandingSandVFX", transform.position, Quaternion.identity);
        EffectManager.Instance.SetEffectDefault("LandSandHitEffect", transform.position, Quaternion.identity);
        EffectManager.Instance.SetEffectDefault("BoomSandVFX", transform.position, Quaternion.identity);
        EffectManager.Instance.SetEffectDefault("BoomSandVFX", transform.position, Quaternion.identity);
        EffectManager.Instance.SetEffectDefault("HitWithSw_Effect1", transform.position, Quaternion.identity);
        EffectManager.Instance.SetEffectDefault("HitWithSw_Sound1", transform.position, Quaternion.identity);
    }
}
