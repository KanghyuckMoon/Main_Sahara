#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using Inventory;
using System.Linq;
using GoogleSpreadSheet;
using System;
using Module;
using Spawner;

public class EnemySpawnListSOMaker : MonoBehaviour
{
    public AllRandomMonsterListSO allRandomMonsterListSO;


    [ContextMenu("MakeEnemyListSO")]
    public void MakeEnemyListSO()
    {
        StartCoroutine(GetText());
    }

    private IEnumerator GetText()
    {
        UnityWebRequest wwwItemSO = UnityWebRequest.Get(URL.RANDOMMONSTERLISTSO);
        yield return wwwItemSO.SendWebRequest();
        SetSOEnemyList(wwwItemSO.downloadHandler.text);
    }


    private void SetSOEnemyList(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

        for (int i = 1; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            string _key = column[0];
            string _minCount = column[1];
            string _maxCount = column[2];
            string[] _randomPercent = column[3].Split(',');
            string[] _randomMonsterMinCount = column[4].Split(',');
            string[] _randomMonsterMaxCount = column[5].Split(',');
            string[] _randomMonsterEnemyAddress = column[6].Split(',');

            RandomMonsterListSO _asset = null;
            _asset = allRandomMonsterListSO.randomMonsterSpawnerSOList.Find(x => x.key == _key);
            
            if (_asset == null)
            {
                _asset = ScriptableObject.CreateInstance<RandomMonsterListSO>();

                AssetDatabase.CreateAsset(_asset, $"Assets/02.ScriptableObject/RandomSpawnEnemyListSO/{_key}_RMSO.asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();

                Selection.activeObject = _asset;

                allRandomMonsterListSO.randomMonsterSpawnerSOList.Add(_asset);
            }

            float[] _randomPercentArr = new float[_randomPercent.Length];
            RandomMonsterData[] _randomMonsterDataArr = new RandomMonsterData[_randomPercent.Length];

            for(int j = 0; j < _randomPercent.Length; ++j)
			{
                _randomPercentArr[j] = int.Parse(_randomPercent[j]);
                _randomMonsterDataArr[j] = new RandomMonsterData();
                _randomMonsterDataArr[j].minSpawnCount = int.Parse(_randomMonsterMinCount[j]);
                _randomMonsterDataArr[j].maxSpawnCount = int.Parse(_randomMonsterMaxCount[j]);
                _randomMonsterDataArr[j].enemyAddress = _randomMonsterEnemyAddress[j];
            }
            _asset.key = _key;
            _asset.minSpawnCount = int.Parse(_minCount);
            _asset.maxSpawnCount = int.Parse(_maxCount);
            _asset.randomPercentArr = _randomPercentArr;
            _asset.spawnMonsterDataArr = _randomMonsterDataArr;

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = _asset;
        }
    }
}

#endif