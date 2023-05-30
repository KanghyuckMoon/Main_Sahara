using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public class ArenaTouchTrigger : AbArenaTrigger
    {
        [SerializeField] private string targetTag = "Player";

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag(targetTag))
            {
                Interact(); 
            }
        }
    }
    
}
