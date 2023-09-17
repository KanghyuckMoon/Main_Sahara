using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Module.Talk;
using Talk;

namespace CondinedModule
{

    public class InvokePathFromTalkModule : MonoBehaviour
    {
        public NPCRegisterManager.NPCTYPE npcType;
        public int pathIndex;

        public void InvokePath()
        {
            NPCRegisterManager.Instance.Get(npcType)?.GetModuleComponent<TalkModule>(ModuleType.Talk)?.InvokePathAction(pathIndex);
        }
    }

}