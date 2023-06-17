using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Testt : GraphView
{
    // Start is called before the first frame update
    public new class UxmlFactory : UxmlFactory<Testt, GraphView.UxmlTraits> { }
    
    public Testt()
    {
    }
        void GenerateVisualContent(MeshGenerationContext mgc)
    {

    }

}
