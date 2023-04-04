using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Utill.Pattern;
using Sound;

namespace Detect
{
    public class MetalDetectorItem : DetectorItem
    {
        [SerializeField] 
        private Light pointLight;
        [SerializeField] 
        private string closeDetectEff;

        [SerializeField] 
        private float closeRadius = 10f;
        
        private float currentTimer = 0f;
        private float maxTimer = 0f;

        public override void Detect()
        {
            base.Detect();

            if (closeRadius > minDistance)
            {
                LinearColor linearColor = default;
                linearColor.red = 1f;
                pointLight.color = Color.red;
                maxTimer = 0.5f;
            }
            else if (radius > minDistance)
            {
                LinearColor linearColor = default;
                linearColor.red = 1f;
                linearColor.green = 1f;
                pointLight.color = Color.yellow;
                maxTimer = 2f;
            }
            else
            {
                LinearColor linearColor = default;
                linearColor.green = 1f;
                pointLight.color = Color.green;
                maxTimer = 100f;
            }

            if (currentTimer > maxTimer)
            {
                currentTimer = 0f;
                //Sound Play
                SoundManager.Instance.PlayEFF(closeDetectEff);
            }
            else
            {
                currentTimer += Time.deltaTime;
            }
        }
    }
}
