using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using UnityEngine.UIElements;
using GoogleSpreadSheet;
using UI.Manager;
using UI.Inventory;
using Shop;
using Inventory;
using Utill.Addressable;
using UI.Popup; 

namespace UI.Shop
{
    public enum ShopType
    {
        BuyShop, //구매
        SellShop,   // 판매 

    }
    public class ShopPresenter : MonoBehaviour, IScreen
    {
        private UIDocument uiDocument;

        [SerializeField]
        private ShopView shopView;
        private InventoryGridSlotsPr inventoryGridSlotsPr;
        private InvenItemUISO invenItemUISO;

        private ShopPopupPr curShopPopupPr; 
        // 프로퍼티
        public IUIController UIController { get ; set; }

        public string name;
        [ContextMenu("캡쳐")]
        public void CaptureTest()
        {
            ScreenCapture.CaptureScreenshot(name);
        }
        private void OnEnable()
        {
            invenItemUISO = AddressablesManager.Instance.GetResource<InvenItemUISO>("InvenItemUISO");
            this.uiDocument = GetComponent<UIDocument>();
            shopView.InitUIDocument(uiDocument);
            shopView.Cashing();
            shopView.Init();

            // 인벤토리 슬롯들 뷰 생성 
            inventoryGridSlotsPr = new InventoryGridSlotsPr(shopView.ParentElement);
            // 슬롯 생성 
            inventoryGridSlotsPr.Init();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha5))
            {
                CaptureTest(); 
            }
        }

        private void UpdateMoneyText()
        {
            int _money = InventoryManager.Instance.GetMoney();
            shopView.SetMoneyLabel(_money);
        }

        /// <summary>
        /// 상점 아이템 세팅  
        /// </summary>
        private void SetShopItem()
        {
            inventoryGridSlotsPr.ClearSlotDatas();

            // 현재 상점npc의 판매 모든 판매아이템 데이터 가져오기 
            var _allDataList = ShopManager.Instance.GetAllItemData();
            foreach (var _data in _allDataList)
            {
                // 각 타입에 맞는 슬롯ui 설정 
                inventoryGridSlotsPr.InvenPanelDic[_data.itemType].SetItemDataUI(_data);
                var _slot = inventoryGridSlotsPr.InvenPanelDic[_data.itemType].GetCurSlot();
                _slot.SetItemData(_data);
                _slot.AddDoubleClicker(() => { AddSlotClickEvt(_slot, _data); });

                // 더블클릭시 구매 이벤트 추가 
                /*inventoryGridSlotsPr.InvenPanelDic[_data.itemType].SlotItemViewList.ForEach((x) =>
                {
                    // 팝업
                    //x.AddDoubleClicker(() => ShopManager.Instance.BuyItem(_data));
                    x.AddDoubleClicker(() => { AddSlotClickEvt(x, _data); });
                });*/
                // ShopManager.Instance.BuyItem();
                // ShopManager.Instance.SellItem();
            }
        }

        /// <summary>
        /// 슬롯 클릭시 이벤트
        /// 팝업창, 돈 업데이트 
        /// </summary>
        /// <param name="_slotPr"></param>
        /// <param name="_data"></param>
        private void AddSlotClickEvt(SlotItemPresenter _slotPr, ItemData _data)
        {
            if (curShopPopupPr != null) return;
            // 확인 팝업 생성 
            var _popupPr = PopupUIManager.Instance.CreatePopup<ShopPopupPr>(PopupType.Shop,_data);
            _popupPr.SetBuySell(_isBuy: true);
            _popupPr.AddClickEvent(
                // 확인 버튼 이벤트 
                () =>
                {
                    ShopManager.Instance.BuyItem(_slotPr.ItemData);
                    UpdateMoneyText();
                    _slotPr.UpdateUI();
                    curShopPopupPr = null; 
                },
                //  취소 버튼 이벤트 
                () => { curShopPopupPr = null; });
            curShopPopupPr = _popupPr; 
            // 돈 텍스트 업데이트 
            UpdateMoneyText() ;
        }

        /// <summary>
        /// 현재 인벤토리 판매 아이템 세팅 
        /// </summary>
        private void SetInvenItem()
        {
            inventoryGridSlotsPr.ClearSlotDatas();
            
            // 인벤토리 데이터 설정 
            List<ItemData> _itemList = InventoryManager.Instance.GetWeaponAndConsumptionList();
            if(_itemList.Count > 0)
            {
                foreach (var _data in _itemList)
                {
                    if (inventoryGridSlotsPr.InvenPanelDic[_data.itemType].CheckIndex() == false)
                    {
                        inventoryGridSlotsPr.CreateRow(invenItemUISO.GetItemUIType(_data.itemType));
                    }
                        
                    inventoryGridSlotsPr.InvenPanelDic[_data.itemType].SetItemDataUI(_data);
                    // 더블클릭시 판매 이벤트 추가 
                    inventoryGridSlotsPr.InvenPanelDic[_data.itemType].SlotItemViewList.ForEach((x) =>
                    {
                        x.AddDoubleClicker(() => ShopManager.Instance.SellItem(_data));
                    });
                }    
            }
            
        }

        [ContextMenu("구매")]
        public void TestBuyShop()
        {
            ActivetShop(ShopType.BuyShop);
        }
        [ContextMenu("판매")]
        public void TestSellShop()
        {
            ActivetShop(ShopType.SellShop);
        }

        /// <summary>   
        /// 상점 활성화 
        /// </summary>
        /// <param name="_shopType"></param>
        public bool ActivetShop(ShopType _shopType)
        {
            string _name = "";
            bool _isLeft = true; 
            switch (_shopType)
            {
                case ShopType.BuyShop:
                    {
                        SetShopItem(); 
                        _name = TextManager.Instance.GetText(UIManager.Instance.TextKeySO.FindKey(TextKeyType.shopBuy));
                        _isLeft = false;
                         break;
                    }
                case ShopType.SellShop:
                    {
                        SetInvenItem(); 
                        _name = TextManager.Instance.GetText(UIManager.Instance.TextKeySO.FindKey(TextKeyType.shopSell));
                        _isLeft = true;
                        break; 
                    }
            }
            this.shopView.SetShopName(_name);
            this.shopView.SetPanelDir(_isLeft); 
            this.shopView.ActiveScreen(true);
            UpdateMoneyText(); 
            
            return true; 
        }

        public bool ActiveView()
        {
            return shopView.ActiveScreen(); 
        }

        public void ActiveView(bool _isActive)
        {
            shopView.ActiveScreen(_isActive);
        }
    }

}
