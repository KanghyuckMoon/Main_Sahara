using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Utill.Addressable;
using DG.Tweening;
using Utill.Pattern;

namespace TimeOfDay
{
    public class TODLight : MonoBehaviour
    {
        [Range(0, 24)]
        public float timeOfDay;

        public float rotation_y = -150.0f;

        public float orbitSpeed = 1.0f;
        public Light sun;
        public Light moon;
        //public Volume skyVolume;
        public AnimationCurve starsCurve;

        public bool isNight;
        //private PhysicallyBasedSky sky;
        
        [SerializeField]
        private TODSO todSO;

        [SerializeField]
        private LensFlareComponentSRP sunLensFlare;
        [SerializeField]
        private LensFlareComponentSRP moonLensFlare;

        // Start is called before the first frame update
        void Start()
        {
            todSO ??= AddressablesManager.Instance.GetResource<TODSO>("T_TODSO");
            //skyVolume.profile.TryGet<PhysicallyBasedSky>(out sky);
            sunLensFlare = sun.GetComponent<LensFlareComponentSRP>();
            moonLensFlare = moon.GetComponent<LensFlareComponentSRP>();
        }

        // Update is called once per frame
        void Update()
        {
            if (todSO is null)
            {
                return;
            }

            if (!todSO.isUpdateOn)
            {
                return;
            }

            if(todSO.isUpdateTime)
            {
                timeOfDay += Time.deltaTime * orbitSpeed;
                timeOfDay %= 24;
                UpdateTime();
            }

            if(todSO.isOnlyNight)
			{
                isNight = true;
                todSO.isNight = true;
            }
        }
        
        void OnValidate()
        {
            if (todSO is null)
            {
                return;
            }
            
            if(todSO.isUpdateTime)
            {
                timeOfDay += Time.deltaTime * orbitSpeed;
                timeOfDay %= 24;
                UpdateTime();
            }

            if(todSO.isOnlyNight)
			{
                isNight = true;
                todSO.isNight = true;
			}
        }

        void UpdateTime()
		{
            float alpha = timeOfDay / 24.0f;
            float sunRotation = starsCurve.Evaluate(alpha);// Mathf.Lerp(-90, 270, alpha);
            float moonRotaion = sunRotation - 180;

            sun.transform.rotation = Quaternion.Euler(sunRotation, rotation_y, 0);
            moon.transform.rotation = Quaternion.Euler(moonRotaion, rotation_y, 0);

            //if (sky is not null)
			//{
            //    sky.spaceEmissionMultiplier.value = starsCurve.Evaluate(alpha);
			//}
            
            CheckNightDayTransition();
        }

        private void CheckNightDayTransition()
		{
            if(isNight)
			{
                if (moon.transform.rotation.eulerAngles.x > 180)
				{
                    StartDay();
				}
			}
            else
            {
                if (sun.transform.rotation.eulerAngles.x > 180)
                {
                    StartNight();
                }
            }
		}

		private void StartDay()
		{
            isNight = false;
            todSO.isNight = false;
            sun.shadows = LightShadows.Soft;
            moon.shadows = LightShadows.None;
            sun.gameObject.SetActive(true);
            moon.gameObject.SetActive(false);
            sunLensFlare.enabled = true;
            moonLensFlare.enabled = false;
        }

        private void StartNight()
        {
            isNight = true;
            todSO.isNight = true;
            sun.shadows = LightShadows.Soft;
            moon.shadows = LightShadows.None;
            moon.gameObject.SetActive(true);
            sun.gameObject.SetActive(false);
            sunLensFlare.enabled = false;
            moonLensFlare.enabled = true;
        }

        public void SetUpdate(bool isUpdate)
        {
            todSO.isUpdateOn = isUpdate;
        }
    }

}