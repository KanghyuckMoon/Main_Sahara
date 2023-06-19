using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

namespace Arena
{
    public class FadeInOutPlatform : PlatformBase
    {
        [SerializeField]
        private Material mat;
        [SerializeField]
        private Collider col; 
        
        protected override void Awake()
        {
            mat = GetComponent<MeshRenderer>().material;
            col = GetComponent<Collider>(); 
        }

        [ContextMenu("테스트")]
        public override void StartAction()
        {
            base.StartAction();
            DoFadeInOut(); 
        }

        /// <summary>
        /// 발판 페이드인 아웃 
        /// </summary>
        private void DoFadeInOut()
        {
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(startDelay);
            seq.Append(mat.DOFade(0, tweenDuration));
            seq.AppendCallback(() => col.enabled = false);
            seq.AppendInterval(actionDelay);
            seq.Append(mat.DOFade(1, tweenDuration));
            seq.AppendCallback(() => col.enabled = true);
            seq.AppendInterval(actionDelay);
            seq.AppendCallback(DoFadeInOut);
        }
        
        
    }
}
