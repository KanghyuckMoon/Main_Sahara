using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 

namespace UI
{
    // ��� Presenter���� UIDocument �޾ƿ����� 
    // PresenterController ������ UIDocument �޾ƿͼ� �Ѱ��ִ��� 

    [Serializable]
    public class BuffContainer : AbUI_Base
    {
        [SerializeField]
        private VisualTreeAsset buffUxml;
        [SerializeField] // �ӽ� 
        private List<BuffView> buffViewList = new List<BuffView>(); 

        public void CreateBuffIcon()
        {
            // �� ������ �Ѱܼ� ���� 
            TemplateContainer buff = buffUxml.Instantiate();
            BuffView buffView = new BuffView(buff);

            buffView.Cashing();
            buffView.Init();

            parentElement.Add(buff); 
            buffViewList.Add(buffView);
            // buffView.��Ÿ�� ���� 
        }

    }
}

