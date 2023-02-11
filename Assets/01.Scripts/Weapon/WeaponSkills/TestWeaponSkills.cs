using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Pool;

namespace Weapon
{
    public class TestWeaponSkills : MonoBehaviour, IWeaponSkills
    {
        private Animator animator;
        private string windObjName = "LongWeapon_Object";

        public void Skills()
        {
            LongWind();
        }

        public void LongWind()
        {
            animator = GetComponentInParent<Animator>();

            GameObject _windObj = ObjectPoolManager.Instance.GetObject(windObjName);

            _windObj.transform.position = animator.gameObject.transform.position + new Vector3(0, 1.1f, 0.8f);
            _windObj.transform.rotation = animator.gameObject.transform.rotation;
            _windObj.SetActive(true);

            _windObj.GetComponent<IndividualObject>().damage = GetComponent<BaseWeapon>().WeaponDataSO.skillDamage;

            animator.Play("Skills2");

            if (_windObj.active)
                StartCoroutine(DestroyObj(_windObj));
        }

        IEnumerator DestroyObj(GameObject windObj)
        {
            yield return new WaitForSeconds(5);
            ObjectPoolManager.Instance.RegisterObject(windObjName, windObj);
            windObj.SetActive(false);
        }
    }
}
