using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Production;
using UI.ConstructorManager; 
using Inventory;
using Utill.Addressable;
using Utill.Pattern;
using System; 

namespace UI.Upgrade
{
    public class UpgradeSlotPresenter
    {
        private UpgradeSlotView upgradeSlotView;
        private VisualElement parent; 
        private VisualElement element1; 
        private ItemData itemData;

        // ������Ƽ 
        public UpgradeSlotView UpgradeSlotView => upgradeSlotView;
        public VisualElement Parent => parent;
        public VisualElement Element1 => element1;
        public ItemData ItemData => itemData;
        public ItemData HaveItemData => InventoryManager.Instance.GetItem(itemData.key);
        public bool IsEnough
        {
            get
            {
                if (HaveItemData == null)
                {
                    ActiveEnough(false); 
                    return false;
                }

                    ActiveEnough(HaveItemData.count >= ItemData.count); 
                return HaveItemData.count >= ItemData.count; // ��� ������� 
            }
        }
        public UpgradeSlotPresenter()
        {
            var _production = UIConstructorManager.Instance.GetProductionUI(typeof(UpgradeSlotView));
            this.upgradeSlotView = _production.Item2 as UpgradeSlotView;
            this.parent = _production.Item1;
            element1 = parent.ElementAt(0);
        }

        public void SetItemData(ItemData _itemData)
        {
            if(this.upgradeSlotView == null)
            {
                Debug.LogWarning("UpgradeSlotView�� �������ּ���");
            }

            this.itemData = _itemData;
            //upgradeSlotView.IsStackable = _itemData.IsStackble;
            if(_itemData.spriteKey != "")
            {
                upgradeSlotView.SetSpriteAndText(AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey), $" {_itemData.count}");
            }
        }

        /// <summary>
        /// �ռ� ��ư ���� ��Ÿ�� ���� ���� ����/ �ʿ� ���� ǥ�� 
        /// </summary>
        /// <param name="_itemData"></param>
        public void SetItemDataHave(ItemData _itemData)
        {
            if (this.upgradeSlotView == null)
            {
                Debug.LogWarning("UpgradeSlotView�� �������ּ���");
            }
            this.itemData = _itemData;
            //upgradeSlotView.IsStackable = _itemData.IsStackble;

            // ���� ���� ���� üũ
            int _curCount = 0;
            if (HaveItemData != null)
            {
                _curCount = InventoryManager.Instance.GetItem(_itemData.key).count; // ���� ���� ���� 
            }

            if (_itemData.spriteKey != "")
            {
                upgradeSlotView.SetSpriteAndText(AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey), $" {_curCount }/{_itemData.count}");
            }
        }

        public void SetPositionType(bool _isAbsolute)
        {
            Parent.style.position = _isAbsolute ? Position.Absolute : Position.Relative;
        }
        /// <summary>
        ///  ����ϸ� �̹��� ������ 100%
        /// </summary>
        /// <param name="_isActive"></param>
        public void ActiveEnough(bool _isActive)
        {
            upgradeSlotView.Image.style.opacity =_isActive ? 1 : 0.7f; 
        }
        public void ActiveMark(bool _isActive)
        {
            this.upgradeSlotView.ActiveMark.style.display = _isActive ? DisplayStyle.Flex : DisplayStyle.None;
        }
        /// <summary>
        /// Ŭ�� �̺�Ʈ �߰� 
        /// </summary>
        public void AddClickEvent(Action _callback)
        {
            this.upgradeSlotView.AddClickEvent(_callback);
        }
    }

}

