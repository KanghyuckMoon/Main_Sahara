using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;


namespace UI.Production
{
    public class PopupTutorialView : AbUI_Base
    {
        enum Labels
        {
            title_label, 
            detail_label
        }

        enum Elements
        {
            detail_image
        }
        
        public override void Cashing()
        {
            //base.Cashing();
            Debug.Log(parentElement.name);
            BindLabels(typeof(Labels));
            BindVisualElements(typeof(Elements));
        }

        public override void Init()
        {
            base.Init();
        }

        public void SetTitle(string _title)
        {
            GetLabel((int)Labels.title_label).text = _title; 
        }
        public void SetDetail(string _detail)
        {
            GetLabel((int)Labels.detail_label).text = _detail; 
        }
        public void SetDetailImage(Sprite _detailImage)
        {
            GetVisualElement((int)Elements.detail_image).style.backgroundImage = new StyleBackground(_detailImage); 
        }


    }
}