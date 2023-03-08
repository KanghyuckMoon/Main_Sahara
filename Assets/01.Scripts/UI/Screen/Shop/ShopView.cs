using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements; 

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
        /// ���� �̸� ���� 
        /// </summary>
        /// <param name="_name"></param>
        public void SetShopName(string _name)
        {
            GetLabel((int)Labels.shop_name).text = _name; 
        }

        /// <summary>
        /// ����â�� ���� ���� ǥ�� ���� ���� 
        /// </summary>
        /// <param name="_isInvenLeft">�κ��丮â�� ����</param>
        public void SetPanelDir(bool _isInvenLeft)
        {
            GetVisualElement((int)Elements.content).style.flexDirection = _isInvenLeft ? FlexDirection.Row : FlexDirection.RowReverse; 
        }

    }

}
