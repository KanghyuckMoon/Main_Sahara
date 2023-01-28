using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 
namespace UI
{
    [Serializable]
    public class InventoryPresenter : MonoBehaviour, IScreen
    {
        [SerializeField]
        private UIDocument uiDocument;

        [SerializeField]
        private InventoryView inventoryView;

        private void Awake()
        {
            uiDocument ??= GetComponent<UIDocument>(); 

            inventoryView.InitUIDocument(uiDocument);
            inventoryView.Cashing(); 
        }
        void Start()
        {
            inventoryView.Init(); 
        }
        
        public void ActiveView()
        {
            inventoryView.ActiveScreen(); 
        }
    }

}

