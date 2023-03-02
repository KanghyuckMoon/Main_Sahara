using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    [CreateAssetMenu(fileName = "QuestSaveDataSO", menuName = "SO/QuestSaveDataSO")]
    public class QuestSaveDataSO : ScriptableObject
    {
        public List<QuestSaveData> questSaveDataList = new List<QuestSaveData>();
        public QuestSaveDataSave questSaveDataSave = new QuestSaveDataSave();

        public void ChangeQuestSaveData(string _key, QuestState _questState)
        {
            QuestSaveData _questSaveData = questSaveDataList.Find(x => x.key == _key);
            if (_questSaveData is not null)
            {
                _questSaveData.questState = _questState;
            }
            else
            {
                questSaveDataList.Add(new QuestSaveData(_key, _questState));
            }
        }
        public QuestSaveDataSave SaveData()
        {
            questSaveDataSave.questSaveDataList.Clear();
            questSaveDataSave.questSaveDataList = this.questSaveDataList;
            return questSaveDataSave;
        }

        public void LoadData()
        {
            this.questSaveDataList = questSaveDataSave.questSaveDataList;
        }
    }

    [HideInInspector]
    [System.Serializable]
    public class QuestSaveDataSave
    {
        public List<QuestSaveData> questSaveDataList = new List<QuestSaveData>();
    }

	[System.Serializable]
    public class QuestSaveData
	{
        public QuestSaveData(string _key, QuestState _questState)
		{
            this.key = _key;
            this.questState = _questState;

        }

        public string key;
        public QuestState questState;
	}
}