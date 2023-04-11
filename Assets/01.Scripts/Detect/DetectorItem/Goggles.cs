using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detect
{
    public class Goggles : MonoBehaviour
    {
        [SerializeField]
        private LayerMask originMask;
        [SerializeField]
        private LayerMask goggleMask;
        private Camera camera;
        
        private void OnEnable()
        {
            camera ??= Camera.main;
            camera.cullingMask = goggleMask.value;
        }

        private void OnDisable()
        {
            camera ??= Camera.main;
            camera.cullingMask = originMask.value;
        }
    }
}
