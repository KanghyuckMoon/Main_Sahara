using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public interface IProjectile
    {
        public void MovingFunc(Vector3 _vector3);
    }
}