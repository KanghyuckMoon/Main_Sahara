using System.Collections;
using System.Collections.Generic;
using Effect;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;
using Utill.Pattern;
using Sound;
using TMPro;

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


        [SerializeField]
        private TextMeshProUGUI distanceValueText;
		[SerializeField]
		private Image underMaskImage;

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
				SetTextIn();
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
                SetTextIn();

			}
            else
            {
                LinearColor linearColor = default;
                linearColor.green = 1f;
                pointLight.color = Color.green;
                maxTimer = 100f;
				SetTextOut();
			}

            if (currentTimer > maxTimer)
            {
                currentTimer = 0f;
                //Sound Play


                EffectManager.Instance.SetEffectDefault(closeDetectEffect, targetEffectTrm.position, CalculateRotation(transform.position, targetTrm.position), Vector3.one, null);
            }
            else
            {
                currentTimer += Time.deltaTime;
            }
        }

		private Vector3 CalculateRotation(Vector3 fromPosition, Vector3 toPosition)
		{
			Vector3 direction = toPosition - fromPosition;
			Quaternion rotation = Quaternion.LookRotation(direction);
            var rotate = rotation.eulerAngles;
            rotate.x = 0;
            rotate.z = 0;
			return rotate;
		}

        private void SetTextOut()
		{
			distanceValueText.text = $"0.";
            underMaskImage.fillAmount = Mathf.Lerp(underMaskImage.fillAmount, 0f, Time.deltaTime);
		}

        private void SetTextIn()
		{
            float underValue = 0f;
			if ((targetItem.DetectItemType & DetectItemType.Metal) != 0)
			{
				underValue = Random.Range(0.4f, 0.5f);
				underMaskImage.fillAmount = Mathf.Lerp(underMaskImage.fillAmount, underValue, Time.deltaTime); 
			}
			else if ((targetItem.DetectItemType & DetectItemType.Structure) != 0 || (targetItem.DetectItemType & DetectItemType.Creture) != 0)
			{
				underValue = Random.Range(0.9f, 1.0f);
				underMaskImage.fillAmount = Mathf.Lerp(underMaskImage.fillAmount, underValue, Time.deltaTime);
			}
			distanceValueText.text = $"{minDistance.ToString("F1")}.";
		}
	}
}
