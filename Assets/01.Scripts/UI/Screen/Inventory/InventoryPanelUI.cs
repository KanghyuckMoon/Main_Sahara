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
    /// �κ��丮�� �� �г� �� ���� Ŭ����
    /// </summary>
    public class InventoryPanelUI
    {
        public int index = 0; // ���� ĭ�� ��ġ �ε��� 
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

        // �Ŵ������� ������ �޾ƿͼ� 
        public void AddEquipSlotView(SlotItemView _slotItemView)
        {
            this.equipItemViewList.Add(_slotItemView);
        }

        public void RemoveSlotView()
        {
            // _col ��ŭ ���� 
            int a = ((slotItemViewList.Count - index) / 4) * 4;
            // 20 - 15  5 / 4  
            int _count = slotItemViewList.Count;
            for (int i = 0; i < a * 4; i++) // 4�� ������ ������ (���߿� �����ڷ� �޾ƿ���) 
            {
                this.slotItemViewList.ElementAt(_count - i).RemoveView();
                this.slotItemViewList.RemoveAt(_count - i);
            }
        }

        /// <summary>
        /// ����UI �� ItemData �߰��ؼ� UI ���� 
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
        /// ����UI �� ItemData �߰��ؼ� UI ���� 
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
