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
        private int curIndex, topIndex, botIndex;
        private bool isTargetTop;

        // 프로퍼티 
        public UIDocument RootUIDocument { get; set ; }
       
        public void OnDisable()
        {
            EventManager.Instance.StopListening(EventsType.UpdateQuickSlot, UpdateUI);
        }
        
        public void Awake()
        {
            quickSlotView.InitUIDocument(RootUIDocument);
            quickSlotView.Cashing();
            quickSlotView.Init();

            // 스타일 클래스 이름 초기화 
            InitAnimateClassDic(); 
            
            EventManager.Instance.StartListening(EventsType.UpdateQuickSlot, UpdateUI);
    
            UpdateUI(); 
        }

        public void Start(object _data)
        {
        }

        public void UpdateUI()
        {
            for (int i = 0; i < quickSlotView.SlotList.Count; i++)
            {
                quickSlotView.SlotList[i].SetItemData(InventoryManager.Instance.GetQuickSlotItem(i));
            }
            //quickSlotView.ArrowSlot.SetItemData(InventoryManager.Instance.GetArrow());


            //quickSlotView.UpdateActiveEffect(); 
        }

        public void ActiveScreen(bool _isActive)
        {
            quickSlotView.ActiveScreen(_isActive);
        }

        private void UpdateActive()
        {
            selectSlotPr.SelectSlot(false);

            //selectSlotPr = _slotList[InventoryManager.Instance.GetCurrentQuickSlotIndex()];
            curIndex = InventoryManager.Instance.GetCurrentQuickSlotIndex();
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
            
        }

        private void DownQuickslots()
        {
            
        }

        private void InitAnimateClassDic()
        {
            this.animateClassDic.Clear();
            
            this.animateClassDic.Add(0,"top_quickslot_temp");
            this.animateClassDic.Add(1,"top_quickslot");
            this.animateClassDic.Add(2,"mid_quickslot");
            this.animateClassDic.Add(3,"bot_quickslot");
            this.animateClassDic.Add(4,"bot_quickslot_temp");
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
                topIndex = 0; 
            }
            else
            {
                topIndex = _idx + 1;
            }

            if (_idx - 1 <= -1)
            {
                botIndex = 5; 
            }
            else
            {
                botIndex = _idx - 1; 
            }
        }

        private void SetQuickSlot()
        {
        }
    }

}

