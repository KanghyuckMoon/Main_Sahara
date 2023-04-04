using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

namespace UI.Upgrade
{
    public class UpgradeReadyView : AbUI_Base
    {
        enum Elements
        {
            need_item_parent,
            weapon_need, 
            etc_need

        }
        enum Labels
        {
            select_item_label 
        }

        enum Buttons
        {
            upgrade_button
        }

        public override void Cashing()
        {
            base.Cashing();
        }

        public override void Init()
        {
            base.Init();
            BindVisualElements(typeof(Elements));
            BindLabels(typeof(Labels));
            BindButtons(typeof(Buttons));
        }
        
        /// <summary>
        /// 좌측 상단에 나타날 필요 아이템 목록 
        /// </summary>
        /// <param name="_v"></param>
        public void SetParentNeedItem(VisualElement _v)
        {
            GetVisualElement((int)Elements.need_item_parent).Add(_v);
        }

        /// <summary>
        /// 필요 아이템 슬롯들 초기화 
        /// </summary>
        public void ClearNeedItem()
        {
            foreach (var _v in GetVisualElement((int)Elements.weapon_need).Children())
            {
                _v.RemoveFromHierarchy();
            }
            foreach (var _v in GetVisualElement((int)Elements.etc_need).Children())
            {
                _v.RemoveFromHierarchy();
            }
        }
        
    }
}