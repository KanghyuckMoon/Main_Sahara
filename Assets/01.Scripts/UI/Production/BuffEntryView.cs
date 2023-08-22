using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 
using UI.Base;

namespace UI.Production
{
    public class BuffEntryView   : AbUI_Base
    {
        private SliderView coolView; 
        private float coolTime; // ????? ?©£? 

        // ??????? 
        public VisualElement Parent => parentElement;
        public SliderView CoolView => coolView; 

        enum Elements
        {
            buff_image
        }

        enum ProgressBars 
        {
            buff_cool
        }

        enum Labels
        {
            time_label
        }

        public BuffEntryView()
        {

        }
        public BuffEntryView(VisualElement root )
        {
            this.parentElement = root; 
        }

        public override void Cashing()
        {
            //base.Cashing();
            BindVisualElements(typeof(Elements));
            BindLabels(typeof(Labels));
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

        public void SetImage(Texture2D _image)
        {
            GetVisualElement((int)Elements.buff_image).style.backgroundImage = new StyleBackground(_image); 
        }
        public void SetText(string _str)
        {
            GetLabel((int)Labels.time_label).text = _str; 
        }
    }

}

