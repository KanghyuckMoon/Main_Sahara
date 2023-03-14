using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager;

namespace PhysicsTime
{
    public class PhyscisTestBoxUp : MonoBehaviour
    {
        private Rigidbody rigid;
        private float timer = 0f;

        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

		private void Update()
		{
			if(timer > 0f)
			{
                timer -= StaticTime.PhysicsDeltaTime;
			}
            else
			{
                rigid.AddForce(Vector3.up * 3f, ForceMode.Impulse);
                timer += 3f;
			}

		}

	}
}
