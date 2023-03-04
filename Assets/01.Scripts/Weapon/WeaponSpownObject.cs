using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public enum WeaponHand
    {
        RightHand,
        LeftHand,
        Weapon
    }
    public class WeaponSpownObject : MonoBehaviour
    {
        public WeaponHand weaponHand;
    }
}