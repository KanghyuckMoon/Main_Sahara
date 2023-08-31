using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using UnityEngine.UIElements;
using Module;
using Inventory;
using UI.EventManage; 
using Inventory;
using UI.Inventory;
using Skill;
using Utill.Addressable;


namespace UI
{
    [System.Serializable]
    public class QuickSlotPresenter : IUIFollower
    {
 //       [SerializeField]
//        private UIDocument uiDocument;

        [SerializeField]
        private QuickSlotView quickSlotView;

        private Dictionary<int, string> animateClassDic = new Dictionary<int, string>();// 퀵슬롯 위치 인덱스에 따른 스타일 클래스 문자열 
        private Dictionary<int, VisualElement> curSlotDic = new Dictionary<int, VisualElement>();// 퀵슬롯 위치 인덱스에 따른 스타일 클래스 문자열 
        private SlotItemPresenter selectSlotPr;
        private int targetIndex, curIndex, topIndex, botIndex;
        private int maxIndex = 4, minIndex = 0;
        private bool isTargetTop;

        [SerializeField]
        private int mana; 
        // 프로퍼티 
        public UIDocument RootUIDocument { get; set ; }
        
        public void OnDisable()
        {
            EventManager.Instance.StopListening(EventsType.UpdateQuickSlot, SetQuickSlot);
        }
        
        public void Awake()
        {
            quickSlotView.InitUIDocument(RootUIDocument);
            quickSlotView.Cashing();
            quickSlotView.Init();

            // 스타일 클래스 이름 초기화 
            InitAnimateClassDic(); 
            
            EventManager.Instance.StartListening(EventsType.UpdateQuickSlot, SetQuickSlot);

            InitSlotDic(); 
            UpdateUI(); 
        }

        public void Start()
        {
            EventManager.Instance.StartListening(EventsType.SetQuickslotMana,(x) => SetMana((int)x));
        }

        private void SetMana(int _value)
        {
            mana = _value; 
            quickSlotView.SetManaText(_value);
            // 업데이트 UI 
        }
        public void OnDestroy()
        {
            
        }
        public void Start(object _data)
        {
        }

        private void InitSlotDic()
        {
            curSlotDic.Clear();
            for (int i = 0; i < quickSlotView.SlotList.Count; i++)
            {
                curSlotDic.Add(i, quickSlotView.SlotList[i].Parent);
            }
        }
        public void UpdateUI()
        {
            for (int i = 0; i < quickSlotView.SlotList.Count; i++)
            {
                quickSlotView.SlotList[i].SetItemData(InventoryManager.Instance.GetQuickSlotItem(i),true);
            }

            // 마나를 가져와야 해 
            // 무기 스킬 펑션 클래스를 가져와 
            // 굿 
            // UI를 가져와서 데이터 업데이트 
            // 내가 새롭게 쓰일때마다 
            //int _mana = InventoryManager.Instance.PlayerWeaponModule.currentWeapon.GetComponent<WeaponSkillFunctions>().usingMana;
            //quickSlotView.ArrowSlot.SetItemData(InventoryManager.Instance.GetArrow());


            //quickSlotView.UpdateActiveEffect(); 
        }

        public void ActiveScreen(bool _isActive)
        {
            quickSlotView.ActiveScreen(_isActive);
            if (_isActive == false)
            {
                UpdateUI(); 
            }
        }

        private void UpdateActive()
        {
            selectSlotPr.SelectSlot(false);

            //selectSlotPr = _slotList[InventoryManager.Instance.GetCurrentQuickSlotIndex()];
            curIndex = InventoryManager.Instance.GetCurrentQuickSlotIndex();
            targetIndex = InventoryManager.Instance.GetCurrentQuickSlotIndex(); 
            SetTopBotIndex(curIndex);
            
            selectSlotPr.SelectSlot(true);
            //selectSlotPr.Parent.Add(GetVisualElement((int)Elements.select_effect));
        }

        private void AnimateQuickSlots()
        {
            int nowIndex = InventoryManager.Instance.GetCurrentQuickSlotIndex(); // 휠이나 키보드키로 변경후 인덱스 
            int indexDiff = nowIndex - curIndex; // 현재 활성화 중인 인덱스와의 차이 
            isTargetTop = indexDiff > 0 ? true : false; // true 위로 이동 
            //
            // 현재 클래스 상태 저장 딕셔너리 
            //
            //
            // 종료 후 설정
            this.curIndex = nowIndex; 
            // 양수면 now -> top으로 
            // 음수면 now -> bot 으로 

        }

        private void UpQuickslots()
        {
            curSlotDic[curIndex].AddToClassList("");
        }

        private void DownQuickslots()
        {
            
        }

        private void InitAnimateClassDic()
        {
            this.animateClassDic.Clear();

            this.animateClassDic.Add(0,"select_quickslot");
            this.animateClassDic.Add(1,"bottom_quickslot");
            this.animateClassDic.Add(2,"bottom_quickslot_temp");
            this.animateClassDic.Add(3,"top_quickslot_temp");
            this.animateClassDic.Add(4,"top_quickslot");
        }
        
        /// <summary>
        /// hud에 나타날 상하 인덱스 설정 
        /// </summary>
        /// <param name="_idx"></param>
        private void SetTopBotIndex(int _idx)
        {
            // 최대 슬롯인 5개보다 많으면 
            if (_idx + 1 >= 6)
            {
                topIndex = _idx - 5; 
            }
            else
            {
                topIndex = _idx + 1;
            }

            if (_idx - 1 <= 0)
            {
                botIndex = _idx + 5;
                
            }
            else
            {
                botIndex = _idx - 1; 
            }
        }

        private void SetQuickSlot()
        {
            curIndex = InventoryManager.Instance.GetCurrentQuickSlotIndex();
            
            RemoveStyleClass(curSlotDic[GetClampIndex(curIndex)]);
            curSlotDic[curIndex].AddToClassList(animateClassDic[0]);

            RemoveStyleClass(curSlotDic[GetClampIndex(curIndex+1)]);
            curSlotDic[GetClampIndex(curIndex+1)].AddToClassList(animateClassDic[1]);
            
            RemoveStyleClass(curSlotDic[GetClampIndex(curIndex+2)]);
            curSlotDic[GetClampIndex(curIndex+2)].AddToClassList(animateClassDic[2]);
            
            RemoveStyleClass(curSlotDic[GetClampIndex(curIndex+3)]);
            curSlotDic[GetClampIndex(curIndex+3)].AddToClassList(animateClassDic[3]);
            
            RemoveStyleClass(curSlotDic[GetClampIndex(curIndex+4)]);
            curSlotDic[GetClampIndex(curIndex+4)].AddToClassList(animateClassDic[4]);
            
            var _skillImage = AddressablesManager.Instance.GetResource<Sprite>(InventoryManager.Instance.GetCurrentQuickSlotItem().spriteKey);
            quickSlotView.SetSkillImage(_skillImage);
        }

        private void RemoveStyleClass(VisualElement _v)
        {
            foreach (var _animateStr in animateClassDic.Values)
            {
                if (_v.ClassListContains(_animateStr))
                {
                    _v.RemoveFromClassList(_animateStr);
                }
            }
        }
        private int GetClampIndex(int _index)
        {
            int _returnV = _index; 
            if (_index > maxIndex)
            {
                int _diff = _index - maxIndex -1 ;
                _returnV = _diff; 
            }
            else if(_index < minIndex)
            {
                int _diff = Mathf.Abs(_index - minIndex);
                _returnV = maxIndex - _diff + 1; // -1부터 max로 넘어가니 +1 
            }

            return _returnV; 
        }
    }

}

