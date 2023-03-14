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
        // 인스펙터 참조 변수 
        [SerializeField]
        private BuffView buffView;
        // private BuffData buffData; 

        private StatData statData;
        private BuffModule buffData;

        // 현재 활성화 중인 버프UI 
        private List<BuffEntryPresenter> curBuffViewList = new List<BuffEntryPresenter>();

        //private List<Buff>
        // 프로퍼티 
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
        /// 버프 데이터 추가시 업데이트 ( 생성)
        /// </summary>
        public void UpdateUI()
        {
            bool _isHave = false; 
            // 버프 모듈의 활성화 중인 버프 돌면서 
            foreach(var _buff in buffData.buffList)
            {
                // 현재 활성화중인 버프UI와 비교하기 
                foreach(var _curBuff in curBuffViewList)
                {
                    if(_buff == _curBuff.BuffData)
                    {
                        _isHave = true; 
                    }
                }
                
                if(_isHave == false) // 새로운 것이라면 
                {
                    CreateBuffIcon(_buff); 
                }
            }
        }

        /// <summary>
        /// 버프 시간 업데이트
        /// </summary>
        public void UpdateBuffTime()
        {
            if (curBuffViewList.Count <= 0) return; 
            foreach(var _buffView in curBuffViewList)
            {
                // 시간이 끝났으면 
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
            // 뭐 데이터 넘겨서 생성 
            BuffEntryPresenter buffEntryPresenter = new BuffEntryPresenter();
            buffEntryPresenter.SetBuffData(_buffData); 
            buffEntryPresenter.SetParent(buffView.ParentElement);
            curBuffViewList.Add(buffEntryPresenter); 

            return buffEntryPresenter.Parent;

            // buffView.쿨타임 시작 
        }

       
    }

}
