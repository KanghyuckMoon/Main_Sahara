using UnityEngine.UIElements;
using System;
using UnityEngine;

public class Gradient : VisualElement
{
    public enum Direction
    {
        horizontal, 
        vertical 
    }
    public new class UxmlFactory : UxmlFactory<Gradient, GradientUxmlTraits> { }

    public class GradientUxmlTraits : UxmlTraits
    {
        UxmlColorAttributeDescription leftColor = new UxmlColorAttributeDescription { name = "left-color", defaultValue = Color.red };
        UxmlColorAttributeDescription rightColor = new UxmlColorAttributeDescription { name = "right-color", defaultValue = Color.black };
        UxmlEnumAttributeDescription<Direction> direction = new UxmlEnumAttributeDescription<Direction> { name = "direction", defaultValue = Direction.horizontal };
        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);

            if (ve == null)
                throw new ArgumentNullException(nameof(ve));

            var grad = (Gradient)ve;
            grad.firstColor = leftColor.GetValueFromBag(bag, cc);
            grad.secondColor = rightColor.GetValueFromBag(bag, cc);
            grad.direction = direction.GetValueFromBag(bag, cc); 
        }
    }

    public Gradient()
    {
        generateVisualContent += GenerateVisualContent;
    }

    public Color firstColor;
    public Color secondColor;
    public Direction direction; 

    static readonly Vertex[] vertices = new Vertex[4];
    static readonly ushort[] indices = { 0, 1, 2, 2, 3, 0 };

    void GenerateVisualContent(MeshGenerationContext mgc)
    {
        var rect = contentRect;
        if (rect.width < 0.1f || rect.height < 0.1f)
            return;

        if(direction == Direction.horizontal)
        {
            vertices[0].tint = firstColor;
            vertices[1].tint = firstColor;
            vertices[2].tint = secondColor;
            vertices[3].tint = secondColor;
        }else if(direction == Direction.vertical)
        {
            vertices[0].tint = secondColor;
            vertices[1].tint = firstColor;
            vertices[2].tint = firstColor;
            vertices[3].tint = secondColor;
        }

        var left = 0f;
        var right = rect.width;
        var top = 0f;
        var bottom = rect.height;

        vertices[0].position = new Vector3(left, bottom, Vertex.nearZ);
        vertices[1].position = new Vector3(left, top, Vertex.nearZ);
        vertices[2].position = new Vector3(right, top, Vertex.nearZ);
        vertices[3].position = new Vector3(right, bottom, Vertex.nearZ);

        MeshWriteData mwd = mgc.Allocate(vertices.Length, indices.Length);
        mwd.SetAllVertices(vertices);
        mwd.SetAllIndices(indices);
    }
}