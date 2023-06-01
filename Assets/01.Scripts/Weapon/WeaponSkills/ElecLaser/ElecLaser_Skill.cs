using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{    public class ElecLaser_Skill : ProjectileObject, IProjectile
    {
        //[SerializeField]
        //private Vector3 _pos = new Vector3(0f, 0.5f, 0f);
        //
        //private Transform player;
        //public Rigidbody rigidbody;

        public void MovingFunc(Vector3 _vector3)
        {
            //transform.SetParent(null);
            //player ??= PlayerObj.Player.transform;
            //Vector3 _dir = (player.position + _pos) - transform.position;
            //rigidbody.AddForce(_dir.normalized * objectData.speed, ForceMode.Impulse);
        }
    }

} 
