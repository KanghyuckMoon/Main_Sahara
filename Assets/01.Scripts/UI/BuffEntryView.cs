using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 
using UI.Base;

namespace UI
{
    public class BuffEntryView   : AbUI_Base
    {
        private SliderView coolView; 
        private float coolTime; // ÄðÅ¸ÀÓ ½Ã°£ 
        enum Elements
        {
            buff_image
        }

        enum ProgressBars 
        {
            buff_cool
        }

        public BuffEntryView(VisualElement root )
        {
            this.parentElement = root; 
        }

        public override void Cashing()
        {
            //base.Cashing();
            Bind<VisualElement>(typeof(Elements));
            Bind<ProgressBar>(typeof(ProgressBars)); 
        }

        //public void Init(/*BuffData buffData*/)
        //{
        //    Init(); 
        //}
        public override void Init()
        {
            base.Init();
            coolView = new SliderView(Get<ProgressBar>((int)ProgressBars.buff_cool));
        }
        
    }

}

