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
        public List<SlotItemPresenter> slotItemViewList = new List<SlotItemPresenter>();
        public List<SlotItemPresenter> equipItemViewList = new List<SlotItemPresenter>();

        private VisualElement parent;

        // ������Ƽ 
        public VisualElement Parent => parent;
        
        public InventoryPanelUI()
        {

        }

        public InventoryPanelUI(VisualElement _parent)
        {
            this.parent = _parent; 
        }

        public void ClearDatas()
        {
            foreach(var _data in slotItemViewList)
            {
                _data.ClearData(); 
            }
        }
        public void AddSlotView(SlotItemPresenter _slotItemView)
        {
            this.slotItemViewList.Add(_slotItemView);
        }

        // �Ŵ������� ������ �޾ƿͼ� 
        public void AddEquipSlotView(SlotItemPresenter _slotItemView)
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
            SlotItemPresenter _slotView = slotItemViewList[index];
            _slotView.SetItemData(_itemData); 
            ++index;
        }

        /// <summary>
        /// ����UI �� ItemData �߰��ؼ� UI ���� 
        /// </summary>
        public void SetItemDataUI(ItemData _itemData, int _index)
        {
            SlotItemPresenter _slotView = slotItemViewList[_index];
            _slotView.SetItemData(_itemData);
        }


        /// <summary>
        /// ����UI �� ItemData �߰��ؼ� UI ���� 
        /// </summary>
        public void SetEquipItemDataUI(ItemData _itemData, int _index)
        {
            SlotItemPresenter _slotView = equipItemViewList[_index];
            _slotView.SetItemData(_itemData);
        }

    }


}
