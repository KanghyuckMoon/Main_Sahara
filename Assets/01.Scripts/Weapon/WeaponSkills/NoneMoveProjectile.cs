using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Weapon
{
    public class NoneMoveProjectile : ProjectileObject, IProjectile
    {

        public void MovingFunc(Vector3 _vector3)
        {
            transform.SetParent(null);
        }
    }
}