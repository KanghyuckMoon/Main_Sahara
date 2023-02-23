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
    /// 인벤토리의 아이템UI enum타입과 아이템 데이터 enum 타입을 연결 시켜주는 클래스
    /// </summary>
    [CreateAssetMenu(menuName = "SO/UI/InvenItemSO")]
    public class InvenItemUISO : ScriptableObject
    {

        [SerializeField, Header("자동으로 List 채울거냐(enum 문자 같아야함)")]  
        private bool isAuto = false; 
        public List<InvenType> invenItemTypeList = new List<InvenType>();

        private void OnValidate()
        {
            if (isAuto == false) return; 
            // 아이템 데이터 타입에 따라 리스트 생성 
            invenItemTypeList.Clear(); 
            foreach (var _dType in Enum.GetValues(typeof(ItemType)))
            {
                if ((ItemType)_dType == ItemType.None) return; 
                foreach(var _uiType in Enum.GetValues(typeof(InventoryGridSlotsView.InvenPanelElements)))
                {
                    // 같은 거 찾아서 맞으면 리스트 추가 
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

