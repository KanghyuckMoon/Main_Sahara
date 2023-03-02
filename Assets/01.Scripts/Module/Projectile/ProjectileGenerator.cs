using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Module
{
    public class ProjectileGenerator : MonoBehaviour
    {
        private AttackModule attackModule;

        private void Start()
        {
            attackModule = GetComponent<AbMainModule>().GetModuleComponent<AttackModule>(ModuleType.Attack);
        }

        public void SpownGeomtry()
        {
            attackModule.CreateProjectile(WeaponHand.Weapon, "ProjectileName");
            attackModule.ProjectileObject?.GetComponent<IProjectile>().MovingFunc(transform.forward);
        }
    }
}