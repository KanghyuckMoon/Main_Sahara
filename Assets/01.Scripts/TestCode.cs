using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Test1");
        Utill.Measurement.Logging.Log("Test2");
    }
}
