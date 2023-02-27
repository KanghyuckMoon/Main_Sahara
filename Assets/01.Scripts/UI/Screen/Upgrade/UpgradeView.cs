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
        public VisualElement SelectParent => GetVisualElement((int)Elements.select_active_panel); // Ŭ���� ������� ��� �ߴ� �� 
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
        /// ���� �θ� �� ���� 
        /// </summary>
        /// <param name="_v"></param>
        public void SetParentSlot(VisualElement _v)
        {
            GetVisualElement((int)Elements.slot_parent).Add(_v); 
        }

        /// <summary>
        /// ȭ�� Ŭ���� 
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
