using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class CurrentArrowInfo
    {

        public CurrentArrowInfo(string _arrowAddress, WeaponHand _weaponHand)
        {
            arrowAddress = _arrowAddress;
            weaponHand = _weaponHand;
        }

        public string arrowAddress;
        public WeaponHand weaponHand;
    }
}