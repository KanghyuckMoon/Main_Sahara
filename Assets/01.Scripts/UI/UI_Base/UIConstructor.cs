using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;

namespace UI.Base
{
    /// <summary>
    /// T 의 View 를 생성하겠다 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UIConstructor<T> : ICreateUI where T : AbUI_Base, new()
    {
        private VisualTreeAsset uiUxml;
        private List<T> uiList = new List<T>();

        // 프로퍼티 
        public List<T> UIList => uiList;

        public UIConstructor(string _address)
        {
            this.uiUxml= AddressablesManager.Instance.GetResource<VisualTreeAsset>(_address); 
            // 어드레서블로 uiUxml 불러오기 
        }

        /// <summary>
        /// 동적 UI 생성 후 반환 
        /// </summary>
        /// <returns></returns>
        public (VisualElement,AbUI_Base) CreateUI()
        {
            // 뭐 데이터 넘겨서 생성 
            TemplateContainer buff = uiUxml.Instantiate();
            VisualElement _v = buff.contentContainer;
            T buffView = new T();

            buffView.InitUIParent(_v.ElementAt(0));
            buffView.Cashing();
            buffView.Init();

            uiList.Add(buffView);

            return (_v, buffView);
        }
    }

}
