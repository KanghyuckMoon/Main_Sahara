using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace CondinedModule
{

public class RestoreTarget : MonoBehaviour
{
    public virtual void Restore(AbMainModule _target)
    {
        var _statModule = _target.GetModuleComponent<StatModule>(ModuleType.Stat);
        var _stateModule = _target.GetModuleComponent<StateModule>(ModuleType.State);
        _statModule.Restore();
        _stateModule.Restore();
        _target.CharacterController.enabled = true;
        _target.IsDead = false;
    }
}
}

