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

        private UpgradePickPresenter upgradePickPresenter; // ���� ���ý� ��Ÿ�� ���巹�̵� �г� 
        private UpgradeCtrlPresenter ctrlPresenter; // �¿� ��ư , ��� �� ���� Pr 
        private UpgradeSlotPresenter _curSlotPr; // ���� ������ ����
        private ElementCtrlComponent elementCtrlComponent; // ������ Ȯ�� ���

        private List<VisualElement> rowList = new List<VisualElement>(); // �� ����Ʈ 
        private List<UpgradeSlotPresenter> allSlotList = new List<UpgradeSlotPresenter>(); // ��� ���� ����Ʈ 

        private Dictionary<int, List<Vector2>> slotPosDIc = new Dictionary<int, List<Vector2>>(); // ��� ���� ���� ��ġ ��ųʸ� 
        private Queue<UpgradeSlotData> itemDataQueue = new Queue<UpgradeSlotData>(); // ������ ������ ���� ť 
        private List<UpgradeSlotData> allItemDataList = new List<UpgradeSlotData>(); // ���� Ʈ���� ��� ������ ����Ʈ
        private List<VisualElement> allItemList = new List<VisualElement>(); // ���� Ʈ���� ��� ������ ����Ʈ 

        private Dictionary<VisualElement, List<VisualElement>> parentSlotDic =
            new Dictionary<VisualElement, List<VisualElement>>();

        private float midX; // �߽� ��ǥ 
        private float midY; // �߽� ��ǥ 
        private bool isFirstSlot = true;

        // ������Ƽ 
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
                    Logging.Log("@@@@@@@@@@@���");
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
            // UIToolkit�� ���̾ƿ� ���� �ð��� �ҿ�Ǳ� ������
            // �ٸ� element�� worldbound�� ����� �������� ���ؼ��� ����� �ð��� ������ �����;��� 
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

            // ����� Ŭ���ϸ� ���׷��̵� UI ���������� 
            //upgradeView.Parent.RegisterCallback<ClickEvent>((x) =>
            //{
            //    upgradePickPresenter.ActiveView(false);
            //    InActiveAllMark();
            //},TrickleDown.TrickleDown);

            upgradePickPresenter = new UpgradePickPresenter(upgradeView.UpgradePickParent);
            upgradePickPresenter.SetButtonEvent(() =>
            {
                ItemUpgradeManager.Instance.Upgrade(_curSlotPr.ItemData.key);
                Logging.Log("���׷��̵� Ŭ��");
            });

            rowList.Clear();
            slotPosDIc.Clear();
            allSlotList.Clear();

            InitDic();

            Logging.Log("@@@@@@@@@@@���");
            ctrlPresenter = new UpgradeCtrlPresenter(upgradeView.Parent, CreateItemTree);
        }

        [SerializeField]
        private bool isComplete = false; // �������� UI ������ �Ϸ� �Ǿ��°� ( �ȵ����� ���� �Ұ� )
        private void LateUpdate()
        {
            if (isComplete == true)
            {
                ElementCtrlComponent.Update();
            }
            if (isConnection == true)
            {
                LineCreateManager.Instance.UpdateLinesPos(ScreenType.Upgrade,
                    upgradeView.MoveScreen.transform.position);
                LineCreateManager.Instance.UpdateLinesScale(ScreenType.Upgrade, upgradeView.MoveScreen.transform.scale);
            }
            // �׽�Ʈ 
            if(Input.GetKeyDown(KeyCode.X))
            {
                CreateItemTree(testItemDataSO);
   
            }
        }

        #region Init

        private float slotDist = 300f; // ���� ���� �Ÿ� 

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

            this.slotPosDIc.Add(1, _oneList);
            this.slotPosDIc.Add(2, _twoList);
            this.slotPosDIc.Add(3, _threeList);
            this.slotPosDIc.Add(4, _fourList);
        }

        #endregion

        [SerializeField] private ItemDataSO testItemDataSO;

        [ContextMenu("�׽�Ʈ")]
        public void Test()
        {
            CreateItemTree(testItemDataSO);
        }

        [ContextMenu("������ Ʈ�� ����")]
        /// <summary>
        ///  ������ Ʈ�� UI ���� �� ������ �ֱ� 
        /// </summary>
        public void CreateItemTree(ItemDataSO _itemDataSO)
        {
            Logging.Log("������ Ʈ�� ���� ����");
            // �������� Ʈ�� ���� 
            elementCtrlComponent.StopTween(); 
            StopAllCoroutines();    
            // ������ �մٸ� ���� 
            LineCreateManager.Instance.DestroyLine(ScreenType.Upgrade);
            // ������ UI ����
            ClearAllSlots();
           //
           this.ElementCtrlComponent.ResetPosAndZoom();
           this.ElementCtrlComponent.SetPos(Vector2.zero);
           //allItemDataList.Clear();
            ItemData _itemData = ItemData.CopyItemDataSO(_itemDataSO);
            itemDataQueue.Enqueue(new UpgradeSlotData(null, _itemData, 0, 1));

            CreateTree(); // ��� �� Ʈ�� ���� 
            StartCoroutine(SetAllSlotPosCo());
        }

        /// <summary>
        /// ��� ���Ե� ����  
        /// </summary>
        /// <param name="_itemUpgradeDataSO"></param>
        private void CreateTree()
        {
            List<UpgradeSlotData> _slotDataList = new List<UpgradeSlotData>(); // ��� ���� ������ ����Ʈ (������ ������ �ʿ�)
            List<ItemData> _list = new List<ItemData>(); // �� ���⿡�� �ʿ��� ��ṫ��� 

            CreateRow(); // ó���� ���� 
            int _count = itemDataQueue.Count();
            int _index = 0;

            while (itemDataQueue.Count > 0)
            {
                if (_index >= _count) // �� �� ���� �� ���� �� ���� 
                {
                    CreateRow(); // �� ����(��ĭ) 
                    //CreateConnection(_slotDataList);
                    _count = itemDataQueue.Count;
                    _index = 0;
                    _slotDataList.Clear();
                    CreateRow();
                }

                UpgradeSlotData _slotData = itemDataQueue.Dequeue(); // ť���� ������ ������ 
                ItemData _itemData = _slotData.itemData;
                VisualElement _parent = CreateSlot(_itemData); // ���� ����
                _parent.style.opacity = 0f;
                allItemList.Add(_parent);


                //// ��ġ ���� 
                if (isFirstSlot == true) // ���� �������̸� ��� ���� ���� 
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

                // ������ ���� ���� �θ� �ڽ� ���� ���� 
                if (_slotData.parentSlot != null)
                {
                    this.parentSlotDic[_slotData.parentSlot].Add(_parent);
                }
                
                ItemUpgradeDataSO _childItemData = ItemUpgradeManager.Instance.GetItemUpgradeDataSO(_itemData.key);
                if (_childItemData != null) // ������� �����Ѵٸ� ť�� �߰� 
                {
                    var _dataList = ItemUpgradeManager.Instance.UpgradeItemSlotList(_itemData.key);
                    
                    // ���⸸ ���� 
                    int _idx = 0; // �� ��° ���������� ( Ʈ�� �߿� )  ���� �� ������ 

                    var _weaponDList = _dataList.Where((x) => x.itemType == ItemType.Weapon).ToList();
                    if (_weaponDList.Count != 0)
                    {
                        // ������ ���� ���� �߰� 
                        this.parentSlotDic.Add(_parent, new List<VisualElement>());
                    }

                    _weaponDList.ForEach((x) =>
                    {
                        UpgradeSlotData _slotData = new UpgradeSlotData(_parent, x, _idx, _weaponDList.Count);
                        itemDataQueue.Enqueue(_slotData);
                        allItemDataList.Add(_slotData);

                        _slotDataList.Add(_slotData); // ������ ������ �ʿ� 
                        ++_idx;
                    });
                }

                ++_index;
            }
        }

        [SerializeField]
        private bool isActive = false; // ������ ������ Ȱ��ȭ ���� 
        [SerializeField]
        private bool isConnection = false; // ������ ���� ���� 

        private int a = 0; 
        [ContextMenu("���")]
        private IEnumerator SetAllSlotPos()
        {
            WaitForSecondsRealtime _w = new WaitForSecondsRealtime(0.05f); 

            int _idx = 1;
            allItemList[0].style.left = midX - allItemList[0].resolvedStyle.width / 2;
            allItemList[0].style.opacity = isConnection ? 1 : 0;

            foreach (var _v in allItemDataList)
            {
                Debug.Log("��ȸ ����");

                float _moveX = slotPosDIc[_v.maxIndex].ElementAt(_v.index).x;
                
                //float _op = _v.parentSlot.worldBound.x - _v.parentSlot.resolvedStyle.left; //  slot�� relative�̱� ������ left == 0 �ε� world bound�� 200 �� �� �ִ�. 
                //if (isConnection == false)
                //{
                allItemList[_idx].style.left =
                    _moveX + _v.parentSlot.worldBound.x + upgradeView.MoveScreen.transform.position.x;
                        //*upgradeView.MoveScreen.transform.scale.x;
                //}

                //yield return new WaitForSeconds(0.05f); 
                // opacity ����
                //DOTween.To(() =>0f, x => allItemList[_idx].style.opacity = x, _op, 0.5f).SetDelay(0.5f); 
                allItemList[_idx].style.opacity = isConnection ? 1 : 0;
                
                if (isConnection == true)
                {
                    Debug.Log("���� ����");

                    if (allItemList[_idx].worldBound.y + allItemList[_idx].resolvedStyle.height + 100 >
                        upgradeView.MoveScreen.resolvedStyle.height)
                        {
                        elementCtrlComponent.TweenMove(new Vector2(0,-200));
                        }
                    yield return _w;
                    Debug.Log("���� ��ġ ����");
                    float _slotL = allItemList[_idx].style.left.value.value;
                    float _slotB = allItemList[_idx].worldBound.x;
                    float _r1 = _slotB - _slotL;
                    float _r2 = _slotL - _r1; 
                    float _targetMoveX =  _r2; 
                    allItemList[_idx].style.left = _targetMoveX;
                    yield return _w;
                    Debug.Log("�� ����");
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

        [ContextMenu("����")]
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
        /// ���� Ŭ���� ���׷��̵� UI ǥ�� 
        /// </summary>
        private void ActiveUpgradePn(UpgradeSlotPresenter _upgradePr)
        {
            Debug.Log("Ŭ��");

            upgradePickPresenter.ClearSlots(); // �ִ��� �ʱ�ȭ ���ְ� 
            InActiveAllMark(); // ��� ���� ��ũ ��Ȱ��ȭ 
            _upgradePr.ActiveMark(true);

            ItemUpgradeDataSO _childItemData =
                ItemUpgradeManager.Instance.GetItemUpgradeDataSO(_upgradePr.ItemData.key); //
            if (_childItemData == null) // ������� ������ 
            {
                upgradePickPresenter.ActiveView(false);
                return;
            }

            // ��ġ ����(�ؾ� ��) 
            Rect _r3 = _upgradePr.Element1.worldBound;
            _upgradePr.Element1.Add(upgradePickPresenter.Parent);
            //upgradePickPresenter.SetPos(new Vector2(_r3.width / 2, _r3.y));
            upgradePickPresenter.SetPos(upgradeView.MoveScreen.resolvedStyle.scale.value.x);


            // �ʿ� ���� ǥ�� 
            int _idx = 0;
            var _list = ItemUpgradeManager.Instance.UpgradeItemSlotList(_childItemData.key)
                .Where((x) => x.itemType != ItemType.Weapon).ToList();
            foreach (var _data in _list)
            {
                UpgradeSlotPresenter _newUpgradePr = new UpgradeSlotPresenter();
                _newUpgradePr.SetItemDataHave(_data);
                upgradePickPresenter.SetSlotParent(_newUpgradePr);
            }

            upgradePickPresenter.ActiveView(true);
        }

        /// <summary>
        /// ���ý� Ȱ��ȭ ��ũ ��κ�Ȱ��ȭ�ϱ� 
        /// </summary>  
        private void InActiveAllMark()
        {
            this.allSlotList.ForEach((x) => x.ActiveMark(false));
        }

        private void CreateConnection(VisualElement _targeSlot /*List<UpgradeSlotData> _slotList*/)
        {
            Debug.Log("������ ����");
            List<Vector2> _pointList = new List<Vector2>();
            Vector2 _startPoint, _midPoint, _midPoint2, _targetPoint;
            //CreateRow(); // �� ���� 
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

                        _startPoint = new Vector2(_slotX - midX, _slotY - midY); // �θ� ��ġ 
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
        /// �� ���� 
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
            upgradeView.ActiveScreen(_isActive);
        }
    }
}