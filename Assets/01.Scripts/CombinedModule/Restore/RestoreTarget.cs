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
        _statModule.Restore();
        _target.IsDead = false;
    }
}
}

