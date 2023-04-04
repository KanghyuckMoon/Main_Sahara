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
        // ?�스?�터 참조 변??
        [SerializeField]
        private BuffView buffView;
        // private BuffData buffData; 

        private StatData statData;
        private BuffModule buffData;

        // ?�재 ?�성??중인 버프UI 
        private List<BuffEntryPresenter> curBuffViewList = new List<BuffEntryPresenter>();

        //private List<Buff>
        // ?�로?�티 
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
        /// 버프 ?�이??추�????�데?�트 ( ?�성)
        /// </summary>
        public void UpdateUI()
        {
            bool _isHave = false; 
            // 버프 모듈???�성??중인 버프 ?�면??
            foreach(var _buff in buffData.buffList)
            {
                // ?�재 ?�성?�중??버프UI?� 비교?�기 
                foreach(var _curBuff in curBuffViewList)
                {
                    if(_buff == _curBuff.BuffData)
                    {
                        _isHave = true; 
                    }
                }
                
                if(_isHave == false) // ?�로??것이?�면 
                {
                    CreateBuffIcon(_buff); 
                }
            }
        }

        /// <summary>
        /// 버프 ?�간 ?�데?�트
        /// </summary>
        public void UpdateBuffTime()
        {
            if (curBuffViewList.Count <= 0) return; 
            foreach(var _buffView in curBuffViewList)
            {
                // ?�간???�났?�면 
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
            // �??�이???�겨???�성 
            BuffEntryPresenter buffEntryPresenter = new BuffEntryPresenter();
            buffEntryPresenter.SetBuffData(_buffData); 
            buffEntryPresenter.SetParent(buffView.ParentElement);
            curBuffViewList.Add(buffEntryPresenter); 

            return buffEntryPresenter.Parent;

            // buffView.쿨�????�작 
        }

       
    }

}
