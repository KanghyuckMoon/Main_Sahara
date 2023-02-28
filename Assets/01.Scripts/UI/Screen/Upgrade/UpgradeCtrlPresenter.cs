using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;
using System;
using Inventory;
using GoogleSpreadSheet; 

namespace UI.Upgrade
{
    public class UpgradeCtrlPresenter
    {
        private UpgradeCtrlView upgradeCtrlView; 

        private FinalItemDataListSO finalListSO; // ���� �� ������ ����ƮSO
        private int curIdx; // ���� Ȱ��ȭ ���� Ʈ�� �ε��� 

        private Action<ItemDataSO> callback; // ���� ��������UI ����� ��Ÿ�� ȣ���� �Լ�( ������ Ʈ�� ǥ�� ) 

        // ������Ƽ 
        private ItemDataSO CurDataSO => finalListSO.itemList[curIdx]; 
        public UpgradeCtrlPresenter(VisualElement _parent, Action<ItemDataSO> _callback)
        {
            // SO�������� 
            finalListSO = AddressablesManager.Instance.GetResource<FinalItemDataListSO>("FinalItemDataListSO");
            curIdx = (int) finalListSO.itemList.Count / 2;
            // �� �ʱ�ȭ 
            upgradeCtrlView = new UpgradeCtrlView(); 
            upgradeCtrlView.InitUIParent(_parent);
            upgradeCtrlView.Cashing();
            upgradeCtrlView.Init();
            // �Լ� ���
            this.callback = _callback;
            // ��ư �̺�Ʈ ���
            AddButtonsEvent();

        }

        [ContextMenu("������Ʈ")]
        /// <summary>
        /// ��, ��ư UI ������Ʈ
        /// </summary>
        public void UpdateUI()
        {
            string _name = GetName(CurDataSO.nameKey);
            this.upgradeCtrlView.SetLabel(PosType.mid, _name);

            // ���� ���� 
            if (curIdx - 1 < 0)
            {
                // ��,��ư ��Ȱ��ȭ 
                this.upgradeCtrlView.InActiveLabel(PosType.left);
                this.upgradeCtrlView.ActiveButton(_isLeft: true, false);
            }
            else
            {
                _name = GetName(finalListSO.itemList[curIdx - 1].nameKey); 
                this.upgradeCtrlView.SetLabel(PosType.left, _name); 
                this.upgradeCtrlView.ActiveButton(_isLeft: true, true);
            }

            // ������ ���� 
            if (curIdx +1 > finalListSO.itemList.Count-1)
            {
                // ��,��ư ��Ȱ��ȭ 
                this.upgradeCtrlView.InActiveLabel(PosType.right);
                this.upgradeCtrlView.ActiveButton(_isLeft: false, false);
            }
            else
            {
                _name = GetName(finalListSO.itemList[curIdx + 1].nameKey);
                this.upgradeCtrlView.SetLabel(PosType.right, _name);
                this.upgradeCtrlView.ActiveButton(_isLeft: false, true);
            }
            callback?.Invoke(CurDataSO);

        }

        private void AddButtonsEvent()
        {
            this.upgradeCtrlView.AddButtonEvent(_isLeft:true, () =>
             {
                 --curIdx;
                 callback?.Invoke(CurDataSO);
             });
            this.upgradeCtrlView.AddButtonEvent(_isLeft: false, () =>
            {
                ++curIdx;
                callback?.Invoke(CurDataSO);
            });
        }

        private string GetName(string _key)
        {
            return TextManager.Instance.GetText(_key);
        }
    }

}
