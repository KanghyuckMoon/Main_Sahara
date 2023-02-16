using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

public class BezierConnect : VisualElement
{
    private Vector2 start;
    private Vector2 end;
    private Color color;
    private float width;
    
    public BezierConnect(Vector2 _start, Vector2 _end, Color _color, float _width)
    {
        this.start = _start;
        this.end = _end;
        this.color = _color;
        this.width = _width;
        generateVisualContent += OnGenerateVisualContent;
    }

    private void OnGenerateVisualContent(MeshGenerationContext _mgc)
    {
      //  Painter2D
    }
}
