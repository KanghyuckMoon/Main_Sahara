using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;
using System; 

namespace UI.Production
{
    enum Labels
    {
        //title_label,
        date_label,
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
            BindVisualElements(typeof(Elements));
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
        public void SetDate(string _date)
        {
            GetLabel((int)Labels.date_label).text = _date;
        }

        public void SetImage(Texture2D _image)
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = new StyleBackground(_image);
        }

        public void AddClickEvent(Action _callback)
        {
            parentElement.RegisterCallback<ClickEvent>((x) => _callback?.Invoke());
        }
    }
}


