using System.Collections;
using System.Collections.Generic;
using Detect;
using UnityEngine;

namespace Arena
{
    public class ArenaCtrlDetectTrigger : AbArenaCtrlTrigger,IDetectItem
    {
        [SerializeField]
        private DetectItemType detectItemType;

        private bool isGetOut = false; 
        
        public List<Observer> Observers { get; }
        public DetectItemType DetectItemType
        {
            get => detectItemType;
            set => detectItemType = value; 
        }
        
        public bool IsGetOut { get => isGetOut; set => isGetOut = value; }
        public void GetOut()
        {
            isGetOut = true; 
            Interact();
        }
    }    
}

