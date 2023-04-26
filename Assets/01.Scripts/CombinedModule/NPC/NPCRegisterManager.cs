using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace CondinedModule
{
    public class NPCRegisterManager : MonoSingleton<NPCRegisterManager>
    {
        public enum NPCTYPE
        {
            None,
            Navi,
            Foolue,
        }

        public Dictionary<NPCTYPE, TalkNPC> talkNpcDic = new Dictionary<NPCTYPE, TalkNPC>();

        public void Register(NPCTYPE _npctype, TalkNPC _talkNpc)
        {
            talkNpcDic.Add(_npctype, _talkNpc);
        }

        public TalkNPC Get(NPCTYPE _npctype)
        {
            return talkNpcDic[_npctype];
        }
    }

}