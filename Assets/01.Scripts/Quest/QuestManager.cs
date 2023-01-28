using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Streaming;
using Utill.Addressable;
using Utill.Pattern;
using System.Linq;

namespace Quest
{
    public partial class QuestManager : MonoSingleton<QuestManager>
    {
        private QuestDataAllSO questDataAllSO;
        public Dictionary<string, QuestData> questDataDic = new Dictionary<string, QuestData>();

		private void Awake()
		{
			if (Instance != this)
			{
				Destroy(gameObject);
			}

			questDataAllSO = AddressablesManager.Instance.GetResource<QuestDataAllSO>("QuestAllDataSO");
			InitQuestData();
		}


		public void LateUpdate()
		{
			ClearCheckQuest();
		}

		/// <summary>
		/// 비활성화중인 퀘스트들을 가져온다
		/// </summary>
		/// <returns></returns>
		public List<QuestData> GetDisableQuest()
		{
			List<QuestData> disableQuestList = GetWhereQuset(QuestState.Disable);
			return disableQuestList;
		}
		/// <summary>
		/// 발견 가능한 퀘스트들을 가져온다
		/// </summary>
		/// <returns></returns>
		public List<QuestData> GetDiscoverableQuest()
		{
			List<QuestData> discoverableQuestList = GetWhereQuset(QuestState.Discoverable);
			return discoverableQuestList;
		}
		/// <summary>
		/// 발견한 퀘스트들을 가져온다
		/// </summary>
		/// <returns></returns>
		public List<QuestData> GetActiveQuest()
		{
			List<QuestData> activeQuestList = GetWhereQuset(QuestState.Active);
			return activeQuestList;
		}
		/// <summary>
		/// 클리어한 퀘스트들을 가져온다
		/// </summary>
		/// <returns></returns>
		public List<QuestData> GetClearQuest()
		{
			List<QuestData> clearQuestList = GetWhereQuset(QuestState.Clear);
			return clearQuestList;
		}
		/// <summary>
		/// 발견했거나 클리어한 퀘스트들을 가졍노다
		/// </summary>
		/// <returns></returns>
		public List<QuestData> GetActiveOrClearQuest()
		{
			List<QuestData> activeAndClearQuestList = questDataDic.Where(item => item.Value.QuestState == QuestState.Active || item.Value.QuestState == QuestState.Clear).Select(item => item.Value).ToList();
			return activeAndClearQuestList;
		}

		partial void InitQuestData();

		private List<QuestData> GetWhereQuset(QuestState questState)
		{
			List<QuestData> disableQuestList = questDataDic.Where(item => item.Value.QuestState == questState).Select(item => item.Value).ToList();
			return disableQuestList;
		}

		private void CreateObject(string _sceneName, List<ObjectDataSO> _objectDataSOList)
		{
			SceneData _sceneData = SceneDataManager.Instance.GetSceneData(_sceneName);

			foreach (var _objectDataSO in _objectDataSOList)
			{
				ObjectData _objectData = new ObjectData();
				_objectData.CopyObjectDataSO(_objectDataSO);
				_objectData.key = ObjectData.totalKey++;
				_sceneData.AddObjectData(_objectData);
			}
		}

		private void CreateAllObject(List<QuestCreateObjectSO> _questCreateObjectSOList)
		{
			//오브젝트 생성
			foreach (var _createObjectSO in _questCreateObjectSOList)
			{
				if (_createObjectSO.targetSceneName == null)
				{
					continue;
				}
				CreateObject(_createObjectSO.targetSceneName, _createObjectSO.objectDataList);
			}
		}

		private void ClearCheckQuest()
		{
			foreach (var _quest in questDataDic)
			{
				if (_quest.Value.QuestState == QuestState.Disable || _quest.Value.QuestState == QuestState.Clear)
				{
					continue;
				}

				if (_quest.Value.IsClear())
				{
					Debug.Log($"퀘스트 클리어 : {_quest.Key}");
					_quest.Value.QuestState = QuestState.Clear;

					foreach (var _linkQuest in _quest.Value.LinkQuestKeyList)
					{
						if (questDataDic[_linkQuest].QuestState == QuestState.Disable)
						{
							questDataDic[_linkQuest].QuestState = QuestState.Discoverable;
							CreateAllObject(questDataDic[_linkQuest].QuestCreateObjectSOList);
						}
					}
				}
			}
		}

	}
}