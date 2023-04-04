
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Inventory; 

namespace UI.Upgrade
{
    public class UpgradeReadyPr : MonoBehaviour
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
        }

        /// <summary>
        /// 현재 선택한 아이템 데이터 설정 
        /// </summary>
        /// <param name="_curItemData"></param>
        public void SetCurSelectedItem(ItemData _curItemData)
        {
            this.curSelectedData = _curItemData; 
        }
        
        /// <summary>
        ///  필요 아이템 리스트 설정 
        /// </summary>
        /// <param name="_itemDataList"></param>
        public void ActiveNeedItems(List<ItemData> _itemDataList)
        {
            foreach (var _data in _itemDataList)
            {
                UpgradeSlotPresenter _newUpgradePr = new UpgradeSlotPresenter();
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
            needItemDataList.Clear();
        }

        public void CheckCanUpgrade()
        {
            
        }
    }
    
}
