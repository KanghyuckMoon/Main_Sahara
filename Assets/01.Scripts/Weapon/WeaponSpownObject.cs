using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public enum WeaponHand
    {
        Right,
        Left
    }
    public class WeaponSpownObject : MonoBehaviour
    {
        public WeaponHand weaponHand;
    }
}