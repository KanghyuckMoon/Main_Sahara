using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;
using Inventory;
using Utill.Addressable;
using Utill.Measurement;
using Utill.Pattern;
using UI.ConstructorManager;
using UI.Production; 

namespace UI.Upgrade
{
    public class UpgradePresenter : MonoBehaviour, IScreen 
    {
        private UIDocument uiDocument;

        [SerializeField]
        private UpgradeView upgradeView; 
        private void OnEnable()
        {
            uiDocument = GetComponent<UIDocument>();
            upgradeView.InitUIDocument(uiDocument);
            upgradeView.Cashing();
            upgradeView.Init(); 
        }

        [ContextMenu("생성 테스트")]
        public void CreateTest()
        {
            this.upgradeView.SetParentSlot(UIConstructorManager.Instance.GetProductionUI(typeof(UpgradeSlotView)).Item1);
        }
        public bool ActiveView()
        {
            return upgradeView.ActiveScreen(); 
        }

        public void ActiveView(bool _isActive)
        {
            upgradeView.ActiveScreen(_isActive); 
        }
    }
}

