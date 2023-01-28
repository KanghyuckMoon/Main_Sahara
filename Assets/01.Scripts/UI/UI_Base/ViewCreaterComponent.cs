using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 

namespace UI
{
    [Serializable]
    public class ViewCreaterComponent 
    {
        [SerializeField]
        private VisualTreeAsset assetUxml;
        //[SerializeField] // �ӽ� 
        //private List<BuffView> buffViewList = new List<BuffView>();
        private AbUI_Base uiBase; 

        public VisualElement CreateBuffIcon(AbUI_Base _uiBase)
        {
            // �� ������ �Ѱܼ� ���� 
            TemplateContainer buff = assetUxml.Instantiate();
            return buff; 
        }

    }

}
