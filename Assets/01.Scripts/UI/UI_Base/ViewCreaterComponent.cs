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
        //[SerializeField] // 임시 
        //private List<BuffView> buffViewList = new List<BuffView>();
        private AbUI_Base uiBase; 

        public VisualElement CreateBuffIcon(AbUI_Base _uiBase)
        {
            // 뭐 데이터 넘겨서 생성 
            TemplateContainer buff = assetUxml.Instantiate();
            return buff; 
        }

    }

}
