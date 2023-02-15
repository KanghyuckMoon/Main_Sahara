using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;
using DG.Tweening; 

namespace UI.Production
{
    public class UpgradeSlotView : AbUI_Base
    {
        enum Elements
        {
            image,
            active_mark
        }
    
        enum Labels
        {
            text
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

        // === UI ¼³Á¤ °ü·Ã === //
        public void SetSpriteAndText(Texture2D _sprite, int _count)
        {
            GetVisualElement((int)Elements.image).style.backgroundImage = new StyleBackground(_sprite);
            GetLabel((int)Labels.text).text = _count.ToString();
        }

        /// <summary>
        /// ¼±ÅÃ½Ã ±ôºý±ôºý 
        /// </summary>
        public void ActiveSelect()
        {

            //GetVisualElement((int)Elements.active_mark)
        }
    }

}

