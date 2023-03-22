using System.Collections;
using System.Collections.Generic;
using Codice.Client.Common;
using UI;
using UnityEngine;

namespace UI.Popup
{
    public class InteracftionPopupView : AbUI_Base
    {
        enum Elements
        {
            image 
        }

        enum Labels
        {
            name, 
            detail 
        }
        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
            BindLabels(typeof(Labels));
        }
    }
        
}
