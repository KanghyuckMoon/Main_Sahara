using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(menuName = "SO/WeaponPositionSO")]
    public class WeaponPositionSO : ScriptableObject
    {
        public Vector3 weaponPosition;
        public Quaternion weaponRotation;
    }
}