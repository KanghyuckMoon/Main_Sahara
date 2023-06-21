using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arena
{
    public class PlatformArenaMap : ArenaMap
    {

        [SerializeField]
        private List<PlatformBase> platformList = new List<PlatformBase>();

        
        protected override void Awake()
        {
            base.Awake();
            platformList = GetComponentsInChildren<PlatformBase>().ToList(); 
        }
    }
}
