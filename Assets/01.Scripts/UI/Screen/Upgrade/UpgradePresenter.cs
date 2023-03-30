using System;
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
using UI;
using UI.MapLiner;

namespace UI.Upgrade
{
    public class UpgradePresenter : MonoBehaviour, IScreen
    {
        private UIDocument uiDocument;

        [SerializeField] private UpgradeView upgradeView;

        private UpgradePickPresenter upgradePickPresenter; // 슬롯 선택시 나타날 업드레이드 패널 
        private UpgradeCtrlPresenter ctrlPresenter; // 좌우 버튼 , 상단 라벨 조작 Pr 
        private UpgradeSlotPresenter _curSlotPr; // 현재 선택한 슬롯
        private ElementCtrlComponent elementCtrlComponent; // 움직임 확대 축소

        private List<VisualElement> rowList = new List<VisualElement>(); // 줄 리스트 
        private List<UpgradeSlotPresenter> allSlotList = new List<UpgradeSlotPresenter>(); // 모든 슬롯 리스트 

        private Dictionary<int, List<Vector2>> slotPosDIc = new Dictionary<int, List<Vector2>>(); // 재료 수에 따른 위치 딕셔너리 
        private Queue<UpgradeSlotData> itemDataQueue = new Queue<UpgradeSlotData>(); // 생성할 아이템 저장 큐 
        private List<UpgradeSlotData> allItemDataList = new List<UpgradeSlotData>(); // 현재 트리의 모든 데이터 리스트
        private List<VisualElement> allItemList = new List<VisualElement>(); // 현재 트리의 모든 아이템 리스트 

        private Dictionary<VisualElement, List<VisualElement>> parentSlotDic =
            new Dictionary<VisualElement, List<VisualElement>>();

        private float midX; // 중심 좌표 
        private float midY; // 중심 좌표 
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
                    Logging.Log("@@@@@@@@@@@등록");
                    ctrlPresenter = new UpgradeCtrlPresenter(upgradeView.Parent, CreateItemTree);
                }

                return ctrlPresenter;
            }
        }

        private Vector2 MoveScreenV => upgradeView.MoveScreen.transform.position; 
        private void Start()
        {
            elementCtrlComponent = new ElementCtrlComponent(upgradeView.MoveScreen);
            //   StartCoroutine(SetAllSlotPosCo());
        }

        IEnumerator SetAllSlotPosCo()
        {
            // UIToolkit은 레이아웃 구축 시간이 소요되기 때문에
            // 다른 element의 worldbound를 제대로 가져오기 위해서는 구축될 시간이 지난후 가져와야함 
            elementCtrlComponent.IsInput = false;
            yield return new WaitForSecondsRealtime(0.05f);
            StartCoroutine(SetAllSlotPos());
            yield return new WaitForSecondsRealtime(0.05f);
            StartCoroutine(SetAllSlotPos());
            yield return new WaitForSecondsRealtime(0.05f);
            StartCoroutine(SetAllSlotPos());
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
            upgradePickPresenter.SetButtonEvent(() =>
            {
                ItemUpgradeManager.Instance.Upgrade(_curSlotPr.ItemData.key);
                Logging.Log("업그레이드 클릭");
            });

            rowList.Clear();
            slotPosDIc.Clear();
            allSlotList.Clear();

            InitDic();

            Logging.Log("@@@@@@@@@@@등록");
            ctrlPresenter = new UpgradeCtrlPresenter(upgradeView.Parent, CreateItemTree);
        }

        [SerializeField]
        private bool isComplete = false; // 대장장이 UI 생성이 완료 되었는가 ( 안됐으면 조작 불가 )

        private void Update()
        {
            if (isComplete == true)
            {
                ElementCtrlComponent.Update();
            }
        }

        private void LateUpdate()
        {

            if (isConnection == true)
            {
                LineCreateManager.Instance.UpdateLinesPos(ScreenType.Upgrade,
                    upgradeView.MoveScreen.transform.position);
                LineCreateManager.Instance.UpdateLinesScale(ScreenType.Upgrade, upgradeView.MoveScreen.parent.transform.scale);
            }
            // 테스트 
            if(Input.GetKeyDown(KeyCode.X))
            {
                CreateItemTree(testItemDataSO);
   
            }
        }

        #region Init

        private float slotDist = 250f; // 슬롯 간의 거리 

        private void InitDic()
        {
            midX = Screen.width / 2;
            midY = Screen.height / 2;

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

            List<Vector2> _fiveList = new List<Vector2>();
            _fiveList.Add(new Vector2(-slotDist * 2, 0));
            _fiveList.Add(new Vector2(-slotDist, 0));
            _fiveList.Add(new Vector2(0, 0));
            _fiveList.Add(new Vector2(slotDist, 0));
            _fiveList.Add(new Vector2(slotDist * 2, 0));
            
            List<Vector2> _sixList = new List<Vector2>();
            _sixList.Add(new Vector2(-slotDist * 3, 0));
            _sixList.Add(new Vector2(-slotDist * 2, 0));
            _sixList.Add(new Vector2(-slotDist, 0));
            _sixList.Add(new Vector2(slotDist, 0));
            _sixList.Add(new Vector2(slotDist * 2, 0));
            _sixList.Add(new Vector2(slotDist * 3, 0));
            
            this.slotPosDIc.Add(1, _oneList);
            this.slotPosDIc.Add(2, _twoList);
            this.slotPosDIc.Add(3, _threeList);
            this.slotPosDIc.Add(4, _fourList);
            this.slotPosDIc.Add(5, _fiveList);
            this.slotPosDIc.Add(6, _sixList);
        }

        #endregion

        [SerializeField] private ItemDataSO testItemDataSO;

        [ContextMenu("테스트")]
        public void Test()
        {
            CreateItemTree(testItemDataSO);
        }

        [ContextMenu("아이템 트리 생성")]
        /// <summary>
        ///  아이템 트리 UI 생성 및 데이터 넣기 
        /// </summary>
        public void CreateItemTree(ItemDataSO _itemDataSO)
        {
            Logging.Log("아이템 트리 생성 시작");
            // 실행중인 트윈 종료 
            elementCtrlComponent.StopTween(); 
            StopAllCoroutines();    
            // 연결점 잇다면 삭제 
            LineCreateManager.Instance.DestroyLine(ScreenType.Upgrade);
            // 최종템 UI 생성
            ClearAllSlots();
           //
           this.ElementCtrlComponent.ResetPosAndZoom();
           this.ElementCtrlComponent.SetPos(Vector2.zero);
           //allItemDataList.Clear();
            ItemData _itemData = ItemData.CopyItemDataSO(_itemDataSO);
            itemDataQueue.Enqueue(new UpgradeSlotData(null, _itemData, 0, 1));

            CreateTree(); // 재료 템 트리 생성 
            StartCoroutine(SetAllSlotPosCo());
        }

        /// <summary>
        /// 재료 슬롯들 생성  
        /// </summary>
        /// <param name="_itemUpgradeDataSO"></param>
        private void CreateTree()
        {
            List<UpgradeSlotData> _slotDataList = new List<UpgradeSlotData>(); // 재료 슬롯 데이터 리스트 (연결점 생성시 필요)
            List<ItemData> _list = new List<ItemData>(); // 한 무기에서 필요한 재료무기들 

            CreateRow(); // 처음줄 생성 
            int _count = itemDataQueue.Count();
            int _index = 0;
    
            while (itemDataQueue.Count > 0)
            {
                if (_index >= _count) // 한 줄 생성 끝 다음 줄 시작 
                {
                    CreateRow(); // 줄 생성(빈칸) 
                    //CreateConnection(_slotDataList);
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
                    _parent.style.left = midX - _parent.resolvedStyle.width;
                    isFirstSlot = false;
                }
                else
                {
                    if (_slotData.maxIndex >= 1)
                    {
                        Debug.Log("MaxIndex" + _slotData.maxIndex);
                    }

                    _parent.style.left = _slotData.parentSlot.worldBound.x +
                                         slotPosDIc[_slotData.maxIndex].ElementAt(_slotData.index).x;
                }

                // 연결점 생성 위해 부모 자식 관계 설정 
                if (_slotData.parentSlot != null)
                {
                    this.parentSlotDic[_slotData.parentSlot].Add(_parent);
                }
                
                ItemUpgradeDataSO _childItemData = ItemUpgradeManager.Instance.GetItemUpgradeDataSO(_itemData.key);
                if (_childItemData != null) // 재료템이 존재한다면 큐에 추가 
                {
                    var _dataList = ItemUpgradeManager.Instance.UpgradeItemSlotList(_itemData.key);
                    
                    // 무기만 생성 
                    int _idx = 0; // 몇 번째 아이템인지 ( 트리 중에 )  같은 줄 내에서 

                    var _weaponDList = _dataList.Where((x) => x.itemType == ItemType.Weapon).Where((x) => x.isSlot == true).ToList();
                    if (_weaponDList.Count != 0)
                    {
                        // 연결점 생성 위한 추가 
                        this.parentSlotDic.Add(_parent, new List<VisualElement>());
                    }

                    _weaponDList.ForEach((x) =>
                    {
                        UpgradeSlotData _slotData = new UpgradeSlotData(_parent, x, _idx, _weaponDList.Count);
                        itemDataQueue.Enqueue(_slotData);
                        allItemDataList.Add(_slotData);

                        _slotDataList.Add(_slotData); // 연결점 생성시 필요 
                        ++_idx;
                    });
                }

                ++_index;
            }
        }

        [SerializeField]
        private bool isActive = false; // 포지션 설정시 활성화 여부 
        [SerializeField]
        private bool isConnection = false; // 연결점 생성 여부 

        private int a = 0; 
        [ContextMenu("고고")]
        private IEnumerator SetAllSlotPos()
        {
            WaitForSecondsRealtime _w = new WaitForSecondsRealtime(0.01f); 

            int _idx = 1;
            allItemList[0].style.left = midX - allItemList[0].resolvedStyle.width / 2;
            allItemList[0].style.opacity = isConnection ? 1 : 0;

            foreach (var _v in allItemDataList)
            {
                Debug.Log("순회 시작");

                float _moveX = slotPosDIc[_v.maxIndex].ElementAt(_v.index).x;
                
                //float _op = _v.parentSlot.worldBound.x - _v.parentSlot.resolvedStyle.left; //  slot이 relative이기 때문에 left == 0 인데 world bound는 200 일 수 있다. 
                //if (isConnection == false)
                //{
                allItemList[_idx].style.left =
                    _moveX + _v.parentSlot.worldBound.x + upgradeView.MoveScreen.transform.position.x;
                        //*upgradeView.MoveScreen.transform.scale.x;
                //}

                //yield return new WaitForSeconds(0.05f); 
                // opacity 설정
                //DOTween.To(() =>0f, x => allItemList[_idx].style.opacity = x, _op, 0.5f).SetDelay(0.5f); 
                allItemList[_idx].style.opacity = isConnection ? 1 : 0;
                
                if (isConnection == true)
                {
                    Debug.Log("생성 시작");

                    if (allItemList[_idx].worldBound.y + allItemList[_idx].resolvedStyle.height + 100 >
                        upgradeView.MoveScreen.resolvedStyle.height)
                        {
                        elementCtrlComponent.TweenMove(new Vector2(0,-200));
                        }
                    yield return _w;
                    Debug.Log("슬롯 위치 설정");
                    float _slotL = allItemList[_idx].style.left.value.value;
                    float _slotB = allItemList[_idx].worldBound.x;
                    float _r1 = _slotB - _slotL;
                    float _r2 = _slotL - _r1; 
                    float _targetMoveX =  _r2; 
                    allItemList[_idx].style.left = _targetMoveX;
                    yield return _w;
                    Debug.Log("선 생성");
                    CreateConnection(allItemList[_idx]);
                    yield return _w;
                }

                ++_idx;
            }
            if (isConnection == true)
            {
                isComplete = true; 
            }
            if (isActive == true)
            {
                isConnection = true;
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
            allItemDataList.Clear();

            this.upgradeView.ClearAllSlots();
            isActive = false;
            isConnection = false;
            isComplete = false; 
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
            _slotPr.SetItemDataHave(_itemData);
            // _slotPr.SetItemData(_itemData);

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

            ItemUpgradeDataSO _childItemData =
                ItemUpgradeManager.Instance.GetItemUpgradeDataSO(_upgradePr.ItemData.key); //
            if (_childItemData == null) // 재료템이 없으면 
            {
                upgradePickPresenter.ActiveView(false);
                return;
            }

            // 위치 설정(해야 해) 
            Rect _slotRect = _upgradePr.Element1.worldBound;
            Rect _screenRect = upgradeView.MoveScreen.worldBound;
            Vector2 _screenScale = upgradeView.MoveScreen.parent.transform.scale; 
            float _slotX = _upgradePr.Element1.resolvedStyle.width;
            upgradePickPresenter.SetPos(new Vector2(_slotX + (_slotRect.x - _screenRect.x) / _screenScale.x /*- _screenPos.x/2*/, 
                (_slotRect.y - _screenRect.y) / _screenScale.y /*+ _screenPos.y*/) );

            // 필요 재료들 표시 
            int _idx = 0;
            var _list = ItemUpgradeManager.Instance.UpgradeItemSlotList(_childItemData.key)
                .Where((x) => x.itemType != ItemType.Weapon && x.isSlot == true).ToList();
            ItemUpgradeManager.Instance.UpgradeItemSlotList(_childItemData.key)
                .Where((x) => x.isSlot == true ).ToList();
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

        private void CreateConnection(VisualElement _targeSlot /*List<UpgradeSlotData> _slotList*/)
        {
            Debug.Log("연결점 생성");
            List<Vector2> _pointList = new List<Vector2>();
            Vector2 _startPoint, _midPoint, _midPoint2, _targetPoint;
            //CreateRow(); // 줄 생성 
            foreach (var _slot in this.parentSlotDic)
            {
                foreach (var _slot2 in _slot.Value)
                {
                    if (_slot2 == _targeSlot)
                    {
                        _pointList.Clear();

                        float _slotX =  MoveScreenV.x + _slot.Key.worldBound.x + _slot.Key.resolvedStyle.width / 2;
                        float _slotY = -MoveScreenV.y +_slot.Key.worldBound.y + _slot.Key.resolvedStyle.height;
                        float _slot2X = MoveScreenV.x + _slot2.worldBound.x + _slot2.resolvedStyle.width / 2;
                        float _slot2Y = -MoveScreenV.y +_slot2.worldBound.y;

                        _startPoint = new Vector2(_slotX - midX, _slotY - midY); // 부모 위치 
                        _midPoint = new Vector2(_slotX - midX, _slotY + (_slot2Y - _slotY) / 2 - midY);
                        _midPoint2 = new Vector2(_slot2X - midX, _slotY + (_slot2Y - _slotY) / 2 - midY);
                        _targetPoint = new Vector2(_slot2X - midX, _slot2Y - midY);

                        _pointList.Add(_startPoint);
                        _pointList.Add(_midPoint);
                        _pointList.Add(_midPoint2);
                        _pointList.Add(_targetPoint);

                        var _line = LineCreateManager.Instance.CreateLine(ScreenType.Upgrade);
                        _line.UpdateMapLine(_pointList);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 줄 생성 
        /// </summary>
        private void CreateRow()
        {
            VisualElement _row = AddressablesManager.Instance.GetResource<VisualTreeAsset>("UpgradeRow").Instantiate()
                .ElementAt(0);
            this.upgradeView.SetParentSlot(_row);
            this.rowList.Add(_row);
        }

        public bool ActiveView()
        {
                bool _isActive = upgradeView.ActiveScreen();
            if (_isActive == true)
            {
                //ClearAllSlots();
                this.UpgradeCtrlPr.UpdateUI();
                //StartCoroutine(SetAllSlotPosCo());
            }
            else
            {
                LineCreateManager.Instance.DestroyLine(ScreenType.Upgrade);
            }

            return _isActive;
        }

        public void ActiveView(bool _isActive)
        {
            if (_isActive == true)
            {
                //ClearAllSlots();
                this.UpgradeCtrlPr.UpdateUI();
                //StartCoroutine(SetAllSlotPosCo());
            }
            else
            {
                LineCreateManager.Instance.DestroyLine(ScreenType.Upgrade);
            }
            upgradeView.ActiveScreen(_isActive);
        }
    }
}