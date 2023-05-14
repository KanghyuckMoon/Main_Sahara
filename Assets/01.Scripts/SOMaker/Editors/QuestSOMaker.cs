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

	public class QuestSOMaker : MonoBehaviour
	{
		public QuestDataAllSO questDataAllSO;


		[ContextMenu("MakeQuestSO")]
		public void MakeQuestSO()
		{
			StartCoroutine(GetText());
        }
        private IEnumerator GetText()
        {
            UnityWebRequest wwwItemSO = UnityWebRequest.Get(URL.QUESTSO);
            yield return wwwItemSO.SendWebRequest();
            SetSOQuest(wwwItemSO.downloadHandler.text);
        }

        private void SetSOQuest(string tsv)
        {
            string[] row = tsv.Split('\n');
            int rowSize = row.Length;

            for (int i = 1; i < rowSize; i++)
            {
                string[] column = row[i].Split('\t');

                string _questKey = column[0];
                string _nameKey = column[1];
                string _explanationKey = column[2];
                string _earlyQuestState = column[3];
                string _questConditionType = column[4];
                string[] _linkQuestKeyList = column[5].Split(',');
                string _isTalkQuest = column[6];
                string _isUpdate = column[7];
                

                QuestDataSO _asset = null;
                _asset = questDataAllSO.questDataSOList.Find(x => x.questKey == _questKey);
                //이미 있는지
                if (_asset == null)
                {
                    _asset = ScriptableObject.CreateInstance<QuestDataSO>();

                    AssetDatabase.CreateAsset(_asset, $"Assets/02.ScriptableObject/Quest/QuestDataSO/{_questKey}_QuestSO.asset");
                    AssetDatabase.SaveAssets();

                    EditorUtility.FocusProjectWindow();

                    Selection.activeObject = _asset;

                    questDataAllSO.questDataSOList.Add(_asset);
                }
                _asset.questKey = _questKey;
                _asset.nameKey = _nameKey;
                _asset.explanationKey = _explanationKey;
                _asset.earlyQuestState = (QuestState)Enum.Parse(typeof(QuestState), _earlyQuestState);
                _asset.questConditionType = (QuestConditionType)Enum.Parse(typeof(QuestConditionType), _questConditionType);
                _asset.linkQuestKeyList = _linkQuestKeyList.ToList();
                if (string.IsNullOrEmpty(_asset.linkQuestKeyList[0]))
                {
                    _asset.linkQuestKeyList = null;
                }
                _asset.isTalkQuest = Convert.ToBoolean(_isTalkQuest);
                _asset.isUpdate = Convert.ToBoolean(_isUpdate);

                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();
                UnityEditor.EditorUtility.SetDirty(_asset);

                Selection.activeObject = _asset;
            }
        }

    }

#endif
