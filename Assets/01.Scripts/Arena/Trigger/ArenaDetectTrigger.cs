using System.Collections;
using System.Collections.Generic;
using Detect;
using UnityEngine;

namespace Arena
{
    public class ArenaDetectTrigger : AbArenaTrigger,IDetectItem
    {
        [SerializeField]
        private DetectItemType detectItemType;
        public List<Observer> Observers { get; }
        public DetectItemType DetectItemType
        {
            get => detectItemType;
            set => detectItemType = value; 
        }
        
        public bool IsGetOut { get; set; }
        public void GetOut()
        {
            
        }
    }    
}

