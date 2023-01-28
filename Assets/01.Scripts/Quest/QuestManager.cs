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
		/// ��Ȱ��ȭ���� ����Ʈ���� �����´�
		/// </summary>
		/// <returns></returns>
		public List<QuestData> GetDisableQuest()
		{
			List<QuestData> disableQuestList = GetWhereQuset(QuestState.Disable);
			return disableQuestList;
		}
		/// <summary>
		/// �߰� ������ ����Ʈ���� �����´�
		/// </summary>
		/// <returns></returns>
		public List<QuestData> GetDiscoverableQuest()
		{
			List<QuestData> discoverableQuestList = GetWhereQuset(QuestState.Discoverable);
			return discoverableQuestList;
		}
		/// <summary>
		/// �߰��� ����Ʈ���� �����´�
		/// </summary>
		/// <returns></returns>
		public List<QuestData> GetActiveQuest()
		{
			List<QuestData> activeQuestList = GetWhereQuset(QuestState.Active);
			return activeQuestList;
		}
		/// <summary>
		/// Ŭ������ ����Ʈ���� �����´�
		/// </summary>
		/// <returns></returns>
		public List<QuestData> GetClearQuest()
		{
			List<QuestData> clearQuestList = GetWhereQuset(QuestState.Clear);
			return clearQuestList;
		}
		/// <summary>
		/// �߰��߰ų� Ŭ������ ����Ʈ���� �������
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
			//������Ʈ ����
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
					Debug.Log($"����Ʈ Ŭ���� : {_quest.Key}");
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