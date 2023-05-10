using System.Collections;
using System.Collections.Generic;
using CondinedModule;
using Module;
using Module.Talk;
using UnityEngine;

namespace Talk
{
    public class EventNPCUsingPath : MonoBehaviour
    {
        [SerializeField] private NPCRegisterManager.NPCTYPE Npctype;
        
        [SerializeField] 
        private int index;
        
        public void NPCPathMove()
        {
            NPCRegisterManager.Instance.Get(Npctype).GetModuleComponent<TalkModule>(ModuleType.Talk).InvokePathAction(index);
        }
    }   
}
