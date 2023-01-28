using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 

namespace UI
{
    // 모든 Presenter마다 UIDocument 받아오느냐 
    // PresenterController 에서만 UIDocument 받아와서 넘겨주느냐 

    [Serializable]
    public class BuffContainer : AbUI_Base
    {
        [SerializeField]
        private VisualTreeAsset buffUxml;
        [SerializeField] // 임시 
        private List<BuffView> buffViewList = new List<BuffView>(); 

        public void CreateBuffIcon()
        {
            // 뭐 데이터 넘겨서 생성 
            TemplateContainer buff = buffUxml.Instantiate();
            BuffView buffView = new BuffView(buff);

            buffView.Cashing();
            buffView.Init();

            parentElement.Add(buff); 
            buffViewList.Add(buffView);
            // buffView.쿨타임 시작 
        }

    }
}

