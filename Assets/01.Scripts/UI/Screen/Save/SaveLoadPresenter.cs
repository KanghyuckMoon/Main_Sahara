using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base; 

namespace UI.Save
{
    public class SaveLoadPresenter : MonoBehaviour,IScreen
    {
        private UIDocument uiDocument; 
        [SerializeField]
        private SaveLoadView saveLoadView;

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

        public bool ActiveView()
        {
            return saveLoadView.ActiveScreen(); 
        }
    
        public void ActiveView(bool _isActive)
        {
            saveLoadView.ActiveScreen(_isActive);
        }
    }

}
