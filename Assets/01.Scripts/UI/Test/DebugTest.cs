using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    public void DebugA()
    {
        Debug.Log("AAA");
    }
    
    public void DebugB()
    {
        Debug.Log("BBB");
    }
    
    public void DebugC()
    {
        Debug.Log("CCC");
    }
    
    public void DebugD()
    {
        Debug.Log("DDD");
    }


    public Transform target; 
    public Vector3 scale;
    public Vector3 pos;
    public Vector3 rot; 
    [ContextMenu("Local")]
    public void SetLocal()
    {
        target.transform.localScale = scale; 
        target.transform.localPosition = scale; 
        target.transform.localEulerAngles = scale; 
        
    }
    
    [ContextMenu("World")]
    public void SetWorld()
    {
        Debug.Log("World" + target.transform.lossyScale);
        Debug.Log("Local" + target.transform.localScale);
        target.transform.position = scale; 
        target.transform.eulerAngles = scale; 
    }
}