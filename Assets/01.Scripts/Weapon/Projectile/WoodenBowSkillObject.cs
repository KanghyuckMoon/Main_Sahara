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
                transform.position = Vector3.LerpUnclamped(transform.position, target.position, Time.deltaTime);
                
                Vector3 directionVec = target.position - transform.position;
                Quaternion qua = Quaternion.LookRotation(directionVec);
                transform.rotation = Quaternion.Slerp(transform.rotation, qua, Time.deltaTime * 2f);
            }
            
            if (isUse) return;
            transform.localPosition = Vector3.Lerp(targetObject.transform.position + pos, transform.position,
                Time.deltaTime * 5);

            transform.rotation =
                Quaternion.Lerp(targetObject.transform.rotation, transform.rotation, Time.deltaTime * 5);

            Collider[] _cols = Physics.OverlapSphere(transform.position, radius);

            foreach (var VARIABLE in _cols)
            {
                if (VARIABLE.CompareTag(tagName))
                {
                    var _effect = ObjectPoolManager.Instance.GetObject("WoodenBow_SkillEffectShot");
                    _effect.transform.position = transform.position;
                    _effect.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    _effect.SetActive(true);

                    target = VARIABLE.transform;
                    isUse = true;
                    
                    //_effect.transform.LookAt(VARIABLE.bounds.center);

                    //transform.DOMove(VARIABLE.bounds.center, .8f).SetEase(Ease.OutQuart);
                    //transform.LookAt(VARIABLE.bounds.center);
                }
            }
        }

        public void SetInfo(GameObject _targetObj, string _tagName)
        {
            transform.localPosition = Pos;
            targetObject = _targetObj;
            Transform _transform;
            (_transform = transform).SetParent(null);
            tagName = _tagName;
            isUse = false;

            _transform.localScale = Vector3.zero;
            gameObject.SetActive(true);

            transform.DOScale(0.3f, 1f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tagName))
            {
                ObjectPoolManager.Instance.RegisterObject("WoodenBowSkill_Arrow", gameObject);
            }
        }
    }
}