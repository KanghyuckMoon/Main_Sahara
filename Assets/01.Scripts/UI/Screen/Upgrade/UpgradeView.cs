using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Upgrade
{
    [System.Serializable]
    public class UpgradeView : AbUI_Base
    {
        enum Elements
        {
            slot_parent,
            upgrade_pick,
            select_active_panel,
            move_screen
        }

        public VisualElement Parent => parentElement; 
        public VisualElement UpgradePickParent => GetVisualElement((int)Elements.upgrade_pick);
        public VisualElement SelectParent => GetVisualElement((int)Elements.select_active_panel); // 클릭시 사방으로 재료 뜨는 거 
        public VisualElement MoveScreen => GetVisualElement((int)Elements.move_screen); 
        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
        }

        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// 슬롯 부모 로 설정 
        /// </summary>
        /// <param name="_v"></param>
        public void SetParentSlot(VisualElement _v)
        {
            GetVisualElement((int)Elements.slot_parent).Add(_v); 
        }

        /// <summary>
        /// 화면 클리어 
        /// </summary>
        public void ClearAllSlots()
        {
            if (GetVisualElement((int)Elements.slot_parent).childCount == 0) return;

            List<VisualElement> _removeList = new List<VisualElement>(); 
            var _list = GetVisualElement((int)Elements.slot_parent).Children();
            foreach (var _v in _list)
            {
                _removeList.Add(_v); 
            }

            foreach(var _v in _removeList)
            {
                _v.RemoveFromHierarchy(); 
            }

        }

    }
}
