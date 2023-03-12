using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Module;
using Data;
using Buff;

namespace UI
{
    [Serializable]
    public class BuffPresenter : IUIFollower,Observer
    {
        // �ν����� ���� ���� 
        [SerializeField]
        private BuffView buffView;
        // private BuffData buffData; 

        private StatData statData;
        private BuffModule buffData;

        // ���� Ȱ��ȭ ���� ����UI 
        private List<BuffEntryPresenter> curBuffViewList = new List<BuffEntryPresenter>();

        //private List<Buff>
        // ������Ƽ 
        public UIDocument RootUIDocument { get; set; }

        public void Awake()
        {
            buffView.InitUIDocument(RootUIDocument);
            buffView.Cashing(); 
        }

        public void Start(object _data)
        {
            buffView.Init();
            buffData = _data as BuffModule;
            buffData.AddObserver(this); 
        }

        public void Update()
        {
            UpdateBuffTime(); 
        }

        public void Receive()
        {
            UpdateUI();
        }

        /// <summary>
        /// ���� ������ �߰��� ������Ʈ ( ����)
        /// </summary>
        public void UpdateUI()
        {
            bool _isHave = false; 
            // ���� ����� Ȱ��ȭ ���� ���� ���鼭 
            foreach(var _buff in buffData.buffList)
            {
                // ���� Ȱ��ȭ���� ����UI�� ���ϱ� 
                foreach(var _curBuff in curBuffViewList)
                {
                    if(_buff == _curBuff.BuffData)
                    {
                        _isHave = true; 
                    }
                }
                
                if(_isHave == false) // ���ο� ���̶�� 
                {
                    CreateBuffIcon(_buff); 
                }
            }
        }

        /// <summary>
        /// ���� �ð� ������Ʈ
        /// </summary>
        public void UpdateBuffTime()
        {
            if (curBuffViewList.Count <= 0) return; 
            foreach(var _buffView in curBuffViewList)
            {
                // �ð��� �������� 
                if (_buffView.UpdateUI() == false)
                {
                    curBuffViewList.Remove(_buffView);
                    _buffView.Destroy();
                    break; 
                }
            }
        }

        public VisualElement CreateBuffIcon(AbBuffEffect _buffData)
        {
            // �� ������ �Ѱܼ� ���� 
            BuffEntryPresenter buffEntryPresenter = new BuffEntryPresenter();
            buffEntryPresenter.SetBuffData(_buffData); 
            buffEntryPresenter.SetParent(buffView.ParentElement);
            curBuffViewList.Add(buffEntryPresenter); 

            return buffEntryPresenter.Parent;

            // buffView.��Ÿ�� ���� 
        }

       
    }

}
