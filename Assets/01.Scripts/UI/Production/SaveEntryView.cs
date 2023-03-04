using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;

namespace UI.Production
{
    enum Labels
    {
        title_label, 
        data_label,
    }
    enum Elements
    {
        image, 
    }
    
    public class SaveEntryView : AbUI_Base, IConstructorUI
    {

        public override void Cashing()
        {
            //base.Cashing();
            BindLabels(typeof(Labels));
            BindLabels(typeof(Elements));
        }

        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// 이름, 설명 설정 
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="_title"></param>
        public void SetNameAndDetail(string _data, string _title)
        {
            GetLabel((int)Labels.data_label).text = _data;
            GetLabel((int)Labels.title_label).text = _title;
        }

        public void SetImage(Texture2D _image)
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = new StyleBackground(_image);
        }
    }
}


