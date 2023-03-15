using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Streaming;

namespace Spawner
{
    [CreateAssetMenu(fileName = "RandomMonsterListSO ", menuName = "SO/RandomMonsterListSO")]
    public class RandomMonsterListSO : ScriptableObject
    {
        public string key;

        public int minSpawnCount = 0;
        public int maxSpawnCount = 1;

        public float[] randomPercentArr;
        public RandomMonsterData[] spawnMonsterDataArr;
    }

    [System.Serializable]
    public class RandomMonsterData
    {
        public int minSpawnCount = 0;
        public int maxSpawnCount = 1;

        public string enemyAddress;
        public ObjectDataSO objectDataSO;
    }
}
