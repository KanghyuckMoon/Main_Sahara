using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


namespace UI.Base
{

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
}

