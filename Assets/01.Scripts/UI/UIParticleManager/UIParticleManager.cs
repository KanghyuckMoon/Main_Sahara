using System;
using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using UnityEngine;
using Coffee.UIParticleExtensions;
using UnityEditor;
using Utill.Pattern;
using Utill.Addressable;
using UnityEngine.UI.Extensions; 
namespace UI.ParticleManger
{
    
    public enum CanvasType
    {
        Cam, 
        Overlay, 
        None 
    }
    public enum ParticleType
    {
        Burst, 
        SandBurst
    }
    
    public class UIParticleManager : MonoSingleton<UIParticleManager>
    {
        private Dictionary<ParticleType, UIParticle> uiParticleDic = new Dictionary<ParticleType, UIParticle>();
        //private Dictionary<ParticleType, UIParticleSystem> uiParticleDic = new Dictionary<ParticleType, UIParticleSystem>();

        public override void Awake()
        {
            
            base.Awake();
            uiParticleDic.Add(ParticleType.Burst, 
                AddressablesManager.Instance.GetResource<GameObject>("UIBurstParticle")
                    .GetComponent<UIParticle>());
            uiParticleDic.Add(ParticleType.SandBurst, 
                AddressablesManager.Instance.GetResource<GameObject>("UISandBurstParticle")
                    .GetComponent<UIParticle>());

            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                Play(ParticleType.Burst, new Vector2(500, 500), transform);
            }
        }

        public UIParticle Play(ParticleType particleType, Vector2 _pos, Transform _parent, bool _isLoop = false)
        {
            UIParticle _particle = uiParticleDic[particleType];
            UIParticle _p = Instantiate(_particle, _parent);
            //_p.StartParticleEmission();
            _p.Play();
           _p.GetComponent<RectTransform>().anchoredPosition = _pos; 
            //_particle.transform.SetParent(_parent);
            if (_isLoop == false)
            {
                StartCoroutine(Pause(_p));
            }
            return _p; 
        }

        private IEnumerator Pause(UIParticle _particle)
        {
            yield return new WaitForSeconds(10f); 
            _particle.Clear();
            // 삭제 
        }
    
        private IEnumerator Pause(UIParticleSystem _particle)
        {
            yield return new WaitForSeconds(10f); 
            _particle.StopParticleEmission();
            // 삭제 
        }}
}