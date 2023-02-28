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

        private UpgradePickPresenter upgradePickPresenter; // ���� ���ý� ��Ÿ�� ���巹�̵� �г� 
        private UpgradeCtrlPresenter ctrlPresenter; // �¿� ��ư , ��� �� ���� Pr 
        private UpgradeSlotPresenter _curSlotPr; // ���� ������ ����

        private List<VisualElement> rowList = new List<VisualElement>(); // �� ����Ʈ 
        private List<UpgradeSlotPresenter> allSlotList = new List<UpgradeSlotPresenter>(); // ��� ���� ����Ʈ 

        [SerializeField]
        private ElementCtrlComponent elementCtrlComponent; // ������ Ȯ�� ��� 
        private Dictionary<int, List<Vector2>> slotPosDIc = new Dictionary<int, List<Vector2>>(); // ��� ���� ���� ��ġ ��ųʸ� 
        private Queue<UpgradeSlotData> itemDataQueue = new Queue<UpgradeSlotData>(); // ������ ������ ���� ť 
        private List<UpgradeSlotData> allItemDataList = new List<UpgradeSlotData>(); // ���� Ʈ���� ��� ������ ����Ʈ
        private List<VisualElement> allItemList = new List<VisualElement>(); // ���� Ʈ���� ��� ������ ����Ʈ 

        private float midX; // �߽� ��ǥ 
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
            // UIToolkit�� ���̾ƿ� ���� �ð��� �ҿ�Ǳ� ������
            // �ٸ� element�� worldbound�� ����� �������� ���ؼ��� ����� �ð��� ������ �����;��� 
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

            // ����� Ŭ���ϸ� ���׷��̵� UI ���������� 
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

        #endregion

        [ContextMenu("������ Ʈ�� ����")]
        /// <summary>
        ///  ������ Ʈ�� UI ���� �� ������ �ֱ� 
        /// </summary>
        public void CreateItemTree(ItemDataSO _itemDataSO)
        {
            // ������ UI ����
            allItemDataList.Clear();
            ItemData _itemData = ItemData.CopyItemDataSO(_itemDataSO);
            itemDataQueue.Enqueue(new UpgradeSlotData(new VisualElement(), _itemData, 0, 1));

            CreateTree(); // ��� �� Ʈ�� ���� 
            this.ElementCtrlComponent.ResetPosAndZoom();
        }

        /// <summary>
        /// ��� ���Ե� ����  
        /// </summary>
        /// <param name="_itemUpgradeDataSO"></param>
        private void CreateTree()
        {
            List<ItemData> _slotDataList = new List<ItemData>(); // ��� ���� ������ ����Ʈ (������ ������ �ʿ�)
            List<ItemData> _list = new List<ItemData>(); // �� ���⿡�� �ʿ��� ��ṫ��� 

            CreateRow(); // ó���� ���� 
            int _count = itemDataQueue.Count();
            int _index = 0;

            while (itemDataQueue.Count > 0)
            {
                if (_index >= _count) // �� �� ���� �� ���� �� ���� 
                {
                    CreateConnection(_slotDataList);
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
                    _parent.style.left = midX - _parent.resolvedStyle.width ;
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
                    int _idx = 0; // �� ��° ���������� ( Ʈ�� �߿� )  ���� �� ������ 
                    var _weaponDList = _dataList.Where((x) => x.itemType == ItemType.Weapon).ToList();
                    _weaponDList.ForEach((x) =>
                    {
                        UpgradeSlotData _slotData = new UpgradeSlotData(_parent, x, _idx, _weaponDList.Count);
                        itemDataQueue.Enqueue(_slotData);
                        allItemDataList.Add(_slotData);
                        ++_idx;
                    });

                    _slotDataList.AddRange(_dataList); // ������ ������ �ʿ� 
                }
                ++_index;
            }

        }
        private bool isActive = false; // ������ ������ Ȱ��ȭ ���� 

        [ContextMenu("���")]
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

                // opacity ����
                float _op = isActive ? 1 : 0;
                //DOTween.To(() =>0f, x => allItemList[_idx].style.opacity = x, _op, 0.5f).SetDelay(0.5f); 
                allItemList[_idx].style.opacity = isActive ? 1 : 0;
                ++_idx;
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
            upgradePickPresenter.ActiveView(true);
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

