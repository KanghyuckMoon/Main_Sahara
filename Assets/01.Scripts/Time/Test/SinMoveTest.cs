using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager
{
    public class SinMoveTest : MonoBehaviour
    {
        void FixedUpdate()
        {
            transform.Translate(new Vector3(Mathf.Sin(Time.time),0,0) * StaticTime.PhysicsFixedDeltaTime);
        }
    }

}
