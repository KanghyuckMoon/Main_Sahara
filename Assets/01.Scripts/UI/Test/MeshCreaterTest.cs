using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Test
{
    public class MeshCreaterTest : MonoBehaviour
    {
        private UIDocument uiDoc;
        private VisualElement root;
        private void Awake()
        {
            uiDoc = GetComponent<UIDocument>();
            root = uiDoc.rootVisualElement; 
        }
        void Start()
        {
            MeshGenerationContext m;
            MeshWriteData mw; 
            VisualElement v; 
            
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                TexturedElement _t = new TexturedElement();
                root.Add(_t); 
            }
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
           // m_Texture = AddressablesManager.Instance.GetResource<Texture2D>("Demon");
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
