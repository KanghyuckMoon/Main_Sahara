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
        // ?¸ìŠ¤?™í„° ì°¸ì¡° ë³€??
        [SerializeField]
        private BuffView buffView;
        // private BuffData buffData; 

        private StatData statData;
        private BuffModule buffData;

        // ?„ì¬ ?œì„±??ì¤‘ì¸ ë²„í”„UI 
        private List<BuffEntryPresenter> curBuffViewList = new List<BuffEntryPresenter>();

        //private List<Buff>
        // ?„ë¡œ?¼í‹° 
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
        /// ë²„í”„ ?°ì´??ì¶”ê????…ë°?´íŠ¸ ( ?ì„±)
        /// </summary>
        public void UpdateUI()
        {
            bool _isHave = false; 
            // ë²„í”„ ëª¨ë“ˆ???œì„±??ì¤‘ì¸ ë²„í”„ ?Œë©´??
            foreach(var _buff in buffData.buffList)
            {
                // ?„ì¬ ?œì„±?”ì¤‘??ë²„í”„UI?€ ë¹„êµ?˜ê¸° 
                foreach(var _curBuff in curBuffViewList)
                {
                    if(_buff == _curBuff.BuffData)
                    {
                        _isHave = true; 
                    }
                }
                
                if(_isHave == false) // ?ˆë¡œ??ê²ƒì´?¼ë©´ 
                {
                    CreateBuffIcon(_buff); 
                }
            }
        }

        /// <summary>
        /// ë²„í”„ ?œê°„ ?…ë°?´íŠ¸
        /// </summary>
        public void UpdateBuffTime()
        {
            if (curBuffViewList.Count <= 0) return; 
            foreach(var _buffView in curBuffViewList)
            {
                // ?œê°„???ë‚¬?¼ë©´ 
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
            // ë­??°ì´???˜ê²¨???ì„± 
            BuffEntryPresenter buffEntryPresenter = new BuffEntryPresenter();
            buffEntryPresenter.SetBuffData(_buffData); 
            buffEntryPresenter.SetParent(buffView.ParentElement);
            curBuffViewList.Add(buffEntryPresenter); 

            return buffEntryPresenter.Parent;

            // buffView.ì¿¨í????œì‘ 
        }

       
    }

}
