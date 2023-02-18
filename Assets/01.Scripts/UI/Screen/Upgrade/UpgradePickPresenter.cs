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
    /// ���׷��̵� â���� ���� Ŭ���� Ȱ��ȭ�� �г�    
    /// </summary>
    public class UpgradePickPresenter
    {
        private UpgradePickView upgradePickView;

        private VisualElement parent;
        private List<UpgradeSlotPresenter> upgradeSlotList = new List<UpgradeSlotPresenter>();

        // ������Ƽ 
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
        /// �ռ��ϱ� ��ư �̺�Ʈ �߰� 
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
            parent.style.left = Parent.worldBound.width / 2 +_v.x; 
        }
        public void SetPos(float _f)
        {
            //parent.style.translate = new StyleTranslate(new Translate(_v.x,_v.y));
            //parent.style.left = _v.x; 
            //parent.style.top = _v.y;
            parent.style.left = (Parent.worldBound.width / 2) / _f;
        }
        /// <summary>
        /// ���� ����Ʈ �ʱ�ȭ 
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
        /// �ռ� ���� ���� üũ 
        /// </summary>
        private void CheckUpgrade()
        {
            // ��� ��ᰡ �ִٸ� �ռ����� 
            bool _isCan = upgradeSlotList.Where((x) => x.IsEnough).Count() == upgradeSlotList.Count; //�ռ� ���� ���� 
            upgradePickView.ActiveUpgradeButton(_isCan);
        }

    }

}

