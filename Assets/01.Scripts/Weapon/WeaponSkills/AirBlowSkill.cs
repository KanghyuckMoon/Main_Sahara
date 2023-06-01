using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Weapon
{
    public class AirBlowSkill : ProjectileObject, IProjectile
    {
        
        public void MovingFunc(Vector3 _vector3)
        {
            Invoke("Delete", 0.5f);
        }

        private void Delete()
        {
            gameObject.SetActive(false);
        }
    }
}