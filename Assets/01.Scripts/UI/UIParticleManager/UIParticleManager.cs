using System;
using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using UnityEngine;
using Coffee.UIParticleExtensions;
using UnityEditor;
using Utill.Pattern;
using Utill.Addressable;

namespace UI.ParticleManger
{
    public enum ParticleType
    {
        Burst, 
        
    }
    
    public class UIParticleManager : MonoSingleton<UIParticleManager>
    {
        private Dictionary<ParticleType, UIParticle> uiParticleDic = new Dictionary<ParticleType, UIParticle>();

        public override void Awake()
        {
            base.Awake();
            uiParticleDic.Add(ParticleType.Burst, 
                AddressablesManager.Instance.GetResource<GameObject>("UIBurstParticle")
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
            _particle.Play();
            _particle.GetComponent<RectTransform>().anchoredPosition = _pos; 
            _particle.transform.SetParent(_parent);
            if (_isLoop == false)
            {
                StartCoroutine(Pause(_particle));
            }
            return _particle; 
        }

        private IEnumerator Pause(UIParticle _particle)
        {
            yield return new WaitForSeconds(10f); 
            _particle.Clear();
            // 삭제 
        }
    }
}