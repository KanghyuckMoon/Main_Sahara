using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Inventory; 
using GoogleSpreadSheet;
using Utill.Addressable;
using Inventory; 
using System.Linq; 

namespace UI.Upgrade
{
    public class UpgradeReadyPr
    {
        private UpgradeReadyView upgradeReadyView;
        
        // 현재 선택한 아이템 데이터
        private ItemData curSelectedData; 
        
        // 필요한 아이템 데이터 리스트( 현재 UI상 활성화중인 리스트)  
        private List<UpgradeSlotPresenter> needItemDataList = new List<UpgradeSlotPresenter>();
        
        public UpgradeReadyPr(VisualElement _parent)
        {
            upgradeReadyView = new UpgradeReadyView(); 
            upgradeReadyView.InitUIParent(_parent);
            upgradeReadyView.Cashing();
            upgradeReadyView.Init();
            
            upgradeReadyView.SetUpgradeBtnCallback(() => 
            {
                ItemUpgradeManager.Instance.Upgrade(curSelectedData.key);
            });
        }

        /// <summary>
        /// 현재 선택한 아이템 데이터 설정 
        /// </summary>
        /// <param name="_curItemData"></param>
        public void SetCurSelectedItem(ItemData _curItemData)
        {
            this.curSelectedData = _curItemData; 
            upgradeReadyView.SetItemLabel(TextManager.Instance.GetText(_curItemData.nameKey));
            upgradeReadyView.SetImage(AddressablesManager.Instance.GetResource<Texture2D>(_curItemData.spriteKey));
        }
        
        /// <summary>
        ///  필요 아이템 리스트 설정 
        /// </summary>
        /// <param name="_itemDataList"></param>
        public void ActiveNeedItems(List<ItemData> _itemDataList)
        {
            foreach (var _data in _itemDataList)
            {
                UpgradeSlotPresenter _newUpgradePr = new UpgradeSlotPresenter(true);
                // 슬롯 애니메이션 
                _newUpgradePr.SetItemDataHave(_data);
                needItemDataList.Add(_newUpgradePr);

                // 슬롯 하나 활성화 
                if (_data.itemType == ItemType.Weapon)  
                {
                    this.upgradeReadyView.SetParentWeaponNeed(_newUpgradePr.Parent.ElementAt(0));
                }
                else
                {
                    this.upgradeReadyView.SetParentEtcNeed(_newUpgradePr.Parent.ElementAt(0));
                }
            }

        }

        /// <summary>
        /// 필요 아이템 슬롯 초기화(비활성화)
        /// </summary>
        public void ClearSlots()
        {
            // 슬롯 삭제 
            upgradeReadyView.ClearNeedItem();
            upgradeReadyView.SetItemLabel("");
            needItemDataList.Clear();
            upgradeReadyView.SetImage(null);
        }
        
        /// <summary>
        /// 합성 가능 여부 체크 
        /// </summary>
        public bool CheckCanUpgrade()
        {
            // 모든 재료가 있다면 합성가능 
            bool _isCan = needItemDataList.Count(x => x.IsEnough) == needItemDataList.Count; //합성 가능 여부 
            upgradeReadyView.ActiveUpgradeButton(_isCan);
            upgradeReadyView.UpgradeButton.pickingMode = _isCan ? PickingMode.Position : PickingMode.Ignore; 
            return _isCan; 
        }
    }
    
}
