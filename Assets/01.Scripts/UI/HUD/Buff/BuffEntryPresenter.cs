using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Production;
using UI.ConstructorManager;
using Buff;
using Utill.Addressable; 

namespace UI
{
    public class BuffEntryPresenter 
    {
        private BuffEntryView buffEntryView;
        private VisualElement parent;

        private AbBuffEffect buffData; 
        // private UIBuffData buffData; 
        // ������Ƽ 
        public VisualElement Parent => parent;
        public AbBuffEffect BuffData => buffData; 

        public BuffEntryPresenter()
        {
            var _prod = UIConstructorManager.Instance.GetProductionUI(typeof(BuffEntryView));
            this.buffEntryView = _prod.Item2 as BuffEntryView;
            this.parent = _prod.Item1; 

        }

        /// <summary>
        /// ������ �θ� ���� 
        /// </summary>
        /// <param name="_parent"></param>
        public void SetParent(VisualElement _parent)
        {
            _parent.Add(this.buffEntryView.Parent);
        }

        public void SetBuffData(AbBuffEffect _buffData)
        {
            this.buffData = _buffData;
            this.buffEntryView.SetImage(AddressablesManager.Instance.GetResource<Texture2D>(buffData.Spriteaddress));
        }
        /// <summary>
        /// ��Ÿ�� ǥ�� �����̴� ������ƮUI 
        /// </summary>
        /// <param name="_targetV"></param>
        public bool UpdateUI()
        {
            // �ð� �� �ƴ� 
            if (buffData.MaxDuration / buffData.Duration <= 0) return false; 

            buffEntryView.CoolView.BarV = 1-buffData.Duration/ buffData.MaxDuration;
            //buffEntryView.SetText(((int)buffData.Duration).ToString());
            return true; 
        }

        public void Destroy()
        {
            buffEntryView.Parent.RemoveFromHierarchy(); 
        }
    }

}
