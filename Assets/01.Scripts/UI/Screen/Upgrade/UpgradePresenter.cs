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

        /// <summary>
        ///  아이템 트리 UI 생성 및 데이터 넣기 
        /// </summary>
        public void CreateItemTree()
        {
            ItemUpgradeDataSO _itemUpgradeData = ItemUpgradeManager.Instance.GetItemUpgradeDataSO("UItem1");

        }
        [ContextMenu("생성 테스트")]
        public void CreateTest()
        {
            UpgradeSlotPresenter _upgradePr = new UpgradeSlotPresenter(); 
            this.upgradeView.SetParentSlot(_upgradePr.Parent);
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

