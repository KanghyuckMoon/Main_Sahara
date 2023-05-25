using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;

namespace UI.Popup
{
    public class InteracftionPopupView : AbUI_Base
    {
        enum Elements
        {
            interaction_popup,
           // image, 
        }

        enum Labels
        {
            detail_label,
            type_label
            
        }
        
        // 프로퍼티 
        public VisualElement InteractionParent => GetVisualElement((int)Elements.interaction_popup);
        public override void Cashing()
        {
            //base.Cashing();
            BindVisualElements(typeof(Elements));
            BindLabels(typeof(Labels));
        }

        public void SetDetail(string _str)
        {
            GetLabel((int)Labels.detail_label).text = _str; 
        }

        public void SetType(string _str)
        {
            GetLabel((int)Labels.type_label).text = _str; 

        }
    }
        
}
