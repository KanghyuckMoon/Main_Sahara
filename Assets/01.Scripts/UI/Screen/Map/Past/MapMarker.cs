using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements; 

public class MapMarker : MonoBehaviour
{
    private Camera mainCam; 

    public Image img;
    public Transform target;
    public Text meter;
    public Vector3 offset; 

    private void Awake()
    {
        mainCam = Camera.main; 
    }

    private void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        Debug.Log($"minX{minX} maxX{maxX}");

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;
        Debug.Log($"minY{minY} maxY{maxY}");

        Vector2 pos = mainCam.WorldToScreenPoint(target.position + offset);

        if(Vector3.Dot((target.position - mainCam.transform.position), mainCam.transform.forward) <0)
        {
            if(pos.x < Screen.width /2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX; 
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos; 
    }
}
