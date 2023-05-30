using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class ElecLaserAttack : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _pos = new Vector3(0f, 0.5f, 0f);
        
        private Transform player;

        [SerializeField] private float speed;
        [SerializeField] private Rigidbody rigid;
        
        private void Update()
        {
            MovingFunc();
        }

        public void MovingFunc()
        {
            player ??= PlayerObj.Player.transform;
            Vector3 _dir = (player.position + _pos) - transform.position;
            rigid.AddForce(_dir.normalized * speed, ForceMode.Impulse);
        }
    }
}