using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;
using System;
using Inventory;
using GoogleSpreadSheet; 

namespace UI.Upgrade
{
    public class UpgradeCtrlPresenter
    {
        private UpgradeCtrlView upgradeCtrlView; 

        private FinalItemDataListSO finalListSO; // 최종 템 데이터 리스트SO
        private int curIdx; // 현재 활성화 중인 트리 인덱스 

        private Action<ItemDataSO> callback; // 현재 대장장이UI 변경시 나타낼 호출할 함수( 아이템 트리 표시 ) 

        // 프로퍼티 
        private ItemDataSO CurDataSO => finalListSO.itemList[curIdx]; 
        public UpgradeCtrlPresenter(VisualElement _parent, Action<ItemDataSO> _callback)
        {
            // SO가져오기 
            finalListSO = AddressablesManager.Instance.GetResource<FinalItemDataListSO>("FinalItemDataListSO");
            curIdx = (int) finalListSO.itemList.Count / 2;
            // 뷰 초기화 
            upgradeCtrlView = new UpgradeCtrlView(); 
            upgradeCtrlView.InitUIParent(_parent);
            upgradeCtrlView.Cashing();
            upgradeCtrlView.Init();
            // 함수 등로
            this.callback = _callback;
            // 버튼 이벤트 등록
            AddButtonsEvent();

        }

        [ContextMenu("업데이트")]
        /// <summary>
        /// 라벨, 버튼 UI 업데이트
        /// </summary>
        public void UpdateUI()
        {
            string _name = GetName(CurDataSO.nameKey);
            this.upgradeCtrlView.SetLabel(PosType.mid, _name);

            // 왼쪽 설정 
            if (curIdx - 1 < 0)
            {
                // 라벨,버튼 비활성화 
                this.upgradeCtrlView.InActiveLabel(PosType.left);
                this.upgradeCtrlView.ActiveButton(_isLeft: true, false);
            }
            else
            {
                _name = GetName(finalListSO.itemList[curIdx - 1].nameKey); 
                this.upgradeCtrlView.SetLabel(PosType.left, _name); 
                this.upgradeCtrlView.ActiveButton(_isLeft: true, true);
            }

            // 오른쪽 설정 
            if (curIdx +1 > finalListSO.itemList.Count-1)
            {
                // 라벨,버튼 비활성화 
                this.upgradeCtrlView.InActiveLabel(PosType.right);
                this.upgradeCtrlView.ActiveButton(_isLeft: false, false);
            }
            else
            {
                _name = GetName(finalListSO.itemList[curIdx + 1].nameKey);
                this.upgradeCtrlView.SetLabel(PosType.right, _name);
                this.upgradeCtrlView.ActiveButton(_isLeft: false, true);
            }
            callback?.Invoke(CurDataSO);

        }

        private void AddButtonsEvent()
        {
            this.upgradeCtrlView.AddButtonEvent(_isLeft:true, () =>
             {
                 --curIdx;
                 callback?.Invoke(CurDataSO);
             });
            this.upgradeCtrlView.AddButtonEvent(_isLeft: false, () =>
            {
                ++curIdx;
                callback?.Invoke(CurDataSO);
            });
        }

        private string GetName(string _key)
        {
            return TextManager.Instance.GetText(_key);
        }
    }

}
