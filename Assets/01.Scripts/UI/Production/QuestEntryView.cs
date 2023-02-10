using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Production
{
    public class QuestEntryView : AbUI_Base
    {
        enum Labels
        {
            name_label, 
            detail_label 
        }

        public QuestEntryView()
        {

        }
        public QuestEntryView(VisualElement _parentE)
        {
            InitUIParent(_parentE);
            Cashing();
            Init(); 
        }

        public override void Cashing()
        {
            //base.Cashing();
            BindLabels(typeof(Labels)); 
        }

        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// 이름, 설명 설정 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_detail"></param>
        public void SetNameAndDetail(string _name, string _detail)
        {
            GetLabel((int)Labels.name_label).text = _name;
            GetLabel((int)Labels.detail_label).text = _detail;
        }
    }

}
