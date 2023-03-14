using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utill.Pattern;
using Quest;
using Inventory;
using Utill.Addressable;
using Shop;
using Module;
using CondinedModule;
using TimeManager;
using System;
using Streaming;
using PathMode;
using Unity.Jobs;
using Unity.Collections;
using Tutorial;

namespace Json
{
    public delegate void SaveEventTransmit(string _sender, string _recipient, object _obj);

    public class SaveManager : MonoSingleton<SaveManager>
    {
        public struct SceneSaveJob : IJobParallelFor
		{
            public NativeArray<FixedString4096Bytes> testArray;
            public FixedString128Bytes date;
            public void Execute(int index)
			{
                string _sceneKey = testArray[index].ToString();
                var _sceneData = SceneDataManager.Instance.SceneDataDic[_sceneKey].objectDataList;
                string _json = StaticSave.ReturnJson<ObjectDataList>(_sceneData);
				try
				{
					StaticSave.SaveJson<ObjectDataList>(_json, _sceneKey + date.ToString());
				}
				catch(Exception e)
				{
                    Debug.LogError("Can't Save");
				}
			}
        }

        public struct ShopSaveJob : IJobParallelFor
        {
            public FixedString128Bytes date;
            public void Execute(int index)
            {
                ShopSO _shopSO = SaveManager.Instance.ShopAllSO.shopSOList[index];
                _shopSO.SaveData();
                string _json = StaticSave.ReturnJson<ShopSave>(_shopSO.shopSave);
                try
                {
                    StaticSave.SaveJson<ShopSave>(_json, _shopSO.shopName + date.ToString());
                }
                catch (Exception e)
                {
                    Debug.LogError("Can't Save");
                }
            }
        }

        public struct StateSaveJob : IJob
        {
            public FixedString128Bytes date;
            public void Execute()
            {
                var _saveData = SaveManager.Instance.StateModuleReadOnly.saveData;
                string _json = StaticSave.ReturnJson<SaveData>(_saveData);
                try
                {
                    StaticSave.SaveJson<SaveData>(_json, date.ToString());
                }
                catch (Exception e)
                {
                    Debug.LogError("Can't Save");
                }
            }
        }
        public struct InventorySaveJob : IJob
        {
            public FixedString128Bytes date;
            public void Execute()
            {
                var _saveData = SaveManager.Instance.InventorySO;
                string _json = StaticSave.ReturnJson<InventorySave>(_saveData.inventorySave);
                try
                {
                    StaticSave.SaveJson<InventorySave>(_json, date.ToString());
                }
                catch (Exception e)
                {
                    Debug.LogError("Can't Save");
                }
            }
        }
        public struct QuestSaveJob : IJob
        {
            public FixedString128Bytes date;
            public void Execute()
            {
                var _saveData = SaveManager.Instance.QuestSaveDataSO;
                string _json = StaticSave.ReturnJson<QuestSaveDataSave>(_saveData.questSaveDataSave);
                try
                {
                    StaticSave.SaveJson<QuestSaveDataSave>(_json, date.ToString());
                }
                catch (Exception e)
                {
                    Debug.LogError("Can't Save");
                }
            }
        }

        public struct PathSaveJob : IJob
        {
            public FixedString128Bytes date;
            public void Execute()
            {
                string _json = StaticSave.ReturnJson<PathSave>(PathModeManager.Instance.pathSave);
                try
                {
                    StaticSave.SaveJson<ObjectDataList>(_json, date.ToString());
                }
                catch (Exception e)
                {
                    Debug.LogError("Can't Save");
                }
            }
        }

        public struct RecordSaveJob : IJob
        {
            public FixedString128Bytes date;
            public FixedString128Bytes imagePath;
            public void Execute()
            {
                SaveRecordDataList _saveRecordDataList = new SaveRecordDataList();
                StaticSave.Load<SaveRecordDataList>(ref _saveRecordDataList);

                SaveRecordData _saveRecordData = new SaveRecordData();
                _saveRecordData.date = date.ToString();
                _saveRecordData.imagePath = imagePath.ToString();
                _saveRecordDataList.dateList.Add(_saveRecordData);

                if (_saveRecordDataList.dateList.Count > 10)
                {
                    var _data = _saveRecordDataList.dateList[0];

                    File.Delete(StaticSave.GetPath() + _data.date + ".png");
                    File.Delete(StaticSave.GetPath() + "InventorySave" + _data.date);
                    File.Delete(StaticSave.GetPath() + "QuestSaveDataSave" + _data.date);
                    File.Delete(StaticSave.GetPath() + "PathSave" + _data.date);
                    File.Delete(StaticSave.GetPath() + "TutorialSaveData" + _data.date);

                    var _sceneDataList = SceneDataManager.Instance.SceneDataDic;
                    foreach (var _sceneData in _sceneDataList)
                    {
                        File.Delete(StaticSave.GetPath() + _sceneData.Key + _data.date);
                    }

                    for (int i = 0; i < SaveManager.Instance.ShopAllSO.shopSOList.Count; ++i)
                    {
                        ShopSO _shopSO = SaveManager.Instance.ShopAllSO.shopSOList[i];
                        File.Delete(StaticSave.GetPath() + _shopSO.shopName + _data.date);
                    }

                    _saveRecordDataList.dateList.RemoveAt(0);
                }

                string _json = StaticSave.ReturnJson<SaveRecordDataList>(_saveRecordDataList);
                StaticSave.SaveJson<SaveRecordDataList>(_json, "");
            }
        }

        public InventorySO InventorySO
		{
            get
			{
                if (inventorySO is null)
				{
                    inventorySO = AddressablesManager.Instance.GetResource<InventorySO>("InventorySO");
				}

                return inventorySO;
            }
        }
        public QuestSaveDataSO QuestSaveDataSO
        {
            get
            {
                if (questSaveDataSO is null)
                {
                    questSaveDataSO = AddressablesManager.Instance.GetResource<QuestSaveDataSO>("QuestSaveDataSO");
                }

                return questSaveDataSO;
            }
        }
        public ShopAllSO ShopAllSO
        {
            get
            {
                if (shopAllSO is null)
                {
                    shopAllSO = AddressablesManager.Instance.GetResource<ShopAllSO>("ShopAllSO");
                }

                return shopAllSO;
            }
        }

        public Transform Player
		{
            get
            {
                player = GameObject.FindObjectOfType<Player>(true).transform;
                return player;
			}
		}

        public StatModule StateModule
		{
            get
			{
                if (Player is null)
				{
                    return null;
                }
                
                AbMainModule _abMainModule = player?.GetComponentInChildren<AbMainModule>();
                stateModule = _abMainModule?.GetModuleComponent<StatModule>(ModuleType.Stat);

                return stateModule;

            }
		}
		private InventorySO inventorySO = null;

        [SerializeField]
        private QuestSaveDataSO questSaveDataSO = null;

        [SerializeField]
        private ShopAllSO shopAllSO = null;

        [SerializeField]
        private Transform player = null;

        public StatModule StateModuleReadOnly => stateModule;

        private StatModule stateModule;

        public string TestDate
		{
            get
			{
                return testDate;
			}
            set
			{
                testDate = value;

            }
		}            

        private string testDate;

        public bool IsContinue
		{
            get
			{
                return isContinue;
			}
            set
			{
                isContinue = value;
			}
        }

        public SaveEventTransmit SaveEventTransmit
        {
            get
            {
                return saveEventTransmit;
            }
            set
            {
                saveEventTransmit = value;
            }
        }

        private SaveEventTransmit saveEventTransmit = default;
        private bool isContinue = false;

        public bool isLoadSuccess = false;

        public void ReceiveEvent(string _sender, object _obj)
        {
            string _date = DateTime.Now.ToString("yyyyMMddhhmmss");
            StartCoroutine(SaveBackgroundThread(_date));
        }

        public SaveRecordDataList GetSaveRecordDataList()
        {
            SaveRecordDataList _saveRecordDataList = new SaveRecordDataList();
            StaticSave.Load<SaveRecordDataList>(ref _saveRecordDataList);

            return _saveRecordDataList;
        }

        public IEnumerator SaveBackgroundThread(string _date)
        {
            if (InventorySO is null || QuestSaveDataSO is null || ShopAllSO is null || Player is null || StateModule is null || PathModeManager.Instance is null)
            {
                yield break;
            }

            string _staticSavepath = StaticSave.GetPath();

            if (!File.Exists(_staticSavepath))
            {
                Directory.CreateDirectory($"{_staticSavepath}");
            }

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            StaticTime.EntierTime = 0;

            stateModule.SaveData();
            inventorySO.SaveData();
            questSaveDataSO.SaveData();

            string _imagePath = StaticSave.GetPath() + _date.ToString() + ".png";

            FixedString128Bytes _fixedDate = new FixedString128Bytes(_date);
            FixedString128Bytes _fixedimagePath = new FixedString128Bytes(_imagePath);


            StateSaveJob _stateSaveJob = new StateSaveJob();
            InventorySaveJob _inventorySaveJob = new InventorySaveJob();
            QuestSaveJob _questSaveJob = new QuestSaveJob();
            PathSaveJob _pathSaveJob = new PathSaveJob();
            RecordSaveJob _recordSaveJob = new RecordSaveJob();

            _stateSaveJob.date = _fixedDate;
            _inventorySaveJob.date = _fixedDate;
            _questSaveJob.date = _fixedDate;
            _pathSaveJob.date = _fixedDate;
            _recordSaveJob.date = _fixedDate;
            _recordSaveJob.imagePath = _fixedimagePath;

            JobHandle _stateSaveJobHandle = _stateSaveJob.Schedule();
            JobHandle _inventorySaveJobHandle = _inventorySaveJob.Schedule();
            JobHandle _questSaveJobHandle = _questSaveJob.Schedule();
            JobHandle _pathSaveJobHandle = _pathSaveJob.Schedule();
            JobHandle __recordSaveJobHandle = _recordSaveJob.Schedule();

            var _sceneDataList = SceneDataManager.Instance.SceneDataDic;
            NativeArray<FixedString4096Bytes> _sceneKeyArray = new NativeArray<FixedString4096Bytes>(_sceneDataList.Count, Allocator.TempJob);

            SceneSaveJob _sceneSaveJob = new SceneSaveJob();
            _sceneSaveJob.testArray = _sceneKeyArray;
            _sceneSaveJob.date = _fixedDate;

            ShopSaveJob _shopSaveJob = new ShopSaveJob();
            _shopSaveJob.date = _fixedDate;


            int _sceneIndex = 0;
            foreach (var _sceneData in _sceneDataList)
            {
                _sceneKeyArray[_sceneIndex++] = new FixedString128Bytes(_sceneData.Key);
            }

            JobHandle _sceneJobHandle = _sceneSaveJob.Schedule(_sceneDataList.Count, 10);
            JobHandle _shopJobHandle = _shopSaveJob.Schedule(ShopAllSO.shopSOList.Count, 5);
            StaticSave.Save<TutorialSaveData>(ref TutorialManager.Instance.tutorialSaveData, _date);


            ScreenCapture.CaptureScreenshot(_imagePath);


            StaticTime.EntierTime = 1;

            while (!_sceneJobHandle.IsCompleted)
            {
                yield return null;
            }
            _sceneJobHandle.Complete();
            //DisPose
            _sceneSaveJob.testArray.Dispose();

            sw.Stop();
            Debug.Log("Save: " + sw.ElapsedMilliseconds.ToString() + "ms");
        }

        [ContextMenu("Save")]
        public void Save(string _date)
		{
            if (InventorySO is null || QuestSaveDataSO is null || ShopAllSO is null || Player is null || StateModule is null)
			{
                return;
			}

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            StaticTime.EntierTime = 0;

            stateModule.SaveData();
            inventorySO.SaveData();
            questSaveDataSO.SaveData();

            StaticSave.Save<SaveData>(ref stateModule.saveData, _date);
            StaticSave.Save<InventorySave>(ref inventorySO.inventorySave, _date);
            StaticSave.Save<QuestSaveDataSave>(ref questSaveDataSO.questSaveDataSave, _date);
            StaticSave.Save<PathSave>(ref PathModeManager.Instance.pathSave, _date);
            StaticSave.Save<TutorialSaveData>(ref TutorialManager.Instance.tutorialSaveData, _date);
            var _sceneDataList = SceneDataManager.Instance.SceneDataDic;

            foreach(var _sceneData in _sceneDataList)
			{
                StaticSave.Save<ObjectDataList>(ref _sceneData.Value.objectDataList, _sceneData.Key + _date);
            }

            for (int i = 0; i < ShopAllSO.shopSOList.Count; ++i)
            {
                ShopSO _shopSO = ShopAllSO.shopSOList[i];
                _shopSO.SaveData();
                StaticSave.Save<ShopSave>(ref _shopSO.shopSave, _shopSO.shopName + _date);
            }

            string _imagePath = StaticSave.GetPath() + _date + ".png";
            ScreenCapture.CaptureScreenshot(_imagePath);

            SaveRecordDataList _saveRecordDataList = new SaveRecordDataList();
            StaticSave.Load<SaveRecordDataList>(ref _saveRecordDataList);

            SaveRecordData _saveRecordData = new SaveRecordData();
            _saveRecordData.date = _date;
            _saveRecordData.imagePath = _imagePath;
            _saveRecordDataList.dateList.Add(_saveRecordData);

            if(_saveRecordDataList.dateList.Count > 10)
			{
                var _data =  _saveRecordDataList.dateList[0];

                File.Delete(StaticSave.GetPath() + _data.date + ".png");
                File.Delete(StaticSave.GetPath() + "InventorySave" + _data.date);
                File.Delete(StaticSave.GetPath() + "QuestSaveDataSave" + _data.date);
                File.Delete(StaticSave.GetPath() + "PathSave" + _data.date);
                File.Delete(StaticSave.GetPath() + "TutorialSaveData" + _data.date);

                foreach (var _sceneData in _sceneDataList)
                {
                    File.Delete(StaticSave.GetPath() + _sceneData.Key + _data.date);
                }

                for (int i = 0; i < ShopAllSO.shopSOList.Count; ++i)
                {
                    ShopSO _shopSO = ShopAllSO.shopSOList[i];
                    File.Delete(StaticSave.GetPath() + _shopSO.shopName + _data.date);
                }
                
                _saveRecordDataList.dateList.RemoveAt(0);
            }

            StaticSave.Save<SaveRecordDataList>(ref _saveRecordDataList);


            StaticTime.EntierTime = 1;

            sw.Stop();
            Debug.Log(sw.ElapsedMilliseconds.ToString() + "ms");
        }

        [ContextMenu("Load")]
        public void Load(string _date)
        {
            isLoadSuccess = false;
            if (InventorySO is null || QuestSaveDataSO is null || ShopAllSO is null || Player is null)
            {
                isLoadSuccess = false;
                return;
            }
            //StaticTime.EntierTime = 0;
            StaticSave.Load<InventorySave>(ref inventorySO.inventorySave, _date);
            StaticSave.Load<QuestSaveDataSave>(ref questSaveDataSO.questSaveDataSave, _date);
            StaticSave.Load<PathSave>(ref PathModeManager.Instance.pathSave, _date);

            //별도의 적용 필요함
            Player.GetComponentInChildren<CharacterController>().enabled = false;
            //(Player.GetComponent<AbMainModule>() as Player).OnEnable();
            stateModule = (Player.GetComponent<AbMainModule>() as Player).GetModuleComponent<StatModule>(ModuleType.Stat);
            StaticSave.Load<SaveData>(ref stateModule.saveData, _date);
            stateModule.LoadData();
            inventorySO.LoadData();
            questSaveDataSO.LoadData();
            PathModeManager.Instance.SetPathList();
            QuestManager.Instance.LoadQuestSaveData(questSaveDataSO);
            Player.GetComponentInChildren<CharacterController>().enabled = true;
            //Player?.gameObject.SetActive(true);

            var _sceneDataList = SceneDataManager.Instance.SceneDataDic;

            foreach (var _sceneData in _sceneDataList)
            {
                _sceneData.Value.SaveUnLoad();
                StaticSave.Load<ObjectDataList>(ref _sceneData.Value.objectDataList, _sceneData.Key + _date);
                _sceneData.Value.SaveLoad();
            }

            for (int i = 0; i < ShopAllSO.shopSOList.Count; ++i)
            {
                ShopSO _shopSO = ShopAllSO.shopSOList[i];
                StaticSave.Load<ShopSave>(ref _shopSO.shopSave, _shopSO.shopName + _date);
                _shopSO.LoadData();
            }
            //StaticTime.EntierTime = 1;
            isLoadSuccess = true;
        }

#if UNITY_EDITOR
        public void Update()
		{
            if (Input.GetKeyDown(KeyCode.P))
			{
                testDate = DateTime.Now.ToString("yyyyMMddhhmmss");
                Save(testDate);
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                Load(testDate);
            }
        }

#endif
    }
}
