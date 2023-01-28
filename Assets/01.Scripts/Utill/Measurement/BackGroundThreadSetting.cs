using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundThreadSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Application.backgroundLoadingPriority = ThreadPriority.Low;
    }
}
