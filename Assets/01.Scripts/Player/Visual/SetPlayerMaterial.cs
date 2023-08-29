using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SetPlayerMaterial : MonoBehaviour
    {
        private Dictionary<SkinnedMeshRenderer, Material> skinnedMeshRenderers =
            new Dictionary<SkinnedMeshRenderer, Material>();

        [SerializeField] private Material testMat;

        private void Start()
        {
            SkinnedMeshRenderer[] lists = GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (var VARIABLE in lists)
            {
                Debug.Log(VARIABLE);
                skinnedMeshRenderers.Add(VARIABLE, VARIABLE.material);
            }
        }

        public void SetMaterials(Material _mat)
        {
            foreach (var VARIABLE in skinnedMeshRenderers.Keys)
            {
                VARIABLE.material = _mat;
            }
        }

        [ContextMenu("머테리얼 적용 해제")]
        public void ResetMaterials()
        {
            foreach (var VARIABLE in skinnedMeshRenderers)
            {
                VARIABLE.Key.material = VARIABLE.Value;
            }
        }

        [ContextMenu("테스트 머테리얼 적용")]
        private void SetTestMat()
        {
            SetMaterials(testMat);
        }
    }
}