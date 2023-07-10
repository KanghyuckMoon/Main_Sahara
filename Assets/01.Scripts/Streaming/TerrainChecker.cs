using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;

namespace Streaming
{
    public class TerrainChecker : MonoBehaviour
    {
        private void Awake()
        {
            AddTerrain();
            GetComponent<Terrain>().materialTemplate = AddressablesManager.Instance.GetResource<Material>("SandMaterial"); //Resources.Load<Material>("Material/HDRPLitGround");
        }

        private void OnDestroy()
        {
            RemoveTerrain();
        }
        
        [ContextMenu("AddTerrain")]
        private void AddTerrain()
        {
            TerrainManager.Instance.EnableTerrain(GetComponent<Terrain>(), gameObject.name);
        }
        
        [ContextMenu("RemoveTerrain")]
        private void RemoveTerrain()
        {
            TerrainManager.Instance.DisableTerrain(gameObject.name);
        }
    }

}