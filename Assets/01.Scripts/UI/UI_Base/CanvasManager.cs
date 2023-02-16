using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var doc = GetComponent<UIDocument>();
        doc.rootVisualElement.Add(new LineDrawer(new Vector2(20, 50), new Vector2(50, 50), 10));
    }
}
