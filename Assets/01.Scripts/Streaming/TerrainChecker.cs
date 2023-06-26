using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Streaming
{
    public class TerrainChecker : MonoBehaviour
    {
        private void Awake()
        {
            TerrainManager.Instance.EnableTerrain(GetComponent<Terrain>(), gameObject.name);
        }

        private void OnDestroy()
        {
            TerrainManager.Instance.DisableTerrain(gameObject.name);
        }
    }

}