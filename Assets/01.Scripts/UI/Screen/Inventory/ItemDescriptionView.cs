using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Inventory
{
    public class ItemDescriptionView : AbUI_Base
    {
        enum Elements
        {
            image, // ������ �̹��� 
        }
        enum Labels
        {
            name_label, // ������ �̸� �ؽ�Ʈ 
            description_label, // ������ ���� �ؽ�Ʈ 
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

        /// <summary>
        /// ������ �̹��� ���� 
        /// </summary>
        /// <param name="_image"></param>
        public void SetImage(Texture2D _image)
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = new StyleBackground(_image); 
        }

        /// <summary>
        /// �̸�, ���� �ؽ�Ʈ ���� 
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

