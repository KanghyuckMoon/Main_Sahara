using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;

namespace UI.Production
{
    public class ShopPopupView : AbUI_Base
    {
        enum Elements
        {
            
        }

        enum Labels
        {
            title_label, 
            //detail_label 
        }

        enum Buttons
        {
            accept_btn, 
            cancel_btn, 
        }

        public override void Cashing()
        {
            //base.Cashing();
            BindVisualElements(typeof(Elements));
            BindLabels(typeof(Labels));
        }

        public override void Init()
        {
            base.Init();
        }

        public void SetTitleLabel(string _str)
        {
            GetLabel((int)Labels.title_label).text = _str; 
        }

        public void SetDetailLabel(string _str)
        {
            //GetLabel((int)Labels.detail_label).text = _str; 
        }

        public void AddBtnEvent()
        {
            
        }
        
        
    }    
}

