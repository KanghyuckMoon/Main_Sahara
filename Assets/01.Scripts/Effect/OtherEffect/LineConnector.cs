using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Effect
{
    public class LineConnector : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform[] transforms;

        private void Update()
        {
            for (int i = 0; i < transforms.Length; ++i)
            {
                lineRenderer.SetPosition(i, transforms[i].position);
            }
        }
    }   
}
