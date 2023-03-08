using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public class BodyRotation : MonoBehaviour
    {
        private Animator animator;
        private int verticalBodyLayerIndex;

        private void Start()
        {
            animator = GetComponent<Animator>();
            verticalBodyLayerIndex = animator.GetLayerIndex("VerticalBody");
        }

        public void SetVerticalBodyLayer(float _weight)
        {
            animator.SetLayerWeight(verticalBodyLayerIndex, _weight);
        }
    }
}