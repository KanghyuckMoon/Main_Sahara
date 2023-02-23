using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using System; 
using System.Linq; 

namespace UI.Inventory
{
    [System.Serializable]
    public class InvenType
    {
        public InventoryGridSlotsView.InvenPanelElements uiType;
        public ItemType dataType; 

       public InvenType(InventoryGridSlotsView.InvenPanelElements _uiType, ItemType _dataType)
        {
            this.uiType = _uiType;
            this.dataType = _dataType; 
        }
    }

    /// <summary>
    /// �κ��丮�� ������UI enumŸ�԰� ������ ������ enum Ÿ���� ���� �����ִ� Ŭ����
    /// </summary>
    [CreateAssetMenu(menuName = "SO/UI/InvenItemSO")]
    public class InvenItemUISO : ScriptableObject
    {

        [SerializeField, Header("�ڵ����� List ä��ų�(enum ���� ���ƾ���)")]  
        private bool isAuto = false; 
        public List<InvenType> invenItemTypeList = new List<InvenType>();

        private void OnValidate()
        {
            if (isAuto == false) return; 
            // ������ ������ Ÿ�Կ� ���� ����Ʈ ���� 
            invenItemTypeList.Clear(); 
            foreach (var _dType in Enum.GetValues(typeof(ItemType)))
            {
                if ((ItemType)_dType == ItemType.None) return; 
                foreach(var _uiType in Enum.GetValues(typeof(InventoryGridSlotsView.InvenPanelElements)))
                {
                    // ���� �� ã�Ƽ� ������ ����Ʈ �߰� 
                    if(Enum.GetName(_dType.GetType(), _dType).ToLower() ==
                    Enum.GetName(_uiType.GetType(), _uiType).Replace("_panel", ""))
                    {
                        invenItemTypeList.Add(new InvenType((InventoryGridSlotsView.InvenPanelElements)_uiType, (ItemType)_dType));
                        break; 
                    }
                }
            }
        }
        public ItemType GetItemType(InventoryGridSlotsView.InvenPanelElements _uiType)
        {
            return invenItemTypeList.Where((x) => x.uiType == _uiType).Select(x => x.dataType).First(); 
        }

        public InventoryGridSlotsView.InvenPanelElements GetItemUIType(ItemType _itemType)
        {
            return invenItemTypeList.Where((x) => x.dataType == _itemType).Select(x => x.uiType).First();
        }
    }
}

