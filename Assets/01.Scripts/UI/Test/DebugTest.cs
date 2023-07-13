using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Measurement;

public class DebugTest : MonoBehaviour
{
    public void DebugA()
    {
        Logging.Log("AAA");
    }
    
    public void DebugB()
    {
        Logging.Log("BBB");
    }
    
    public void DebugC()
    {
        Logging.Log("CCC");
    }
    
    public void DebugD()
    {
        Logging.Log("DDD");
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
        Logging.Log("World" + target.transform.lossyScale);
        Logging.Log("Local" + target.transform.localScale);
        target.transform.position = scale; 
        target.transform.eulerAngles = scale; 
    }
}