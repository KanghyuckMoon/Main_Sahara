using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class FlyEffect_Treasure : TreasureMapEffect
    {
        [SerializeField] private int count = 4;

        private Rigidbody rigid;

        private float currentDelay;
        private float delay = 0.6f;
        
        private void FixedUpdate()
        {
            if (currentDelay <= 0)
            {
                var _col = Physics.OverlapSphere(transform.position, 4);

                foreach (var _variable in _col)
                {
                    if (_variable.name != "Player") return;

                    currentDelay = delay;

                    var randomPos = new Vector3(Random.Range(-2f, 2f), Random.Range(0.4f, 3f), Random.Range(-2f, 2f));
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