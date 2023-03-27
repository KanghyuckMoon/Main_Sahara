using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill;
using Pool;
using Utill.Addressable;
using Utill.Pattern;

namespace Effect
{
    public interface ISkinEffect {public void Setting(SkinnedMeshRenderer _skinnedMeshRenderer, Transform _modelRoot, Vector3 correctionAngle,  Vector3 correctionPos); }


    /// <summary>
    /// 이펙트 생성 매니저
    /// </summary>
    public class EffectManager : MonoSingleton<EffectManager>
    {
        private bool _isInit = false;

        public void Start()
        {
            if (!_isInit)
            {
                if (Instance == this)
                {
                    Init();
                }
                else
                {
                    Instance.Init();
                }
            }
        }

        /// <summary>
        /// 초기화
        /// </summary>
        private void Init()
        {
            if (_isInit)
            {
                return;
            }
            _isInit = true;
        }

        /// <summary>
        /// 이펙트 설치
        /// </summary>
        /// <param name="pos"></param>
        public void SetEffectDefault(string _adress, Vector3 _pos, Quaternion _quaternion)
        {
            if (!_isInit)
            {
                Init();
            }

            GameObject effect = ObjectPoolManager.Instance.GetObject(_adress);
            effect.transform.position = _pos;
			effect.transform.rotation = _quaternion;
			effect.transform.SetParent(null);
			effect.gameObject.SetActive(true);
        }

        /// <summary>
        /// 이펙트 설치
        /// </summary>
        /// <param name="pos"></param>
        public void SetEffectDefault(string _adress, Vector3 _pos, Vector3 _eulerAngles, Vector3 _size, Transform parent = null)
        {
            
            if (!_isInit)
            {
                Init();
            }

            GameObject effect = ObjectPoolManager.Instance.GetObject(_adress);
            effect.transform.position = _pos;
            effect.transform.eulerAngles = _eulerAngles;
            effect.transform.localScale = _size;
            if (parent)
            {
                effect.transform.SetParent(parent);
            }
            else
            {
                effect.transform.SetParent(null);
            }
            effect.gameObject.SetActive(true);
        }

        public void SetEffectSkin(string _adress, SkinnedMeshRenderer _skinnedMeshRenderer, Transform _obj, Transform _root, Vector3 _correctionAngle,Vector3 _correctionPos,  Scene _scene)
        {
            if (!_isInit)
            {
                Init();
            }

            GameObject effect = ObjectPoolManager.Instance.GetObject(_adress);
            effect.transform.SetParent(null);
            if (_scene != null)
			{
                SceneManager.MoveGameObjectToScene(effect, _scene);
			}
            effect.GetComponent<ISkinEffect>().Setting(_skinnedMeshRenderer, _root, _correctionAngle, _correctionPos);
            if(_obj != null)
            {
                effect.transform.position = _obj.position;
            }
            else
            {
                effect.transform.position = Vector3.zero;
            }
            effect.transform.SetParent(null);
            effect.gameObject.SetActive(true);
        }
        public void SetEffectSkin(string _adress, SkinnedMeshRenderer _skinnedMeshRenderer, Transform _obj, Transform _root, Vector3 _correctionAngle,Vector3 _correctionPos)
        {
            if (!_isInit)
            {
                Init();
            }

            GameObject effect = ObjectPoolManager.Instance.GetObject(_adress);
            effect.GetComponent<ISkinEffect>().Setting(_skinnedMeshRenderer, _root, _correctionAngle, _correctionPos);
            if(_obj != null)
            {
                effect.transform.position = _obj.position;
            }
            else
            {
                effect.transform.position = Vector3.zero;
            }
            effect.transform.SetParent(null);
            effect.gameObject.SetActive(true);
        }
    }

}