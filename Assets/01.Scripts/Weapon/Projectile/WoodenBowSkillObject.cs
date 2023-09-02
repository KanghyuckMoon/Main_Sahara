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

        private string tagName;

        private bool isUse = false;

        public Vector3 Pos => pos;

        private void Update()
        {
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
                    isUse = true;

                    var _effect = ObjectPoolManager.Instance.GetObject("WoodenBow_SkillEffectShot");
                    _effect.transform.position = transform.position;
                    _effect.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    _effect.SetActive(true);

                    _effect.transform.LookAt(VARIABLE.transform);

                    transform.DOMove(VARIABLE.transform.position, 1f).SetEase(Ease.OutQuart);
                    transform.LookAt(VARIABLE.transform);
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
    }
}