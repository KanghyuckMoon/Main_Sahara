using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attack
{
    public class AttackColliderSetting : MonoBehaviour
    {
        private Collider collider;
        private SwordSetting swordSetting;

        private void Start()
        {
            swordSetting = GetComponentInParent<SwordSetting>();
            collider = GetComponent<Collider>();
            collider.enabled = false;
        }

        void Update()
        {
            collider.enabled = swordSetting.isColliderOn;
        }
    }
}