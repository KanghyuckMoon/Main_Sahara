using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoundRebuild : MonoBehaviour
{
    public Bounds bounds = new Bounds();
    public bool isInit = false;

    public void Start()
    {
        if (isInit)
        {
            Rebuild();
        }
    }

    [ContextMenu("Rebuild")]
    public void Rebuild()
    {
        Physics.RebuildBroadphaseRegions(bounds, 5);
    }
}
