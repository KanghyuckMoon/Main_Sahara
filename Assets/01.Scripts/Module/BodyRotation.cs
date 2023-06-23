using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using DG.Tweening;
using Pool;
//using Codice.Client.Common.Locks.LockOwnerNameResolver;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace Module
{
    public class BodyRotation : MonoBehaviour
    {
        //private Volume volume;
        [SerializeField]
        private GameObject chromaticEffect;

        [SerializeField] private GameObject[] cameras;
        [SerializeField] private Vector3 position;
        [SerializeField] private Quaternion rotation;

        [SerializeField]
        private GameObject light;

        [SerializeField] private string speedLine;
        public GameObject dashEffect;


        public Volume volume;
        
        private Animator animator;
        private int verticalBodyLayerIndex;

        private void Start()
        {
            animator = GetComponent<Animator>();
            verticalBodyLayerIndex = animator.GetLayerIndex("VerticalBody");
        }

        public void SetVerticalBodyLayer(int _value)
        {
            DOTween.To(() => animator.GetLayerWeight(verticalBodyLayerIndex),
                (_x) =>
                    animator.SetLayerWeight(verticalBodyLayerIndex, _x)
                , (float)_value, 0.12f);
        }

        public void SetCanLand(int _on)
        {
            bool _isOn = _on > 0 ? true : false;

            animator.SetBool("CanLand", _isOn);
        }

        public void SetChromaticAberration(float _duration)
        {
            StopCoroutine("ActiveAhromaticAberration");
            StartCoroutine(ActiveAhromaticAberration(_duration));
        }

        IEnumerator ActiveAhromaticAberration(float _duration)
        {
            chromaticEffect.SetActive(true);
            GameObject _a = ObjectPoolManager.Instance.GetObject(speedLine);

            if (cameras[0].activeInHierarchy){
                _a.transform.SetParent(cameras[0].transform);
            }

            if (cameras[1].activeInHierarchy)
            {
                _a.transform.SetParent(cameras[1].transform);
            }
            
            if (cameras[2].activeInHierarchy)
            {
                _a.transform.SetParent(cameras[2].transform);
            }
            
            if (cameras[3].activeInHierarchy)
            {
                _a.transform.SetParent(cameras[3].transform);
            }

            _a.transform.localPosition = position;
            _a.transform.localRotation = rotation;
            _a.SetActive(true);
            _a.GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(_duration);
            _a.SetActive(false);
            ObjectPoolManager.Instance.RegisterObject(speedLine, _a);
            chromaticEffect.SetActive(false);
        }

        public void SetLightOff()
        {
            light.SetActive(false);
        }
        
    }
}