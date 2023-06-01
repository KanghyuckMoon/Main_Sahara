using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    [System.Serializable]
    public class ArenaStageData
    {
        public int level;
        public GameObject arenaPrefab; 
    }
    
    [CreateAssetMenu(menuName = "SO/ArenaDataSO")]
    public class ArenaStageDataSO : ScriptableObject
    {
        [Header("투기장 리스트")]
        public List<ArenaStageData> arenaStageList = new List<ArenaStageData>(); 
        [Header("최대 투기장 단계")]
        public int maxLevel;
        [Header("현재 투기장 단계")] 
        public int curLevel; 
        
        public bool isClear;
    }
}