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

namespace UI.Upgrade
{
    public class UpgradePresenter : MonoBehaviour, IScreen
    {
        private UIDocument uiDocument;

        [SerializeField]
        private UpgradeView upgradeView;

        private UpgradePickPresenter upgradePickPresenter;
        private UpgradeSlotPresenter _curSlotPr; // ���� ������ ����

        private List<VisualElement> rowList = new List<VisualElement>(); // �� ����Ʈ 
        private List<UpgradeSlotPresenter> allSlotList = new List<UpgradeSlotPresenter>(); // ��� ���� ����Ʈ 

        private ElementCtrlComponent elementCtrlComponent; // ������ Ȯ�� ��� 
        private Dictionary<int, List<Vector2>> slotPosDIc = new Dictionary<int, List<Vector2>>();
        private Queue<UpgradeSlotData> itemDataQueue = new Queue<UpgradeSlotData>();
        private List<UpgradeSlotData> allItemDataList = new List<UpgradeSlotData>();
        private List<VisualElement> allItemList = new List<VisualElement>();

        private float midX; // �߽� ��ǥ 
        private bool isFirstSlot = true;

        // ������Ƽ 
        public IUIController UIController { get; set; }
        private VisualElement CurRow => rowList[rowList.Count - 1];

        private void Start()
        {
            elementCtrlComponent = new ElementCtrlComponent(upgradeView.MoveScreen);
            StartCoroutine(Co());
        }
        IEnumerator Co()
        {
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

            //upgradeView.Parent.RegisterCallback<ClickEvent>((x) =>
            //{
            //    upgradePickPresenter.ActiveView(false);
            //    InActiveAllMark();
            //},TrickleDown.TrickleDown);

            upgradePickPresenter = new UpgradePickPresenter(upgradeView.UpgradePickParent);
            upgradePickPresenter.SetButtonEvent(() => ItemUpgradeManager.Instance.Upgrade(_curSlotPr.ItemData.key));


            rowList.Clear();
            _vList.Clear(); 
            slotPosDIc.Clear();
            allSlotList.Clear(); 

            InitVList();
            InitDic();

            CreateItemTree();
        }

        private void LateUpdate()
        {
            elementCtrlComponent.Update();
        }

        private float slotDist = 300f; // ���� ���� �Ÿ� 
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
        [ContextMenu("������ Ʈ�� ����")]
        /// <summary>
        ///  ������ Ʈ�� UI ���� �� ������ �ֱ� 
        /// </summary>
        public void CreateItemTree()
        {
            // ������ UI ����
            //CreateRow();
            allItemDataList.Clear(); 
            ItemData _itemData = ItemData.CopyItemDataSO(AddressablesManager.Instance.GetResource<ItemDataSO>("UItem1"));
            itemDataQueue.Enqueue(new UpgradeSlotData(new VisualElement(), _itemData, 0, 1));
            // CreateSlot(_itemData);

            //StartCoroutine(CreateChildItem());
            CreateChildItem();  
        }

        private bool isEnd = false;
        /// <summary>
        /// ��� ���Ե� ����  
        /// </summary>
        /// <param name="_itemUpgradeDataSO"></param>
        void CreateChildItem()
        {
            var _slotDataList = new List<ItemData>(); // ��� ���� ������ ����Ʈ 
            List<ItemData> _list = new List<ItemData>(); // �� ���⿡�� �ʿ��� ��ṫ��� 

            CreateRow();
            int _count = itemDataQueue.Count();
            int _i = 0;

            while (itemDataQueue.Count > 0)
            {
                if (_i >= _count) // �� �� ���� �� ���� �� ���� 
                {
                    CreateConnection(_slotDataList);
                    _count = itemDataQueue.Count;
                    _i = 0;
                    CreateRow();
                }

                UpgradeSlotData _slotData = itemDataQueue.Dequeue();
                ItemData _itemData = _slotData.itemData;
                VisualElement _parent = CreateSlot(_itemData); // ���� ����
                _parent.style.opacity = 0f; 
                allItemList.Add(_parent);

                // _parent.RegisterCallback<GeometryChangedEvent>((x) =>
                // {
                //isEnd = true;
                //// ��ġ ���� 
                if (isFirstSlot == true)
                {
                    _parent.style.left = midX;
                    isFirstSlot = false;
                }
                else
                {
                    _parent.style.left = _slotData.parentSlot.worldBound.x + slotPosDIc[_slotData.maxIndex].ElementAt(_slotData.index).x;
                }

                ItemUpgradeDataSO _childItemData = ItemUpgradeManager.Instance.GetItemUpgradeDataSO(_itemData.key);
                    if (_childItemData != null) // ������� �����Ѵٸ� ť�� �߰� 
                    {
                        var _dataList = ItemUpgradeManager.Instance.UpgradeItemSlotList(_itemData.key);

                        // ���⸸ ���� 
                        int _idx = 0;
                        var _weaponDList = _dataList.Where((x) => x.itemType == ItemType.Weapon).ToList();
                        _weaponDList.ForEach((x) =>
                        {
                            UpgradeSlotData _slotData = new UpgradeSlotData(_parent, x, _idx, _weaponDList.Count);
                            itemDataQueue.Enqueue(_slotData);
                            allItemDataList.Add(_slotData); 
                            ++_idx;
                        });

                        _slotDataList.AddRange(_dataList);
                    }
                    ++_i;

                //});


                //while (true)
                //{
                //    if (isEnd == false)
                //        yield return null;
                //    else
                //    {
                //        isEnd = false;
                //        break;
                //    }
                //}

            }

        }

        private bool isActive = false; 
        [ContextMenu("���")]
        private void SetAllSlotPos()
        {
            int _idx = 1;
            allItemList[0].style.left = midX;
            allItemList[0].style.opacity = isActive ? 1 : 0;

            foreach (var _v in allItemDataList)
            {
                float _moveX =  slotPosDIc[_v.maxIndex].ElementAt(_v.index).x;
                _moveX = _moveX < 0 ? _moveX - allItemList[_idx].resolvedStyle.width : _moveX;
                allItemList[_idx].style.left = _moveX + _v.parentSlot.worldBound.x;

                // opacity ����
                allItemList[_idx].style.opacity = isActive ? 1 : 0; 
                ++_idx;
            }
            isActive = true; 
        }

        [ContextMenu("��Ȱ��ȭ")]
        public void InActiveAll()
        {
            foreach (var _v in allItemList)
            {
                // opacity ����
                _v.style.opacity = 0;
            }
        }

        [ContextMenu("Ȱ��ȭ")]
        public void ActiveAll()
        {
            foreach (var _v in allItemList)
            {
                // opacity ����
                _v.style.opacity = 1;
            }
        }

        [ContextMenu("����")]
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
            UpgradeSlotPresenter _upgradePr = new UpgradeSlotPresenter();
            _upgradePr.AddClickEvent(
                () =>
                {
                    ActiveUpgradePn(_upgradePr);
                    _curSlotPr = _upgradePr;
                }
            );

            //this.upgradeView.SetParentSlot(_upgradePr.Parent);
            allSlotList.Add(_upgradePr);
            this.CurRow.Add(_upgradePr.Parent.ElementAt(0));
            _upgradePr.SetItemData(_itemData);

            return _upgradePr.Element1;
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

            ItemUpgradeDataSO _childItemData = ItemUpgradeManager.Instance.GetItemUpgradeDataSO(_upgradePr.ItemData.key); //
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
            var _list = ItemUpgradeManager.Instance.UpgradeItemSlotList(_childItemData.key).Where((x) => x.itemType != ItemType.Weapon).ToList();
            foreach (var _data in _list)
            {
                UpgradeSlotPresenter _newUpgradePr = new UpgradeSlotPresenter();
                _newUpgradePr.SetItemDataHave(_data);
                upgradePickPresenter.SetSlotParent(_newUpgradePr);
            }

            //foreach (var _data in _list)
            //{
            //    UpgradeSlotPresenter _newUpgradePr = new UpgradeSlotPresenter();
            //    _newUpgradePr.SetPositionType(true);
            //    _newUpgradePr.SetItemDataHave(_data);
            //    _newUpgradePr.Parent.transform.position = _vList[_idx];
            //    upgradePickPresenter.SetAbsoluteParent(_newUpgradePr);
            //    _idx++; 
            //}
            upgradePickPresenter.ActiveView(true);
        }

        private List<Vector2> _vList = new List<Vector2>();

        private void InitVList()
        {
            float _dist = 200f;
            _vList.Add(new Vector2(-_dist, _dist));
            _vList.Add(new Vector2(-_dist, 0f));
            _vList.Add(new Vector2(-_dist, -_dist));
            _vList.Add(new Vector2(0f, -_dist));
            _vList.Add(new Vector2(_dist, -_dist));
            _vList.Add(new Vector2(_dist, 0f));
            _vList.Add(new Vector2(_dist, _dist));
            _vList.Add(new Vector2(0f, _dist));
        }

        /// <summary>
        /// �ռ� ��ư�� �Լ� �߰� 
        /// </summary>
        public void AddUpgradeBtnEvent()
        {
            // �κ��丮���� �ʿ� ���� ��ŭ ���̰� 
            // ���� �ִ°� ��ŭ ȹ�� 

            //upgradePickPresenter.I
        }

        /// <summary>
        /// ���ý� Ȱ��ȭ ��ũ ��κ�Ȱ��ȭ�ϱ� 
        /// </summary>
        private void InActiveAllMark()
        {
            this.allSlotList.ForEach((x) => x.ActiveMark(false));
        }
        private void CreateConnection(List<ItemData> _slotList)
        {
            CreateRow(); // �� ���� 
            int _count = _slotList.Count; // �ڽ� ���� 
            int _midCount = (_count / 2); // �߰� ���� 
            if (_count % 2 == 0) //  ¦�����
            {
                for (int i = 0; i < _count; i++)
                {
                    if (i < _midCount)
                    {
                        // �������� ����
                    }
                    else if (i < _midCount)
                    {
                        // �� ����
                    }
                    else if (i == _midCount - 1)
                    {
                        // ������ ���ϴ� ����
                    }
                    else if (1 == _midCount + 1)
                    {
                        // ���� ���ϴ� ���� 
                    }
                    // �θ� ���� 
                }
            }
            else // Ȧ����� 
            {
                for (int i = 0; i < _count; i++)
                {
                    if (i == _midCount)
                    {
                        // I ���� 
                    }
                    else if (i < _midCount)
                    {
                        // r ����
                    }
                    else if (i > _midCount)
                    {
                        // �� ������  
                    }
                    // �θ� ���� 

                }
            }
        }

        /// <summary>
        /// �� ���� 
        /// </summary>
        private void CreateRow()
        {
            VisualElement _row = AddressablesManager.Instance.GetResource<VisualTreeAsset>("UpgradeRow").Instantiate().ElementAt(0);
            this.upgradeView.SetParentSlot(_row);
            this.rowList.Add(_row);
        }

        public bool ActiveView()
        {
                ClearAllSlots();
                CreateItemTree();
                StartCoroutine(Co());
            return upgradeView.ActiveScreen();
        }

        public void ActiveView(bool _isActive)
        {
            upgradeView.ActiveScreen(_isActive);
        }
    }



}

