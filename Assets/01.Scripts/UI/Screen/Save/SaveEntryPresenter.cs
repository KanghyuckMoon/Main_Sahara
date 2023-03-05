using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Production;
using UI.ConstructorManager;
using System; 

namespace UI.Save
{
    public class SaveEntryPresenter : MonoBehaviour
    {
        private SaveEntryView saveEntryView;
        private VisualElement parent;

        public VisualElement Parent => parent;
        public SaveEntryPresenter()
        {
           var _pr = UIConstructorManager.Instance.GetProductionUI(typeof(SaveEntryView));
            saveEntryView = _pr.Item2 as SaveEntryView;
            parent = _pr.Item1; 
        }

        public void SetParent(VisualElement _parent)
        {
            _parent.Add(this.saveEntryView.ParentElement);
        }

        /// <summary>
        /// 이미지, 날짜 설정 
        /// </summary>
        /// <param name="_imgPath"></param>
        /// <param name="_date"></param>
        public void SetStrData(string _imgPath, string _date)
        {
            byte[] _byteTexture = System.IO.File.ReadAllBytes(_imgPath);
            Texture2D _texture = new Texture2D(0, 0);
            _texture.LoadImage(_byteTexture);

            saveEntryView.SetImage(_texture);
            saveEntryView.SetDate(_date); 
        }

        public void AddClickEvent(Action _callback)
        {
            this.saveEntryView.AddClickEvent(_callback);
        }
    }

}
