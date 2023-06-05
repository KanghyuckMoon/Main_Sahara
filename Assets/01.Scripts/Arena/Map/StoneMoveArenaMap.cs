using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public class StoneMoveArenaMap : MonoBehaviour,IArenaMap
    {
        private List<GameObject> stoneList = new List<GameObject>();    
        public bool IsActive { get; }
        public void StartArena()
        {
        }

        public void CompleteArena()
        {
        }

        public bool CheckCondition()
        {
            return true; 
        }
            
        //private  void Create
    }
}
