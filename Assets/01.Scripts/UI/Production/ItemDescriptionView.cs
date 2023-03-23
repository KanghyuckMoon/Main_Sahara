    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Production
{
    public class ItemDescriptionView : AbUI_Base
    {
        enum Elements
        {
            description_panel,
            image, // 아이템 이미지 
        }
        enum Labels
        {
            name_label, // 아이템 이름 텍스트 
            description_label, // 아이템 설명 텍스트 
        }

        public VisualElement Panel => GetVisualElement((int)Elements.description_panel); 

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

        /// <summary>
        /// 아이템 이미지 설정 
        /// </summary>
        /// <param name="_image"></param>
        public void SetImage(Texture2D _image)
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = new StyleBackground(_image); 
        }

        /// <summary>
        /// 이름, 설명 텍스트 설정 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_description"></param>
        public void SetNameAndDesciption(string _name, string _description)
        {
            GetLabel((int)Labels.name_label).text = _name;
            GetLabel((int)Labels.description_label).text = _description; 
        }

        public void SetPos(Vector2 _pos)
        {
            parentElement.style.left = _pos.x;
            parentElement.style.top = _pos.y;
        }

    }

}

