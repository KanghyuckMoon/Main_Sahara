using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pool;
using UnityEngine;

namespace Weapon
{
    public class WoodenBowSkillObject : MonoBehaviour
    {
        [SerializeField] private string address;
        [SerializeField] private Vector3 pos;
        [SerializeField] private float radius = 9;
        private GameObject targetObject;

        private Transform target;

        private string tagName;

        private bool isUse = false;

        public Vector3 Pos => pos;

        private void Update()
        {
            if (isUse)
            {
                transform.position = Vector3.LerpUnclamped(transform.position, target.position + new Vector3(0, 0.6f, 0), 10 * Time.deltaTime);
                
                Vector3 directionVec = (target.position + new Vector3(0, 0.6f, 0)) - transform.position;
                Quaternion qua = Quaternion.LookRotation(directionVec);
                transform.rotation = Quaternion.Slerp(transform.rotation, qua, Time.deltaTime * 2f);
            }
            
            if (isUse) return;

            Collider[] _cols = Physics.OverlapSphere(transform.position, radius);

            foreach (var VARIABLE in _cols)
            {
                if (VARIABLE.CompareTag(tagName))
                {
                    transform.SetParent(null);
                    isUse = true;
                    var _effect = ObjectPoolManager.Instance.GetObject("WoodenBow_SkillEffectShot");
                    _effect.transform.position = transform.position;
                    _effect.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    _effect.SetActive(true);

                    target = VARIABLE.transform;

                    //_effect.transform.LookAt(VARIABLE.bounds.center);

                    //transform.DOMove(VARIABLE.bounds.center, .8f).SetEase(Ease.OutQuart);
                    //transform.LookAt(VARIABLE.bounds.center);
                }
            }
        }

        public void SetInfo(GameObject _targetObj, string _tagName, Vector3 _pos)
        {
            //transform.localPosition = Pos;
            targetObject = _targetObj;
            Transform _transform;
            (_transform = transform).SetParent(_targetObj.transform);
            
            tagName = _tagName;
            isUse = false;

            _transform.localPosition = _pos;
            _transform.localRotation = Quaternion.identity;
            pos = _pos;

            _transform.localScale = Vector3.zero;
            gameObject.SetActive(true);

            transform.DOScale(0.3f, 1f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tagName))
            {
                ObjectPoolManager.Instance.RegisterObject(address, gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}