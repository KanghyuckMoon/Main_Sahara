using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Module.Talk;
using Talk;

namespace CondinedModule
{

    public class InvokeEndTalk : MonoBehaviour
    {
        public NPCRegisterManager.NPCTYPE npcType;
        
        public void NpcEndTalk()
        {
            NPCRegisterManager.Instance.Get(npcType)?.GetModuleComponent<TalkModule>(ModuleType.Talk)?.EndTalk();
        }
    }

}