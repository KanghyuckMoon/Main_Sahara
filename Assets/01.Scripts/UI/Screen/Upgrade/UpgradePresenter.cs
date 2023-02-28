using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;
using Inventory;
using Utill.Addressable;
using Utill.Measurement;
using Utill.Pattern;
using UI.ConstructorManager;
using UI.Production;
using System.Linq;
using DG.Tweening; 

namespace UI.Upgrade
{
    public class UpgradePresenter : MonoBehaviour, IScreen
    {
        private UIDocument uiDocument;

        [SerializeField]
        private UpgradeView upgradeView;

        private UpgradePickPresenter upgradePickPresenter; // 슬롯 선택시 나타날 업드레이드 패널 
        private UpgradeCtrlPresenter ctrlPresenter; // 좌우 버튼 , 상단 라벨 조작 Pr 
        private UpgradeSlotPresenter _curSlotPr; // 현재 선택한 슬롯

        private List<VisualElement> rowList = new List<VisualElement>(); // 줄 리스트 
        private List<UpgradeSlotPresenter> allSlotList = new List<UpgradeSlotPresenter>(); // 모든 슬롯 리스트 

        [SerializeField]
        private ElementCtrlComponent elementCtrlComponent; // 움직임 확대 축소 
        private Dictionary<int, List<Vector2>> slotPosDIc = new Dictionary<int, List<Vector2>>(); // 재료 수에 따른 위치 딕셔너리 
        private Queue<UpgradeSlotData> itemDataQueue = new Queue<UpgradeSlotData>(); // 생성할 아이템 저장 큐 
        private List<UpgradeSlotData> allItemDataList = new List<UpgradeSlotData>(); // 현재 트리의 모든 데이터 리스트
        private List<VisualElement> allItemList = new List<VisualElement>(); // 현재 트리의 모든 아이템 리스트 

        private float midX; // 중심 좌표 
        private bool isFirstSlot = true;

        // 프로퍼티 
        public IUIController UIController { get; set; }
        private VisualElement CurRow => rowList[rowList.Count - 1];
        private ElementCtrlComponent ElementCtrlComponent
        {
            get
            {
                if (elementCtrlComponent == null)
                {
                    elementCtrlComponent = new ElementCtrlComponent(upgradeView.MoveScreen);
                }
                return elementCtrlComponent;
            }
        }
        private UpgradeCtrlPresenter UpgradeCtrlPr
        {
            get
            {
                if (ctrlPresenter == null)
                {
                    ctrlPresenter = new UpgradeCtrlPresenter(upgradeView.Parent, CreateItemTree);
                }
                return ctrlPresenter;
            }
        }
        private void Start()
        {
            elementCtrlComponent = new ElementCtrlComponent(upgradeView.MoveScreen);
            //   StartCoroutine(SetAllSlotPosCo());
        }
        IEnumerator SetAllSlotPosCo()
        {
            // UIToolkit은 레이아웃 구축 시간이 소요되기 때문에
            // 다른 element의 worldbound를 제대로 가져오기 위해서는 구축될 시간이 지난후 가져와야함 
            yield return new WaitForSeconds(0.01f);
            SetAllSlotPos();
            yield return new WaitForSeconds(0.01f);
            SetAllSlotPos();
            yield return new WaitForSeconds(0.01f);
            SetAllSlotPos();
        }

        private void OnEnable()
        {
            uiDocument = GetComponent<UIDocument>();
            upgradeView.InitUIDocument(uiDocument);
            upgradeView.Cashing();
            upgradeView.Init();

            // 빈공간 클릭하면 업그레이드 UI 없어지도록 
            //upgradeView.Parent.RegisterCallback<ClickEvent>((x) =>
            //{
            //    upgradePickPresenter.ActiveView(false);
            //    InActiveAllMark();
            //},TrickleDown.TrickleDown);

            upgradePickPresenter = new UpgradePickPresenter(upgradeView.UpgradePickParent);
            upgradePickPresenter.SetButtonEvent(() => ItemUpgradeManager.Instance.Upgrade(_curSlotPr.ItemData.key));

            rowList.Clear();
            slotPosDIc.Clear();
            allSlotList.Clear();

            InitDic();

            ctrlPresenter = new UpgradeCtrlPresenter(upgradeView.Parent, CreateItemTree);
        }

        private void LateUpdate()
        {
            ElementCtrlComponent.Update();
        }

        #region Init
        private float slotDist = 300f; // 슬롯 간의 거리 
        private void InitDic()
        {
            midX = Screen.width / 2;

            List<Vector2> _oneList = new List<Vector2>();
            _oneList.Add(Vector2.zero);

            List<Vector2> _twoList = new List<Vector2>();
            _twoList.Add(new Vector2(-slotDist, 0));
            _twoList.Add(new Vector2(slotDist, 0));

            List<Vector2> _threeList = new List<Vector2>();
            _threeList.Add(new Vector2(-slotDist, 0));
            _threeList.Add(new Vector2(0, 0));
            _threeList.Add(new Vector2(slotDist, 0));


            List<Vector2> _fourList = new List<Vector2>();
            _fourList.Add(new Vector2(-slotDist * 2, 0));
            _fourList.Add(new Vector2(-slotDist, 0));
            _fourList.Add(new Vector2(slotDist, 0));
            _fourList.Add(new Vector2(slotDist * 2, 0));

            this.slotPosDIc.Add(1, _oneList);
            this.slotPosDIc.Add(2, _twoList);
            this.slotPosDIc.Add(3, _threeList);
            this.slotPosDIc.Add(4, _fourList);
        }

        #endregion

        [ContextMenu("아이템 트리 생성")]
        /// <summary>
        ///  아이템 트리 UI 생성 및 데이터 넣기 
        /// </summary>
        public void CreateItemTree(ItemDataSO _itemDataSO)
        {
            // 최종템 UI 생성
            allItemDataList.Clear();
            ItemData _itemData = ItemData.CopyItemDataSO(_itemDataSO);
            itemDataQueue.Enqueue(new UpgradeSlotData(new VisualElement(), _itemData, 0, 1));

            CreateTree(); // 재료 템 트리 생성 
            this.ElementCtrlComponent.ResetPosAndZoom();
        }

        /// <summary>
        /// 재료 슬롯들 생성  
        /// </summary>
        /// <param name="_itemUpgradeDataSO"></param>
        private void CreateTree()
        {
            List<ItemData> _slotDataList = new List<ItemData>(); // 재료 슬롯 데이터 리스트 (연결점 생성시 필요)
            List<ItemData> _list = new List<ItemData>(); // 한 무기에서 필요한 재료무기들 

            CreateRow(); // 처음줄 생성 
            int _count = itemDataQueue.Count();
            int _index = 0;

            while (itemDataQueue.Count > 0)
            {
                if (_index >= _count) // 한 줄 생성 끝 다음 줄 시작 
                {
                    CreateConnection(_slotDataList);
                    _count = itemDataQueue.Count;
                    _index = 0;
                    _slotDataList.Clear();
                    CreateRow();
                }

                UpgradeSlotData _slotData = itemDataQueue.Dequeue(); // 큐에서 데이터 꺼내서 
                ItemData _itemData = _slotData.itemData;
                VisualElement _parent = CreateSlot(_itemData); // 슬롯 생성
                _parent.style.opacity = 0f;
                allItemList.Add(_parent);

                //// 위치 설정 
                if (isFirstSlot == true) // 최종 아이템이면 가운데 고정 생성 
                {
                    _parent.style.left = midX - _parent.resolvedStyle.width ;
                    isFirstSlot = false;
                }
                else
                {
                    _parent.style.left = _slotData.parentSlot.worldBound.x + slotPosDIc[_slotData.maxIndex].ElementAt(_slotData.index).x;
                }

                ItemUpgradeDataSO _childItemData = ItemUpgradeManager.Instance.GetItemUpgradeDataSO(_itemData.key);
                if (_childItemData != null) // 재료템이 존재한다면 큐에 추가 
                {
                    var _dataList = ItemUpgradeManager.Instance.UpgradeItemSlotList(_itemData.key);

                    // 무기만 생성 
                    int _idx = 0; // 몇 번째 아이템인지 ( 트리 중에 )  같은 줄 내에서 
                    var _weaponDList = _dataList.Where((x) => x.itemType == ItemType.Weapon).ToList();
                    _weaponDList.ForEach((x) =>
                    {
                        UpgradeSlotData _slotData = new UpgradeSlotData(_parent, x, _idx, _weaponDList.Count);
                        itemDataQueue.Enqueue(_slotData);
                        allItemDataList.Add(_slotData);
                        ++_idx;
                    });

                    _slotDataList.AddRange(_dataList); // 연결점 생성시 필요 
                }
                ++_index;
            }

        }
        private bool isActive = false; // 포지션 설정시 활성화 여부 

        [ContextMenu("고고")]
        private void SetAllSlotPos()
        {
            int _idx = 1;
            allItemList[0].style.left = midX;
            allItemList[0].style.opacity = isActive ? 1 : 0;

            foreach (var _v in allItemDataList)
            {
                float _moveX = slotPosDIc[_v.maxIndex].ElementAt(_v.index).x;
                _moveX = _moveX < 0 ? _moveX - allItemList[_idx].resolvedStyle.width : _moveX;
                allItemList[_idx].style.left = _moveX + _v.parentSlot.worldBound.x;

                // opacity 설정
                float _op = isActive ? 1 : 0;
                //DOTween.To(() =>0f, x => allItemList[_idx].style.opacity = x, _op, 0.5f).SetDelay(0.5f); 
                allItemList[_idx].style.opacity = isActive ? 1 : 0;
                ++_idx;
            }
            isActive = true;
        }
        
        [ContextMenu("삭제")]
        private void ClearAllSlots()
        {
            isFirstSlot = true;
            rowList.Clear();
            allSlotList.Clear();
            allItemList.Clear();

            this.upgradeView.ClearAllSlots();
            isActive = false;
        }

        private VisualElement CreateSlot(ItemData _itemData)
        {
            UpgradeSlotPresenter _slotPr = new UpgradeSlotPresenter();
            _slotPr.AddClickEvent(
                () =>
                {
                    ActiveUpgradePn(_slotPr);
                    _curSlotPr = _slotPr;
                }
            );

            //this.upgradeView.SetParentSlot(_upgradePr.Parent);
            allSlotList.Add(_slotPr);
            this.CurRow.Add(_slotPr.Parent.ElementAt(0));
            _slotPr.SetItemData(_itemData);

            return _slotPr.Element1;
        }

        /// <summary>
        /// 슬롯 클릭시 업그레이드 UI 표시 
        /// </summary>
        private void ActiveUpgradePn(UpgradeSlotPresenter _upgradePr)
        {
            Debug.Log("클릭");

            upgradePickPresenter.ClearSlots(); // 있던거 초기화 해주고 
            InActiveAllMark(); // 모든 선택 마크 비활성화 
            _upgradePr.ActiveMark(true);

            ItemUpgradeDataSO _childItemData = ItemUpgradeManager.Instance.GetItemUpgradeDataSO(_upgradePr.ItemData.key); //
            if (_childItemData == null) // 재료템이 없으면 
            {
                upgradePickPresenter.ActiveView(false);
                return;
            }

            // 위치 설정(해야 해) 
            Rect _r3 = _upgradePr.Element1.worldBound;
            _upgradePr.Element1.Add(upgradePickPresenter.Parent);
            //upgradePickPresenter.SetPos(new Vector2(_r3.width / 2, _r3.y));
            upgradePickPresenter.SetPos(upgradeView.MoveScreen.resolvedStyle.scale.value.x);


            // 필요 재료들 표시 
            int _idx = 0;
            var _list = ItemUpgradeManager.Instance.UpgradeItemSlotList(_childItemData.key).Where((x) => x.itemType != ItemType.Weapon).ToList();
            foreach (var _data in _list)
            {
                UpgradeSlotPresenter _newUpgradePr = new UpgradeSlotPresenter();
                _newUpgradePr.SetItemDataHave(_data);
                upgradePickPresenter.SetSlotParent(_newUpgradePr);
            }
            upgradePickPresenter.ActiveView(true);
        }

        /// <summary>
        /// 선택시 활성화 마크 모두비활성화하기 
        /// </summary>
        private void InActiveAllMark()
        {
            this.allSlotList.ForEach((x) => x.ActiveMark(false));
        }
        private void CreateConnection(List<ItemData> _slotList)
        {
            CreateRow(); // 줄 생성 
            int _count = _slotList.Count; // 자식 개수 
            int _midCount = (_count / 2); // 중간 개수 
            if (_count % 2 == 0) //  짝수라면
            {
                for (int i = 0; i < _count; i++)
                {
                    if (i < _midCount)
                    {
                        // ㄱ리버스 생성
                    }
                    else if (i < _midCount)
                    {
                        // ㄱ 생성
                    }
                    else if (i == _midCount - 1)
                    {
                        // 오른쪽 향하는 라인
                    }
                    else if (1 == _midCount + 1)
                    {
                        // 왼쪽 향하는 라인 
                    }
                    // 부모 설정 
                }
            }
            else // 홀수라면 
            {
                for (int i = 0; i < _count; i++)
                {
                    if (i == _midCount)
                    {
                        // I 생성 
                    }
                    else if (i < _midCount)
                    {
                        // r 생성
                    }
                    else if (i > _midCount)
                    {
                        // ㄱ 새엇ㅇ  
                    }
                    // 부모 설정 

                }
            }
        }

        /// <summary>
        /// 줄 생성 
        /// </summary>
        private void CreateRow()
        {
            VisualElement _row = AddressablesManager.Instance.GetResource<VisualTreeAsset>("UpgradeRow").Instantiate().ElementAt(0);
            this.upgradeView.SetParentSlot(_row);
            this.rowList.Add(_row);
        }

        public bool ActiveView()
        {
            bool _isActive = upgradeView.ActiveScreen();
            if (_isActive == true)
            {
                ClearAllSlots();
                this.UpgradeCtrlPr.UpdateUI();
                StartCoroutine(SetAllSlotPosCo());
            }
            return _isActive;
        }

        public void ActiveView(bool _isActive)
        {
            upgradeView.ActiveScreen(_isActive);
        }
    }

}

