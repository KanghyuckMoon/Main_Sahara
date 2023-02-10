using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attack
{
    public class SwordSetting : MonoBehaviour
    {
        public bool isColliderOn;
        public void SetAttackCollider(int isOn)
        {
            isColliderOn = isOn == 0 ? false : true;
        }
    }
}