using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLength : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("local scale" + transform.localScale);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            float PositionOfTheHead = (GetComponent<MeshFilter>().mesh.bounds.extents.z * transform.localScale.z) + transform.position.z;
            Debug.Log("2번째" + PositionOfTheHead); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("MeshFilter Bounds" + GetComponent<MeshFilter>().mesh.bounds);
            Debug.Log("MeshRenderer Bounds" + GetComponent<MeshRenderer>().bounds);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("4번째"  +GetTotalMeshFilterBounds(transform)); 
        }
    }
    private static Bounds GetTotalMeshFilterBounds(Transform objectTransform)
    {
        var meshFilter = objectTransform.GetComponent<MeshFilter>();
        var result = meshFilter != null ? meshFilter.mesh.bounds : new Bounds();

        foreach (Transform transform in objectTransform)
        {
            var bounds = GetTotalMeshFilterBounds(transform);
            result.Encapsulate(bounds.min);
            result.Encapsulate(bounds.max);
        }
        var scaledMin = result.min;
        scaledMin.Scale(objectTransform.localScale);
        result.min = scaledMin;
        var scaledMax = result.max;
        scaledMax.Scale(objectTransform.localScale);
        result.max = scaledMax;
        return result;
    }
}
