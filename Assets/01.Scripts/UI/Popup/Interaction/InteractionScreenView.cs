using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;

namespace UI.Popup
{
    [System.Serializable]
    public class InteractionScreenView : AbUI_Base
    {
        enum Elements
        {
            interaction_parent, 
            
        }
        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
        }

        public override void Init()
        {
            base.Init();
        }

        public void SetParent(VisualElement _v)
        {
            GetVisualElement((int)Elements.interaction_parent).Add(_v);
        }
    }
}
