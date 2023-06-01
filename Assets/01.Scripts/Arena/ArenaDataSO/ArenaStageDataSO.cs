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
        [Header("������ ����Ʈ")]
        public List<ArenaStageData> arenaStageList = new List<ArenaStageData>(); 
        [Header("�ִ� ������ �ܰ�")]
        public int maxLevel;
        [Header("���� ������ �ܰ�")] 
        public int curLevel; 
        
        public bool isClear;
    }
}