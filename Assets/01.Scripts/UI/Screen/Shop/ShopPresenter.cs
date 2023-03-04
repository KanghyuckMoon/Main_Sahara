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

namespace UI.Shop
{
    public enum ShopType
    {
        BuyShop, //����
        SellShop,   // �Ǹ� 

    }
    public class ShopPresenter : MonoBehaviour, IScreen
    {
        private UIDocument uiDocument;

        [SerializeField]
        private ShopView shopView;
        private InventoryGridSlotsPr inventoryGridSlotsPr;

        // ������Ƽ
        public IUIController UIController { get ; set; }

        public string name;
        [ContextMenu("ĸ��")]
        public void CaptureTest()
        {
            ScreenCapture.CaptureScreenshot(name);
        }
        private void OnEnable()
        {
            this.uiDocument = GetComponent<UIDocument>();
            shopView.InitUIDocument(uiDocument);
            shopView.Cashing();
            shopView.Init();

            // �κ��丮 ���Ե� �� ���� 
            inventoryGridSlotsPr = new InventoryGridSlotsPr(shopView.ParentElement);
            // ���� ���� 
            inventoryGridSlotsPr.Init();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha5))
            {
                CaptureTest(); 
            }
        }
        /// <summary>
        /// ���� ������ ����  
        /// </summary>
        private void SetShopItem()
        {
            inventoryGridSlotsPr.ClearSlotDatas(); 

            var _allDataList = ShopManager.Instance.GetAllItemData();
            foreach(var _data in _allDataList)
            {
                inventoryGridSlotsPr.ItemSlotDic[_data.itemType].SetItemDataUI(_data);
                // ����Ŭ���� ���� �̺�Ʈ �߰� 
                inventoryGridSlotsPr.ItemSlotDic[_data.itemType].SlotItemViewList.ForEach((x) =>
                {
                    x.AddDoubleClicker(() => ShopManager.Instance.BuyItem(_data));
                });
            }
            //   ShopManager.Instance.BuyItem();
         //   ShopManager.Instance.SellItem();
        }

        /// <summary>
        /// ���� �κ��丮 ������ ���� 
        /// </summary>
        private void SetInvenItem()
        {
            inventoryGridSlotsPr.ClearSlotDatas();
            
            // �κ��丮 ������ ���� 
            List<ItemData> _itemList = InventoryManager.Instance.GetWeaponAndConsumptionList();
            foreach (var _data in _itemList)
            {
                inventoryGridSlotsPr.ItemSlotDic[_data.itemType].SetItemDataUI(_data);
                // ����Ŭ���� �Ǹ� �̺�Ʈ �߰� 
                inventoryGridSlotsPr.ItemSlotDic[_data.itemType].SlotItemViewList.ForEach((x) =>
                {
                    x.AddDoubleClicker(() => ShopManager.Instance.SellItem(_data));
                });
            }
        }

        [ContextMenu("����")]
        public void TestBuyShop()
        {
            ActivetShop(ShopType.BuyShop);
        }
        [ContextMenu("�Ǹ�")]
        public void TestSellShop()
        {
            ActivetShop(ShopType.SellShop);
        }

        /// <summary>
        /// ���� Ȱ��ȭ 
        /// </summary>
        /// <param name="_shopType"></param>
        public void ActivetShop(ShopType _shopType)
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
