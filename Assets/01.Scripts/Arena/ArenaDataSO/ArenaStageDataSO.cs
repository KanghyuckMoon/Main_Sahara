using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public class ArenaStageData
    {
    
    }
    public class ArenaStageDataSO : ScriptableObject
    {
        [Header("현재 투기장 단계")]
        public int level;

        public int maxLevel;
        public bool isClear;

        public GameObject arenaPrefab; 
    }
}