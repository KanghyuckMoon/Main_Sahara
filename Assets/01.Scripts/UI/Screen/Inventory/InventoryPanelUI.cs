using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using UI.Production;
using Inventory;
using Utill.Addressable;


namespace UI.Inventory
{
    /// <summary>
    /// 인벤토리의 한 패널 뷰 관리 클래스
    /// </summary>
    public class InventoryPanelUI
    {
        public int index = 0; // 남은 칸의 위치 인덱스 
        public int addRow;
        public List<SlotItemView> slotItemViewList = new List<SlotItemView>();
        public List<SlotItemView> equipItemViewList = new List<SlotItemView>(); 

        public InventoryPanelUI()
        {

        }

        public void AddSlotView(SlotItemView _slotItemView)
        {
            this.slotItemViewList.Add(_slotItemView);
        }

        // 매니저에서 데이터 받아와서 
        public void AddEquipSlotView(SlotItemView _slotItemView)
        {
            this.equipItemViewList.Add(_slotItemView);
        }

        public void RemoveSlotView()
        {
            // _col 만큼 삭제 
            int a = ((slotItemViewList.Count - index) / 4) * 4;
            // 20 - 15  5 / 4  
            int _count = slotItemViewList.Count;
            for (int i = 0; i < a * 4; i++) // 4를 변수로 빼야해 (나중에 생성자로 받아오기) 
            {
                this.slotItemViewList.ElementAt(_count - i).RemoveView();
                this.slotItemViewList.RemoveAt(_count - i);
            }
        }

        /// <summary>
        /// 슬롯UI 에 ItemData 추가해서 UI 변경 
        /// </summary>
        public void SetItemDataUI(ItemData _itemData)
        {
            SlotItemView _slotView = slotItemViewList[index];
            _slotView.IsStackable = _itemData.stackble;
            if (_slotView.IsStackable == true)
            {
                _slotView.SetText(_itemData.count);
            }
            _slotView.SetSprite(AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey));
            ++index;
        }


        /// <summary>
        /// 슬롯UI 에 ItemData 추가해서 UI 변경 
        /// </summary>
        public void SetEquipItemDataUI(ItemData _itemData, int _index)
        {
            SlotItemView _slotView = equipItemViewList[_index];
            _slotView.IsStackable = _itemData.stackble;
            if (_slotView.IsStackable == true)
            {
                _slotView.SetText(_itemData.count);
            }
            if(_itemData.spriteKey != "")
            {
                _slotView.SetSprite(AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey));
            }
        }

    }


}
