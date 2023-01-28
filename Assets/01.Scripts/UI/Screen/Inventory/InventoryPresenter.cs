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
        private Camera inventoryCam; 

        [SerializeField]
        private InventoryView inventoryView;

        private void Awake()
        {
            uiDocument ??= GetComponent<UIDocument>(); 

            inventoryView.InitUIDocument(uiDocument);
        }
        private void OnEnable()
        {
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

        public void ActiveView(bool _isActive)
        {
            inventoryCam.gameObject.SetActive(_isActive); // �κ��丮 Ȱ��ȭ�ÿ��� ī�޶� Ȱ��ȭ 
            inventoryView.ActiveScreen(_isActive);
        }
    }

}

