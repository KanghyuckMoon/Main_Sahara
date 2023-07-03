using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondinedModule
{
    public class NPCTeleportEvent : MonoBehaviour
    { 
        [SerializeField]
        private NPCRegisterManager.NPCTYPE npeType;

        public void Teleport()
        {
            var _npc = NPCRegisterManager.Instance.Get(npeType);
            _npc.transform.position = transform.position;
        }
    }
    
}
