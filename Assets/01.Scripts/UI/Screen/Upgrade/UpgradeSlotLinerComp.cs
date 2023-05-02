using System.Collections.Generic;
using System.Linq;
using Inventory;
using UI.Canvas;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using Utill.Addressable;

namespace UI.Upgrade
{
    public class UpgradeSlotLinerComp
    {
        private LineDataSO lineDataSO; 
        private List<UpgradeSlotPresenter> allItemList = new List<UpgradeSlotPresenter>(); 
        private Dictionary<VisualElement, MapLiner> linerDic = new Dictionary<VisualElement, MapLiner>();

        // 프로퍼티 
        private LineDataSO LineDataSO
        {
            get
            {
                if (lineDataSO is null)
                {
                    lineDataSO =AddressablesManager.Instance.GetResource<LineDataSO>("UpgradeLineDataSO");
                }

                return lineDataSO; 
            }
        }

        private LineData defaultLineData => LineDataSO.GetLineData(LineType.UpgradeDefault);
        private LineData activeLineData => LineDataSO.GetLineData(LineType.UpgradeActive);
        public UpgradeSlotLinerComp()
        {
            lineDataSO = AddressablesManager.Instance.GetResource<LineDataSO>("UpgradeLineDataSO");
        }
        public void InitAllItemList(List<UpgradeSlotPresenter> _alllItemList)
        {
            this.allItemList = _alllItemList;
        }
        
        public void AddSlotAndLiner(VisualElement _element, MapLiner _liner)
        {
            linerDic.Add(_element, _liner);
        }

        public void ClearSlots()
        {
            linerDic.Clear();
        }

        
        /// <summary>
        ///  합성 가능한 아이템 체크 후 데이터 설정 
        /// </summary>
        public void CheckCreateItem()
        {
            foreach (var _slot in allItemList)
            {
                ItemUpgradeDataSO _childItemData = ItemUpgradeManager.Instance.GetItemUpgradeDataSO(_slot.ItemData.key);
                if (_childItemData == null) continue; // 재료템이 존재하지 않으면 리턴 
                
                // 모든 재료 아이템을 가지고 있는가  
                bool _isHaveAll = true; 
                List<ItemData> _dataList = ItemUpgradeManager.Instance.UpgradeItemSlotList(_slot.ItemData.key);
                foreach (var _data in _dataList.Where(_data => InventoryManager.Instance.GetItem(_slot.ItemData.key).count < _data.count))
                {
                    _isHaveAll = false;
                }

                // 라인 설정 ( 색, material) 
                LineData _lineData = _isHaveAll ? activeLineData : defaultLineData; 
                SetLineData(_slot, _lineData);
            }
            
        }

        /// <summary>
        /// 선 관련 설정 (색, material) 
        /// </summary>
        /// <param name="_slot"></param>
        /// <param name="_data"></param>
        private void SetLineData(UpgradeSlotPresenter _slot,LineData _data)
        {
            foreach (var _line in linerDic
                         .Where(_line => _line.Key.Equals(_slot.Element1) is true))
            {
                _line.Value.SetColor(_data.color);
                _line.Value.SetMaterial(_data.material);
            }
        }
    }
}