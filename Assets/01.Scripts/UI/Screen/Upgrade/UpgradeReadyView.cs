using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 
using UI.UtilManager;
using Inventory; 
using UI.Base;

namespace UI.Upgrade
{
    public class UpgradeReadyView : AbUI_Base
    {
        enum Elements
        {
            need_item_parent,
            weapon_need, 
            etc_need,
            select_image

        }
        enum Labels
        {
            select_item_label 
        }

        enum Buttons
        {
            upgrade_button
        }

        private const string inactiveStr = "inactive";

        public Button UpgradeButton => GetButton((int)Buttons.upgrade_button);
        public override void Cashing()
        {
            //base.Cashing();
            BindVisualElements(typeof(Elements));
            BindLabels(typeof(Labels));
            BindButtons(typeof(Buttons));
        }

        public override void Init()
        {
            base.Init();
        }

        public void SetImage(Texture2D _image)
        {
            GetVisualElement((int)Elements.select_image).style.backgroundImage = _image; 
        }
        
        /// <summary>
        /// 현재 선택한 라벨 설정
        /// </summary>
        /// <param name="_name"></param>
        public void SetItemLabel(string _name)
        {
            Label _itemLabel = GetLabel((int)Labels.select_item_label); 
            UIUtilManager.Instance.AnimateText(_itemLabel,_name);
        }
        
        public void ActiveUpgradeButton(bool _isActive)
        {
            if (_isActive == false)
            {
                GetButton((int)Buttons.upgrade_button).AddToClassList(inactiveStr);
                return; 
            }
            GetButton((int)Buttons.upgrade_button).RemoveFromClassList(inactiveStr);
        }
        
        /// <summary>
        /// 좌측 상단에 나타날 무기 필요 아이템 목록을 부모로 
        /// </summary>
        /// <param name="_v"></param>
        public void SetParentWeaponNeed(VisualElement _v)
        {
            GetVisualElement((int)Elements.weapon_need).Add(_v);
        }
        
        /// <summary>
        /// 좌측 상단에 나타날 무기 외 필요 아이템 목록을 부모로 
        /// </summary>
        /// <param name="_v"></param>
        public void SetParentEtcNeed(VisualElement _v)
        {
            GetVisualElement((int)Elements.etc_need).Add(_v);
        }

        /// <summary>
        /// 필요 아이템 슬롯들 초기화 
        /// </summary>
        public void ClearNeedItem()
        {
            var _weaponNeedList = GetVisualElement((int)Elements.weapon_need).Children();
            var _etcNeedList = GetVisualElement((int)Elements.etc_need).Children();
            
            GetVisualElement((int)Elements.weapon_need).Clear();
            GetVisualElement((int)Elements.etc_need).Clear();
            foreach (var _v in _weaponNeedList)
            {
            //    _v.RemoveFromHierarchy();
            }
            foreach (var _v in _etcNeedList)
            {
             //   _v.RemoveFromHierarchy();
            }
        }
        
    }
}