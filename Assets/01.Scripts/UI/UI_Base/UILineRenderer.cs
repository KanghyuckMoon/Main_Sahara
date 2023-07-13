using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Utill.Measurement;


namespace UI.Base
{

    public class UILineRenderer : Graphic
    {
        public float lineWidth = 2.0f;
        public Vector2[] points;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            Logging.Log("dd");
            vh.Clear();

            if (points == null || points.Length < 2)
                return;

            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector2 startPos = points[i];
                Vector2 endPos = points[i + 1];

                AddLine(vh, startPos, endPos);
            }
        }

        void AddLine(VertexHelper vh, Vector2 startPos, Vector2 endPos)
        {
            UIVertex[] quad = new UIVertex[4];
            Vector2 lineDirection = (endPos - startPos).normalized;
            Vector2 lineNormal = new Vector2(-lineDirection.y, lineDirection.x);

            quad[0].position = startPos + lineNormal * (lineWidth / 2);
            quad[1].position = startPos - lineNormal * (lineWidth / 2);
            quad[2].position = endPos + lineNormal * (lineWidth / 2);
            quad[3].position = endPos - lineNormal * (lineWidth / 2);

            for (int i = 0; i < 4; i++)
                quad[i].color = color;

            vh.AddUIVertexQuad(quad);
        }
    }
    /*
    public class UILineRenderer : Graphic
    {
        public Vector2Int girdSize;

        public List<Vector2> pointList = new List<Vector2>();

        private float width;
        private float height;
        private float unitWidth;
        private float unitHeight;

        public float thickness = 10f; 
        protected override void OnPopulateMesh(VertexHelper _vh)
        {
            base.OnPopulateMesh(_vh); 
            _vh.Clear();        

            width = rectTransform.rect.width;
            height = rectTransform.rect.height;

            unitWidth = width / (float)girdSize.x;
            unitHeight = height / (float)girdSize.y;

            if(pointList.Count < 2)
            {
                return;     
            }

            for(int i =0; i < pointList.Count; i++)
            {
                Vector2 _point = pointList[i];

                DrawVerticesForPoint(_point, _vh); 
            }

            for(int i = 0; i< pointList.Count -1; i++)
            {
                int _index = i * 2;
                _vh.AddTriangle(_index + 0, _index + 1, _index + 3);
                _vh.AddTriangle(_index + 3, _index + 2, _index + 0); 
            }
        }

        private void DrawVerticesForPoint(Vector2 _point, VertexHelper _vh)
        {
            UIVertex _vertex = UIVertex.simpleVert;
            _vertex.color = color;

            _vertex.position = new Vector3(-thickness / 2, 0);
            _vertex.position += new Vector3(unitWidth * _point.x, unitHeight * _point.y);
            _vh.AddVert(_vertex);


            _vertex.position = new Vector3(thickness / 2, 0);
            _vertex.position += new Vector3(unitWidth * _point.x, unitHeight * _point.y);
            _vh.AddVert(_vertex);
        }
    }
    */
}

