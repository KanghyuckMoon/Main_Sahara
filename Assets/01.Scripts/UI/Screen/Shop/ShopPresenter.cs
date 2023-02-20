using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using UnityEngine.UIElements;
using GoogleSpreadSheet;
using UI.Manager; 

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

        // 프로퍼티
        public IUIController UIController { get ; set; }

        private void OnEnable()
        {
            this.uiDocument = GetComponent<UIDocument>();
            shopView.InitUIDocument(uiDocument);
            shopView.Cashing();
            shopView.Init(); 
        }

        /// <summary>
        /// 상점 활성화 
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
                        _name = TextManager.Instance.GetText(UIManager.Instance.TextKeySO.FindKey(TextKeyType.shopBuy));
                        _isLeft = false;
                         break;
                    }
                case ShopType.SellShop:
                    {
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
