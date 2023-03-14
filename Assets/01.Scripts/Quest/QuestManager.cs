using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Streaming;
using Utill.Addressable;
using Utill.Pattern;
using Utill.Measurement;
using System.Linq;
using GameManager;

namespace Quest
{
	public delegate void QuestEventTransmit(string _sender, string _recipient, object _obj);
	public partial class QuestManager : MonoSingleton<QuestManager>, Observer
    {
        private QuestDataAllSO questDataAllSO;
		private QuestSaveDataSO questSaveDataSO;
        public Dictionary<string, QuestData> questDataDic = new Dictionary<string, QuestData>();
		private bool isInit = false;

		public QuestEventTransmit QuestEventTransmit
		{
			get
			{
				return questEventTransmit;
			}
			set
			{
				questEventTransmit = value;
			}
		}

		private QuestEventTransmit questEventTransmit = default;

		private void Awake()
		{
			if (Instance != this)
			{
				Destroy(gameObject);
			}

			GamePlayerManager.Instance.AddObserver(this);
			Receive();

			if (!isInit)
			{
				InitQuestData();
			}
			isInit = true;
		}

		public void SendEvent(string _recipient, object _obj)
		{
			questEventTransmit?.Invoke("QuestManager", _recipient, _obj);
		}

		public void ReceiveEvent(string _sender, object _obj)
		{
			if(_sender is "InventoryManager")
			{
				var _list = questDataDic.Where(item => item.Value.QuestConditionType == QuestConditionType.Inventory).Select(item => item.Value).ToList();
				foreach (var _quest in _list)
				{
					if (_quest.QuestState == QuestState.Disable || _quest.QuestState == QuestState.Clear || _quest.QuestState == QuestState.Achievable)
					{
						continue;
					}

					if (_quest.IsClear())
					{
						if (_quest.IsTalkQuest)
						{
							_quest.QuestState = QuestState.Achievable;
							questSaveDataSO.ChangeQuestSaveData(_quest.QuestKey, _quest.QuestState);
							//NPC 대화 데이터 설정
						}
						else
						{
							QuestClear(_quest);
						}
					}
				}
			}
		}



		public void LateUpdate()
		{
			if (!GamePlayerManager.Instance.IsPlaying)
			{
				return;
			}
			ClearCheckQuest();
		}

		public void LoadQuestSaveData(QuestSaveDataSO _questSaveDataSO)
		{
			if (!isInit)
			{
				InitQuestData();
			}
			for (int i = 0; i < _questSaveDataSO.questSaveDataList.Count; ++i)
			{
				QuestSaveData _questSaveData = _questSaveDataSO.questSaveDataList[i];
				questDataDic[_questSaveData.key].QuestState = _questSaveData.questState;
			}
		}

		public QuestData GetQuestData(string _key)
		{
			QuestData _questData = null;
			if (questDataDic.TryGetValue(_key, out _questData))
			{
				return _questData;
			}
			return null;
		}

		/// <summary>
		/// 비활성화중인 퀘스트들을 가져온다
		/// </summary>
		/// <returns></returns>
		public List<QuestData> GetDisableQuest(QuestCategory _questCategory = QuestCategory.Main)
		{
			List<QuestData> disableQuestList = GetWhereQuset(QuestState.Disable, _questCategory);
			return disableQuestList;
		}
		/// <summary>
		/// 발견 가능한 퀘스트들을 가져온다
		/// </summary>
		/// <returns></returns>
		public List<QuestData> GetDiscoverableQuest(QuestCategory _questCategory = QuestCategory.Main)
		{
			List<QuestData> discoverableQuestList = GetWhereQuset(QuestState.Discoverable, _questCategory);
			return discoverableQuestList;
		}
		/// <summary>
		/// 발견한 퀘스트들을 가져온다
		/// </summary>
		/// <returns></returns>
		public List<QuestData> GetActiveQuest(QuestCategory _questCategory = QuestCategory.Main)
		{
			List<QuestData> activeQuestList = GetWhereQuset(QuestState.Active, _questCategory);
			return activeQuestList;
		}
		/// <summary>
		/// 클리어한 퀘스트들을 가져온다
		/// </summary>
		/// <returns></returns>
		public List<QuestData> GetClearQuest(QuestCategory _questCategory = QuestCategory.Main)
		{
			List<QuestData> clearQuestList = GetWhereQuset(QuestState.Clear, _questCategory);
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

		public void ChangeQuestActive(string _key)
		{
			QuestData _questData = questDataDic[_key];
			if (_questData.QuestState == QuestState.Active || _questData.QuestState == QuestState.Clear || _questData.QuestState == QuestState.Achievable)
			{
				return;
			}
			_questData.QuestState = QuestState.Active;
		}
		public void ChangeQuestClear(string _key)
		{
			QuestData _questData = questDataDic[_key];
			if(_questData.QuestState is QuestState.Clear || _questData.QuestState is QuestState.Achievable)
			{
				return;
			}
			
			if (_questData.IsTalkQuest)
			{
				_questData.QuestState = QuestState.Achievable;
				questSaveDataSO.ChangeQuestSaveData(_questData.QuestKey, _questData.QuestState);
			}
			else
			{
				QuestClear(questDataDic[_key]);
			}
		}
		public void ChangeQuestDiscoverable(string _key)
		{
			QuestData _questData = questDataDic[_key];
			if (_questData.QuestState == QuestState.Active || _questData.QuestState == QuestState.Clear || _questData.QuestState == QuestState.Achievable)
			{
				return;
			}
			_questData.QuestState = QuestState.Discoverable;
			questSaveDataSO.ChangeQuestSaveData(_questData.QuestKey, _questData.QuestState);
			CreateAllObject(_questData.QuestCreateObjectSOList);
		}

		public void TalkQuestClear(string _key)
		{
			QuestClear(questDataDic[_key]);
		}

		public void Receive()
		{
			if(GamePlayerManager.Instance.IsPlaying)
			{
				InitQuestData();
			}
		}

		partial void InitQuestData();

		private List<QuestData> GetWhereQuset(QuestState _questState, QuestCategory questCategory)
		{
			List<QuestData> disableQuestList = questDataDic.Where(item => item.Value.QuestState == _questState && item.Value.QuestCategory == questCategory).Select(item => item.Value).ToList();
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
				_sceneData?.AddObjectData(_objectData);
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
				if (_quest.Value.QuestState == QuestState.Disable || _quest.Value.QuestState == QuestState.Clear || _quest.Value.QuestState == QuestState.Achievable)
				{
					continue;
				}

				if (_quest.Value.IsClear())
				{
					if (_quest.Value.IsTalkQuest)
					{
						_quest.Value.QuestState = QuestState.Achievable;
						questSaveDataSO.ChangeQuestSaveData(_quest.Value.QuestKey, _quest.Value.QuestState);
						//NPC 대화 데이터 설정
					}
					else
					{
						QuestClear(_quest.Value);
					}
				}
			}
		}

		private void QuestClear(QuestData _questData)
		{
			Logging.Log($"퀘스트 클리어 : {_questData.QuestKey}");
			_questData.QuestState = QuestState.Clear;
			questSaveDataSO.ChangeQuestSaveData(_questData.QuestKey, _questData.QuestState);

			foreach (var _linkQuest in _questData.LinkQuestKeyList)
			{
				if (questDataDic[_linkQuest].QuestState == QuestState.Disable)
				{
					questDataDic[_linkQuest].QuestState = QuestState.Discoverable;
					questSaveDataSO.ChangeQuestSaveData(questDataDic[_linkQuest].QuestKey, questDataDic[_linkQuest].QuestState);
					CreateAllObject(questDataDic[_linkQuest].QuestCreateObjectSOList);
				}
			}
		}
	}
}