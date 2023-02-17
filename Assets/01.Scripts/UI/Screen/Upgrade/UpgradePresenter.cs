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
        private List<VisualElement> rowList = new List<VisualElement>(); // 줄 리스트 
        private List<UpgradeSlotPresenter> allSlotList = new List<UpgradeSlotPresenter>(); // 모든 슬롯 리스트 

        private ElementCtrlComponent elementCtrlComponent; // 움직임 확대 축소 

        // 프로퍼티 
        private VisualElement CurRow => rowList[rowList.Count - 1];

        private void Start()
        {
            elementCtrlComponent = new ElementCtrlComponent(upgradeView.Parent); 
        }
        private void OnEnable()
        {
            uiDocument = GetComponent<UIDocument>();
            upgradeView.InitUIDocument(uiDocument);
            upgradeView.Cashing();
            upgradeView.Init();

            upgradeView.Parent.RegisterCallback<ClickEvent>((x) =>
            {
                upgradePickPresenter.ActiveView(false);
                InActiveAllMark();
            });

                upgradePickPresenter = new UpgradePickPresenter(upgradeView.UpgradePickParent);
            upgradePickPresenter.SetButtonEvent(() => ItemUpgradeManager.Instance.Upgrade(_curSlotPr.ItemData.key));

            InitVList(); 

            CreateItemTree();
        }

        private void Update()
        {
            elementCtrlComponent.Update(); 
        }

        [ContextMenu("Line 생성 테스트")]
        public void TestLine()
        {
            upgradeView.SetParentSlot(new LineDrawer(new Vector2(20, 50), new Vector2(50, 50), 10));
            upgradeView.SetParentSlot(new TexturedElement());
        }

        [ContextMenu("아이템 트리 생성")]
        /// <summary>
        ///  아이템 트리 UI 생성 및 데이터 넣기 
        /// </summary>
        public void CreateItemTree()
        {
            // 최종템 UI 생성
            //CreateRow();
            ItemData _itemData = ItemData.CopyItemDataSO(AddressablesManager.Instance.GetResource<ItemDataSO>("UItem1"));
            itemDataQueue.Enqueue(_itemData); 
           // CreateSlot(_itemData);

            CreateChildItem();
        }

        private Queue<ItemData> itemDataQueue = new Queue<ItemData>();
        /// <summary>
        /// 재료 슬롯들 생성  
        /// </summary>
        /// <param name="_itemUpgradeDataSO"></param>
        private void CreateChildItem()
        {
            var _slotList = new List<ItemData>(); // 재료 슬롯 데이터 리스트 
            List<ItemData> _list = new List<ItemData>(); // 한 무기에서 필요한 재료무기들 

            CreateRow();
            int _count = itemDataQueue.Count();
            int _i = 0;

            while (itemDataQueue.Count > 0)
            {
                if (_i >= _count) // 한 줄 생성 끝 다음 줄 시작 
                {
                    CreateConnection(_slotList);
                    _count = itemDataQueue.Count;
                    _i = 0;
                    CreateRow();
                }
                ItemData _itemData = itemDataQueue.Dequeue();
                CreateSlot(_itemData); // 슬롯 생성

                ItemUpgradeDataSO _childItemData = ItemUpgradeManager.Instance.GetItemUpgradeDataSO(_itemData.key);
                if (_childItemData != null) // 재료템이 존재한다면 큐에 추가 
                {
                    var _dataList = ItemUpgradeManager.Instance.UpgradeItemSlotList(_itemData.key);
                    _dataList.ForEach((x) => itemDataQueue.Enqueue(x));
                    //_list.Add( _dataList);
                }
                ++_i;
            }
            // 줄 생성 
            // 큐 카운트 세 
            // 
            // 쭉 생성 

        }

        private UpgradeSlotPresenter _curSlotPr; // 현재 선택한 슬롯
        private void CreateSlot(ItemData _itemData)
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
            upgradePickPresenter.SetPos(new Vector2(_r3.x + _r3.width + 50f, _r3.y));

            // 필요 재료들 표시 
            int _idx = 0; 
            var _list = ItemUpgradeManager.Instance.UpgradeItemSlotList(_childItemData.key);
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
        /// 합성 버튼에 함수 추가 
        /// </summary>
        public void AddUpgradeBtnEvent()
        {
            // 인벤토리에서 필요 개수 만큼 줄이고 
            // 현재 있는거 만큼 획득 

            //upgradePickPresenter.I
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
            return upgradeView.ActiveScreen();
        }

        public void ActiveView(bool _isActive)
        {
            upgradeView.ActiveScreen(_isActive);
        }
    }

    class TexturedElement : VisualElement
    {
        static readonly Vertex[] k_Vertices = new Vertex[4];
        static readonly ushort[] k_Indices = { 0, 1, 2, 2, 3, 0 };

        static TexturedElement()
        {
            k_Vertices[0].tint = Color.white;
            k_Vertices[1].tint = Color.white;
            k_Vertices[2].tint = Color.white;
            k_Vertices[3].tint = Color.white;
        }

        public TexturedElement()
        {
            generateVisualContent += OnGenerateVisualContent;
            m_Texture = AddressablesManager.Instance.GetResource<Texture2D>("Demon");
        }

        Texture2D m_Texture;

        void OnGenerateVisualContent(MeshGenerationContext mgc)
        {
            Rect r = contentRect;
            r.height = 1000f;
            if (r.width < 0.01f || r.height < 0.01f)
                return; // Skip rendering when too small.

            float left = 0;
            float right = r.width;
            float top = 0;
            float bottom = r.height;

            k_Vertices[0].position = new Vector3(left, bottom, Vertex.nearZ);
            k_Vertices[1].position = new Vector3(left, top, Vertex.nearZ);
            k_Vertices[2].position = new Vector3(right, top, Vertex.nearZ);
            k_Vertices[3].position = new Vector3(right, bottom, Vertex.nearZ);

            MeshWriteData mwd = mgc.Allocate(k_Vertices.Length, k_Indices.Length, m_Texture);

            // Since the texture may be stored in an atlas, the UV coordinates need to be
            // adjusted. Simply rescale them in the provided uvRegion.
            Rect uvRegion = mwd.uvRegion;
            k_Vertices[0].uv = new Vector2(0, 0) * uvRegion.size + uvRegion.min;
            k_Vertices[1].uv = new Vector2(0, 1) * uvRegion.size + uvRegion.min;
            k_Vertices[2].uv = new Vector2(1, 1) * uvRegion.size + uvRegion.min;
            k_Vertices[3].uv = new Vector2(1, 0) * uvRegion.size + uvRegion.min;

            mwd.SetAllVertices(k_Vertices);
            mwd.SetAllIndices(k_Indices);
        }
    }

}

