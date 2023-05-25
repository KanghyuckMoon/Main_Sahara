using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Map
{
    public class TutorialTextmark : MonoBehaviour
    {
        [SerializeField] private TextMeshPro textMeshPro;
        [SerializeField] private bool isMark;
        [SerializeField] private bool isInitAlphaZero = true;
        private void Start()
        {
            if (isInitAlphaZero)
            {
                textMeshPro.alpha = 0f;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isMark)
            {
                return;
            }
            isMark = true;
            textMeshPro.DOFade(1f, 2f);

        }

        private void OnTriggerExit(Collider other)
        {
            textMeshPro.DOFade(0f, 2f);
        }
    }   
}
