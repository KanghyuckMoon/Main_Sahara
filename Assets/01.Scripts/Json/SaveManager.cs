using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using Quest;
using Inventory;
using Utill.Addressable;
using Shop;
using Module;
using CondinedModule;
using TimeManager;

namespace Json
{
    public class SaveManager : MonoSingleton<SaveManager>
    {
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
                if (stateModule is null)
				{
                    AbMainModule _abMainModule = player?.GetComponentInChildren<AbMainModule>();
                    stateModule = _abMainModule?.GetModuleComponent<StatModule>(ModuleType.Stat);

                }
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

        private StatModule stateModule;

        [ContextMenu("Save")]
        public void Save()
		{
            if (InventorySO is null || QuestSaveDataSO is null || ShopAllSO is null || Player is null || StateModule is null)
			{
                return;
			}

            StaticTime.EntierTime = 0;

            stateModule.SaveData();
            inventorySO.SaveData();
            questSaveDataSO.SaveData();
            
            StaticSave.Save<SaveData>(ref stateModule.saveData);
            StaticSave.Save<InventorySave>(ref inventorySO.inventorySave);
            StaticSave.Save<QuestSaveDataSave>(ref questSaveDataSO.questSaveDataSave);

            for (int i = 0; i < ShopAllSO.shopSOList.Count; ++i)
            {
                ShopSO _shopSO = ShopAllSO.shopSOList[i];
                _shopSO.SaveData();
                StaticSave.Save<ShopSave>(ref _shopSO.shopSave, _shopSO.shopName);
            }
            StaticTime.EntierTime = 1;
        }

        [ContextMenu("Load")]
        public void Load()
        {
            if (InventorySO is null || QuestSaveDataSO is null || ShopAllSO is null || Player is null || StateModule is null)
            {
                return;
            }
            Player?.gameObject.SetActive(false);
            StaticTime.EntierTime = 0;
            StaticSave.Load<SaveData>(ref stateModule.saveData);
            StaticSave.Load<InventorySave>(ref inventorySO.inventorySave);
            StaticSave.Load<QuestSaveDataSave>(ref questSaveDataSO.questSaveDataSave);

            //별도의 적용 필요함
            stateModule.LoadData();
            Player?.gameObject.SetActive(true);
            inventorySO.LoadData();
            questSaveDataSO.LoadData();
            QuestManager.Instance.LoadQuestSaveData(questSaveDataSO);

            for (int i = 0; i < ShopAllSO.shopSOList.Count; ++i)
            {
                ShopSO _shopSO = ShopAllSO.shopSOList[i];
                StaticSave.Load<ShopSave>(ref _shopSO.shopSave, _shopSO.shopName);
                _shopSO.LoadData();
            }
            StaticTime.EntierTime = 1;
        }

#if UNITY_EDITOR
        public void Update()
		{
            if (Input.GetKeyDown(KeyCode.P))
			{
                Save();
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                Load();
            }
        }

#endif
    }
}
