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

        private List<VisualElement> rowList = new List<VisualElement>(); // �� ����Ʈ 

        // ������Ƽ 
        private VisualElement CurRow => rowList[rowList.Count - 1];
        private void OnEnable()
        {
            uiDocument = GetComponent<UIDocument>();
            upgradeView.InitUIDocument(uiDocument);
            upgradeView.Cashing();
            upgradeView.Init();

            TestLine(); 

        }

        [ContextMenu("Line ���� �׽�Ʈ")]
        public void TestLine()
        {
            upgradeView.SetParentSlot(new LineDrawer(new Vector2(20, 50), new Vector2(50, 50), 10));
            upgradeView.SetParentSlot(new TexturedElement());
        }
        [ContextMenu("������ Ʈ�� ����")]
        /// <summary>
        ///  ������ Ʈ�� UI ���� �� ������ �ֱ� 
        /// </summary>
        public void CreateItemTree()
        {
            // ������ UI ����
            CreateRow();
            ItemData _itemData= ItemData.CopyItemDataSO(AddressablesManager.Instance.GetResource<ItemDataSO>("UItem1"));
            CreateSlot(_itemData);

            CreateChildItem(ItemUpgradeManager.Instance.GetItemUpgradeDataSO(_itemData.key));
        }

        private void CreateChildItem(ItemUpgradeDataSO _itemUpgradeDataSO)
        {
            CreateRow(); // �� ���� 
            var _slotList = ItemUpgradeManager.Instance.UpgradeItemSlotList(_itemUpgradeDataSO.key);
            // ������ ���� 
            //CreateConnection(_slotList); 

            // ���� ����
            foreach (var _item in _slotList)
            {
                CreateSlot(_item);
                ItemUpgradeDataSO _childItemData = ItemUpgradeManager.Instance.GetItemUpgradeDataSO(_item.key);
                if (_childItemData != null) // ������� �����Ѵٸ� 
                {
                    // ���⼭�� ����
                    CreateChildItem(_childItemData);
                }
            }
        }

        public void CreateSlot(ItemData _itemData)
        {
            UpgradeSlotPresenter _upgradePr = new UpgradeSlotPresenter();
            //this.upgradeView.SetParentSlot(_upgradePr.Parent);
            this.CurRow.Add(_upgradePr.Parent); 
            _upgradePr.SetItemData(_itemData);
        }

        private void CreateConnection(List<VisualElement> _slotList)
        {
            int _count = _slotList.Count; // �ڽ� ���� 
            if (_count % 2 == 0) //  ¦�����
            {
                for (int i = 0; i < _count; i++)
                {
               //    if (i < _count / 2)
                       // _slotList[i]
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// �� ���� 
        /// </summary>
        private void CreateRow()
        {
            VisualElement _row = AddressablesManager.Instance.GetResource<VisualTreeAsset>("UpgradeRow").Instantiate();
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

