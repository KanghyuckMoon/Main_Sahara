using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;

namespace UI.Base
{
    /// <summary>
    /// T �� View �� �����ϰڴ� 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UIConstructor<T> : ICreateUI where T : AbUI_Base, new()
    {
        private VisualTreeAsset uiUxml;
        private List<T> uiList = new List<T>();

        // ������Ƽ 
        public List<T> UIList => uiList;

        public UIConstructor(string _address)
        {
            this.uiUxml= AddressablesManager.Instance.GetResource<VisualTreeAsset>(_address); 
            // ��巹����� uiUxml �ҷ����� 
        }

        /// <summary>
        /// ���� UI ���� �� ��ȯ 
        /// </summary>
        /// <returns></returns>
        public (VisualElement,AbUI_Base) CreateUI()
        {
            // �� ������ �Ѱܼ� ���� 
            TemplateContainer buff = uiUxml.Instantiate();
            T buffView = new T();

            buffView.InitUIParent(buff);
            buffView.Cashing();
            buffView.Init();

            uiList.Add(buffView);

            return (buff, buffView);
        }
    }

}
