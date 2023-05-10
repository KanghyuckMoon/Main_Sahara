using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;
using System; 

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
            price_label 
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
            BindButtons(typeof(Buttons));
        }

        public override void Init()
        {
            base.Init();
        }

        public void SetPriceLabel(int _price)
        {
            GetLabel((int)Labels.price_label).text = String.Format("АЁАн : " + "{0:###}", _price); 
        }
        public void SetTitleLabel(string _str)
        {
            GetLabel((int)Labels.title_label).text = _str; 
        }

        public void SetDetailLabel(string _str)
        {
            //GetLabel((int)Labels.detail_label).text = _str; 
        }

        public void AddBtnEvent(Action _acceptCallback, Action _cancelCallback)
        {
            AddButtonEvent<ClickEvent>((int)Buttons.accept_btn, () =>
            {
                _acceptCallback?.Invoke();
                ParentElement.RemoveFromHierarchy();
            });
            AddButtonEvent<ClickEvent>((int)Buttons.cancel_btn , () =>
            {
                _cancelCallback?.Invoke(); 
                ParentElement.RemoveFromHierarchy();
            });
        }
        
        
    }    
}

