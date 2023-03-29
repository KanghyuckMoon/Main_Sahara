#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using Quest;
using System.Linq;
using GoogleSpreadSheet;
using System;
using Module;
using Module.Talk;

	public class TextSOMaker : MonoBehaviour
    {
        public AllTalkDataSO allTalkDataSO;


        [ContextMenu("MakeTalkSO")]
        public void MakeTalkSO()
        {
            StartCoroutine(GetText());
        }
        private IEnumerator GetText()
        {
            UnityWebRequest wwwItemSO = UnityWebRequest.Get(URL.TALKSO);
            yield return wwwItemSO.SendWebRequest();
            SetSOTalk(wwwItemSO.downloadHandler.text);
        }
        private void SetSOTalk(string tsv)
        {
            string[] row = tsv.Split('\n');
            int rowSize = row.Length;

            for (int i = 1; i < rowSize; i++)
            {
                string[] column = row[i].Split('\t');

                string _key = column[0];
                string[] _talkKey = column[1].Split(',');
                string[] _authorKey = column[2].Split(',');
                string[] _talkCondition = column[3].Split(',');
                string[] _cutSceneKey = column[4].Split(',');
                string[] _isUseCutScene = column[5].Split(',');
                string[] _questKey = column[6].Split(',');
                string[] _questState = column[7].Split(',');
                string[] _defaultTalkCodeList = column[8].Split(',');
                string[] _defaultAutherCodeList = column[9].Split(',');
                string _talkRange = column[10];


                TalkDataSO _asset = null;
                _asset = allTalkDataSO.talkDataSOList.Find(x => x.key == _key);
                //이미 있는지
                if (_asset == null)
                {
                    _asset = ScriptableObject.CreateInstance<TalkDataSO>();

                    AssetDatabase.CreateAsset(_asset, $"Assets/02.ScriptableObject/TalkSO/{_key}_TalkSO.asset");
                    AssetDatabase.SaveAssets();

                    EditorUtility.FocusProjectWindow();

                    Selection.activeObject = _asset;

                    allTalkDataSO.talkDataSOList.Add(_asset);
                }

                List<TalkData> _talkDataList = new List<TalkData>();

                for (int j = 0; j < _talkKey.Length; ++j)
				{
                    TalkData _talkData = new TalkData();

                    _talkData.talkText = _talkKey[j];
                    _talkData.authorText = _authorKey[j];
                    _talkData.talkCondition = (TalkCondition)Enum.Parse(typeof(TalkCondition), _talkCondition[j]);
                    _talkData.cutSceneKey = _cutSceneKey[j];
                    _talkData.isUseCutScene = Convert.ToBoolean(_isUseCutScene[j]);
                    _talkData.questKey = _questKey[j];
                    _talkData.questState = (QuestState)Enum.Parse(typeof(QuestState), _questState[j]);

                    _talkDataList.Add(_talkData);
                }

                _asset.key = _key;
                _asset.talkDataList = _talkDataList;
                _asset.defaultTalkCodeList = _defaultTalkCodeList.ToList();
                _asset.defaultAutherCodeList = _defaultAutherCodeList.ToList();
                _asset.talkRange = float.Parse(_talkRange);

                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();

                Selection.activeObject = _asset;
            }
        }

    }
#endif
