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
            upgrade,
            select_active_panel,
        }

        public VisualElement UpgradePickParent => GetVisualElement((int)Elements.upgrade);
        public VisualElement SelectParent => GetVisualElement((int)Elements.select_active_panel); // 클릭시 사방으로 재료 뜨는 거 
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

    }
}
