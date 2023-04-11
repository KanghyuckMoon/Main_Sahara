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
using Utill.Coroutine;

namespace UI.Upgrade
{
    public class UpgradeSlotPresenter
    {
        private UpgradeSlotView upgradeSlotView;
        private VisualElement parent; 
        private VisualElement element1; 
        private ItemData itemData;

        private const string inactiveStr = "upgrade_slot_inactive"; 
        private const string activeStr = "upgrade_slot_active"; 
        // 프로퍼티 
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
                return HaveItemData.count >= ItemData.count; // 재료 충분한지 
            }
        }
        public UpgradeSlotPresenter(bool _isAnimation = false)
        {
            var _production = UIConstructorManager.Instance.GetProductionUI(typeof(UpgradeSlotView));
            this.upgradeSlotView = _production.Item2 as UpgradeSlotView;
            this.parent = _production.Item1;
            element1 = parent.ElementAt(0);

            if (_isAnimation == true)
            {
                element1.RemoveFromClassList(activeStr);
                element1.AddToClassList(inactiveStr);
                StaticCoroutineManager.Instance.InstanceDoCoroutine(Animate());
            }
            //element1.RemoveFromClassList(inactiveStr);
        }

        public IEnumerator Animate()
        {
            yield return new WaitForSeconds(0.01f); 
            element1.RemoveFromClassList(inactiveStr);
        }
        public void UpdateUI()
        {

        }

        public void SetItemData(ItemData _itemData)
        {
            if(this.upgradeSlotView == null)
            {
                Debug.LogWarning("UpgradeSlotView를 생성해주세요");
            }

            this.itemData = _itemData;
            //upgradeSlotView.IsStackable = _itemData.IsStackble;
            if(_itemData.spriteKey != "")
            {
                upgradeSlotView.SetSpriteAndText(AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey), $" {_itemData.count}");
            }
        }

        /// <summary>
        /// 합성 버튼 옆에 나타날 현재 보유 개수/ 필요 개수 표시 
        /// </summary>
        /// <param name="_itemData"></param>
        public void SetItemDataHave(ItemData _itemData)
        {
            if (this.upgradeSlotView == null)
            {
                Debug.LogWarning("UpgradeSlotView를 생성해주세요");
            }
            this.itemData = _itemData;
            //upgradeSlotView.IsStackable = _itemData.IsStackble;

            // 현재 보유 개수 체크
            int _curCount = 0;
            if (HaveItemData != null)
            {
                _curCount = InventoryManager.Instance.GetItem(_itemData.key).count; // 현재 보유 개수 
            }

            if (_itemData.spriteKey != "")
            {
                upgradeSlotView.SetSpriteAndText(AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey), $" {_curCount }/{_itemData.count}");
            }
        }

        private const string selectStr = "select_slot "; 
        /// <summary>
        ///  충분하면 이미지 불투명도 100%
        /// </summary>
        /// <param name="_isActive"></param>
        public void ActiveEnough(bool _isActive)
        {
            upgradeSlotView.Image.style.opacity =_isActive ? 1 : 0.7f; 
        }
        public void ActiveMark(bool _isActive)
        {
            // 크기 확대 
            if (_isActive == true)
            {
                element1.RemoveFromClassList(activeStr);
                this.element1.AddToClassList(selectStr);
            }
            else
            {
                element1.AddToClassList(activeStr);
                this.element1.RemoveFromClassList(selectStr);
            }
            this.upgradeSlotView.ActiveMark.style.display = _isActive ? DisplayStyle.Flex : DisplayStyle.None;
        }
        /// <summary>
        /// 클릭 이벤트 추가 
        /// </summary>
        public void AddClickEvent(Action _callback)
        {
            this.upgradeSlotView.AddClickEvent(_callback);
        }
    }

}

