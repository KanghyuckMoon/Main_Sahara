using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;
using Json;
using UnityEngine.SceneManagement;
using System;

namespace UI.Save
{
    public class SaveLoadPresenter : MonoBehaviour,IScreen
    {
        private UIDocument uiDocument; 
        [SerializeField]
        private SaveLoadView saveLoadView;

        private List<SaveEntryPresenter> entryList = new List<SaveEntryPresenter>(); 
        
        private Action onActiveScreenEvt = null;

        // 프로퍼티 
        public Action OnActiveScreen
        {
            get => onActiveScreenEvt;
            set => onActiveScreenEvt = value;
        }
        
        public IUIController UIController { get ; set; }

        private void Awake()
        {
            uiDocument = GetComponent<UIDocument>(); 
        }

        private void OnEnable()
        {
            saveLoadView.InitUIDocument(uiDocument); 
            saveLoadView.Cashing();
            saveLoadView.Init(); 
        }

        [ContextMenu("테스트")]
        public void UpdateUI()
        {
            ClearSaveEntries(); 

            var _list = SaveManager.Instance.GetSaveRecordDataList(); 
            foreach(var _v in _list.dateList)
            {
                SaveEntryPresenter _entry = new SaveEntryPresenter();
                _entry.SetStrData(_v.imagePath, _v.date);
                _entry.AddClickEvent(() =>
                {
                    Debug.Log("클릭");
                    Load(_v.date); 
                });
                saveLoadView.SetParent(_entry.Parent);
                entryList.Add(_entry);
            }
        }
        [ContextMenu("활성화")]
        public bool ActiveView()
        {
            UpdateUI(); 
            return saveLoadView.ActiveScreen(); 
        }
    
        public void ActiveView(bool _isActive)
        {
            UpdateUI(); 
            saveLoadView.ActiveScreen(_isActive);
        }
    
        private void ClearSaveEntries()
        {
            if (entryList.Count == 0) return; 
            
            foreach(var _entry in entryList)
            {
                _entry.Parent.RemoveFromHierarchy();
            }
            entryList.Clear(); 
        }

        private void Load(string _data)
        {
            SaveManager.Instance.TestDate = _data;
            SceneManager.LoadScene("SaveLoadScene");

        }
    }

}
