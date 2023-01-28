using UnityEngine.UIElements;
using System;
using UnityEngine;

public class Gradient : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Gradient, GradientUxmlTraits> { }

    public class GradientUxmlTraits : UxmlTraits
    {
        UxmlColorAttributeDescription leftColor = new UxmlColorAttributeDescription { name = "left-color", defaultValue = Color.red };
        UxmlColorAttributeDescription rightColor = new UxmlColorAttributeDescription { name = "right-color", defaultValue = Color.black };


        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);

            if (ve == null)
                throw new ArgumentNullException(nameof(ve));

            var grad = (Gradient)ve;
            grad.leftColor = leftColor.GetValueFromBag(bag, cc);
            grad.rightColor = rightColor.GetValueFromBag(bag, cc);
        }
    }

    public Gradient()
    {
        generateVisualContent += GenerateVisualContent;
    }

    public Color leftColor;
    public Color rightColor;

    static readonly Vertex[] vertices = new Vertex[4];
    static readonly ushort[] indices = { 0, 1, 2, 2, 3, 0 };

    void GenerateVisualContent(MeshGenerationContext mgc)
    {
        var rect = contentRect;
        if (rect.width < 0.1f || rect.height < 0.1f)
            return;
        vertices[0].tint = leftColor;
        vertices[1].tint = leftColor;
        vertices[2].tint = rightColor;
        vertices[3].tint = rightColor;

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