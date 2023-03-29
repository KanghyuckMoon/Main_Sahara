using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UI.Production;
using System.Linq;
namespace UI.Upgrade
{
    /// <summary>
    /// 업그레이드 창에서 슬롯 클릭시 활성화될 패널    
    /// </summary>
    public class UpgradePickPresenter
    {
        private UpgradePickView upgradePickView;

        private VisualElement parent;
        private List<UpgradeSlotPresenter> upgradeSlotList = new List<UpgradeSlotPresenter>();

        // 프로퍼티 
        public VisualElement Parent => parent;
        public UpgradePickPresenter(VisualElement _v)
        {
            this.parent = _v;

            upgradePickView = new UpgradePickView();
            upgradePickView.InitUIParent(_v);
            upgradePickView.Cashing();
            upgradePickView.Init();
        }

        /// <summary>
        /// 합성하기 버튼 이벤트 추가 
        /// </summary>
        /// <param name="_callback"></param>
        public void SetButtonEvent(Action _callback)
        {
            this.upgradePickView.AddComposeButtonEvent(_callback);
        }

        public void SetSlotParent(UpgradeSlotPresenter _v)
        {
            upgradeSlotList.Add(_v);
            upgradePickView.SetParent(_v.Parent);
        }

        public void SetAbsoluteParent(UpgradeSlotPresenter _v)
        {
            upgradePickView.SetParent(_v.Parent);
        }
        public void SetPos(Vector2 _v)
        {
            //parent.style.translate = new StyleTranslate(new Translate(_v.x,_v.y));
            //parent.style.left = _v.x; 
            //parent.style.top = _v.y;
            parent.transform.position = _v; 
            //parent.style.left = Parent.worldBound.width / 2 +_v.x;
        }
        public void SetPos(float _f)
        {
            //parent.style.translate = new StyleTranslate(new Translate(_v.x,_v.y));
            //parent.style.left = _v.x; 
            //parent.style.top = _v.y;
        //    parent.style.left = (100) / _f;
            //parent.style.left = 100;
            //parent.transform.position = _v; 

        }
        /// <summary>
        /// 슬롯 리스트 초기화 
        /// </summary>
        public void ClearSlots()
        {
            upgradeSlotList.ForEach((x) => x.Parent.RemoveFromHierarchy());
            upgradeSlotList.Clear();
        }

        public void ActiveView()
        {
            upgradePickView.ActiveScreen();
        }
        public void ActiveView(bool _isActive)
        {
            if(_isActive == true)
            {
                CheckUpgrade();
            }
            upgradePickView.ActiveScreen(_isActive);
        }


        /// <summary>
        /// 합성 가능 여부 체크 
        /// </summary>
        private void CheckUpgrade()
        {
            // 모든 재료가 있다면 합성가능 
            bool _isCan = upgradeSlotList.Where((x) => x.IsEnough).Count() == upgradeSlotList.Count; //합성 가능 여부 
            upgradePickView.ActiveUpgradeButton(_isCan);
        }

    }

}

