using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using DG.Tweening; 
namespace Arena
{
    public class UpDownPlatform : PlatformBase
    {
        private void OnDisable()
        {
            DOTween.KillAll(); 
        }

        [ContextMenu("Å×½ºÆ°")]
        public override void StartAction()
        {
            DoPlatform();            
        }

        private void DoPlatform()
        {
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(startDelay);
            seq.Append(transform.DOMoveY(3, tweenDuration));
            seq.AppendInterval(actionDelay);
            seq.Append(transform.DOMoveY(-3, tweenDuration)); 
            seq.AppendInterval(actionDelay);
            seq.AppendCallback(DoPlatform);
            
        }
    }
    
}
