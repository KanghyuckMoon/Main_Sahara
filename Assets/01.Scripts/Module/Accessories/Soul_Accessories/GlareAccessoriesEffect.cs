using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using TimeManager;
using DG.Tweening;
using Pool;
using UnityEngine.Rendering;
using Utill.Addressable;
using Utill.Coroutine;
using Weapon;

namespace PassiveItem
{
    public class GlareAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        private StateModule stateModule;
        private Material effectMat;

        private List<SkinnedMeshRenderer> skinnedlist = new List<SkinnedMeshRenderer>();
        private List<AbMainModule> list = new List<AbMainModule>();

        private ParticleSystem light;

        private int lifeCount;

        private bool canUse = true;
        
        public GlareAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            stateModule = mainModule.GetModuleComponent<StateModule>(ModuleType.State);

            effectMat = AddressablesManager.Instance.GetResource<Material>("SturnEffectMat");
            if (mainModule.name != "Player") return;
            light = mainModule.transform.Find("LightEffect").GetComponent<ParticleSystem>();
            light.gameObject.SetActive(false);
            canUse = true;
        }
        
        public void ApplyPassiveEffect()
        {
        }

        public void UpdateEffect()
        {
            if (!canUse && !mainModule.player) return;
            if (Input.GetKeyDown(KeyCode.LeftControl) && !stateModule.CheckState(State.SKILL))
            {
                canUse = false;
                mainModule.Animator.SetTrigger("LightEffect");
                light.gameObject.SetActive(true);
                mainModule.StartCoroutine(SetLightFalse());
                SetEnemySturn();
            }
        }

        IEnumerator SetLightFalse()
        {
            yield return new WaitForSeconds(1f);
            light.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(1.3f);
            
            foreach (var _skinnedMeshRenderer in skinnedlist)
            {
                Material[] mat = _skinnedMeshRenderer.materials;
                Array.Resize(ref mat, mat.Length - 1);
                _skinnedMeshRenderer.materials = mat;
            }
            
            foreach (var abMainModule in list)
            {
                abMainModule.PersonalTime = 1;
            }
            
            canUse = true;
            skinnedlist.Clear();
            list.Clear();
        }

        private void SetEnemySturn()
        {
            var _col = Physics.OverlapSphere(mainModule.transform.position + new Vector3(0, 1, 0), 3);
            foreach (var VARIABLE in _col)
            {
                if (!VARIABLE.CompareTag("Enemy")) continue;
                var _renderer = VARIABLE.GetComponentsInChildren<SkinnedMeshRenderer>();

                foreach (var _VARIABLE in _renderer)
                {
                    var mat = _VARIABLE.materials;
                    Array.Resize(ref mat, mat.Length + 1);
                    mat[Mathf.Max(0, mat.Length - 1)] = effectMat;
                    _VARIABLE.materials = mat;
                        
                    skinnedlist.Add(_VARIABLE);
                }

                var _enemy = VARIABLE.GetComponent<AbMainModule>();
                if (_enemy is null) continue;
                _enemy.PersonalTime = 0f;
                list.Add(_enemy);
            }

            mainModule.StartCoroutine(SetLightFalse());
        }

        public void ClearPassiveEffect()
        {
        }

        public void UpgradeEffect()
        {
            
        }
    }
}