using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Item
{
    public class FlyEffect_Treasure : TreasureMapEffect
    {
        [SerializeField] private int count = 5;

        [SerializeField] private bool isPositionExit;
        [SerializeField] private Vector3 endPosition;

        private Rigidbody rigid;

        private float currentDelay = 0;
        private float delay = 0.8f;

        private void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (count <= 0) return;
            if (currentDelay <= 0)
            {
                var _col = Physics.OverlapSphere(transform.position, 4);

                foreach (var _variable in _col)
                {
                    if (!_variable.CompareTag("Player")) continue;

                    currentDelay = delay;

                    //Debug.LogError("lasiufhlawieufhlweufh");
                    
                    var randomPos = isPositionExit ? 
                        (endPosition - transform.position).normalized * 4f :
                        new Vector3(Random.Range(-6f, -3f), Random.Range(2f, 2.3f), Random.Range(-1f, -0.1f));
                    count--;

                    rigid.AddForce(randomPos, ForceMode.Impulse);
                }
            }
            else
            {
                currentDelay -= Time.fixedDeltaTime;
            }
        }
    }
}