using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements; 
using UI.Base;

namespace UI.Shop
{

    [Serializable]
    public class ShopView : AbUI_Base
    {
        enum Labels
        {
            shop_name,

        }

        enum Elements
        {
            content, 
              
        }

        public override void Cashing()
        {
            base.Cashing();
            BindLabels(typeof(Labels));
            BindVisualElements(typeof(Elements));
        }

        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// 상점 이름 설정 
        /// </summary>
        /// <param name="_name"></param>
        public void SetShopName(string _name)
        {
            GetLabel((int)Labels.shop_name).text = _name; 
        }

        /// <summary>
        /// 상점창과 상점 주인 표시 순서 설정 
        /// </summary>
        /// <param name="_isInvenLeft">인벤토리창이 왼쪽</param>
        public void SetPanelDir(bool _isInvenLeft)
        {
            GetVisualElement((int)Elements.content).style.flexDirection = _isInvenLeft ? FlexDirection.Row : FlexDirection.RowReverse; 
        }

    }

}
