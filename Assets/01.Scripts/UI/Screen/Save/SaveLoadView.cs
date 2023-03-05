using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace UI.Save
{
    [System.Serializable]
    public class SaveLoadView : AbUI_Base
    {
        enum Elements
        {
            save_entry_parent
        }
        enum Labels
        {
            title
        }

        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
            BindLabels(typeof(Labels));
        }

        public override void Init()
        {
            base.Init();
        }

        public void SetParent(VisualElement _v)
        {
            GetVisualElement((int)Elements.save_entry_parent).Add(_v); 
        }

        public void RemoveFromView(VisualElement _v)
        {
            GetVisualElement((int)Elements.save_entry_parent).Remove(_v); 
        }
    }
}

