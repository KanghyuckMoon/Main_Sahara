using System.Collections;
using System.Collections.Generic;
using Effect;
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
        private string closeDetectEffect;
        
        [SerializeField] 
        private Transform targetEffectTrm;
        
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
                if ((targetItem.DetectItemType & DetectItemType.Metal) != 0)
                {
                    linearColor.red = 1f;
                    linearColor.blue = 1f;
                    pointLight.color = Color.magenta;   
                }
                else if((targetItem.DetectItemType & DetectItemType.Structure) != 0 || (targetItem.DetectItemType & DetectItemType.Creture) != 0)
                {
                    linearColor.red = 1f;
                    pointLight.color = Color.red;
                }
                maxTimer = 0.3f;
            }
            else if (radius > minDistance)
            {
                LinearColor linearColor = default;
                linearColor.red = 1f;
                linearColor.green = 1f;
                pointLight.color = Color.yellow;
                
                float _normalizedValue = (minDistance - closeRadius) / (radius - closeRadius);
                float _timer = Mathf.Lerp(0.5f, 2f, _normalizedValue);
                
                maxTimer = _timer;
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
                EffectManager.Instance.SetEffectDefault(closeDetectEffect, targetEffectTrm.position, Vector3.zero, Vector3.one, targetEffectTrm);
            }
            else
            {
                currentTimer += Time.deltaTime;
            }
        }
    }
}
