using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using TimeManager;

namespace PhysicsTime
{
	public class PhysicsTimeManager : MonoSingleton<PhysicsTimeManager>
    {
        private float timer = 0f;

        void Update()
        {
            if (Physics.autoSimulation)
                return; // do nothing if the automatic simulation is enabled
            if (StaticTime.PhysicsDeltaTime == 0f)
                return;

            timer += StaticTime.PhysicsDeltaTime;

            // Catch up with the game time.
            // Advance the physics simulation in portions of Time.fixedDeltaTime
            // Note that generally, we don't want to pass variable delta to Simulate as that leads to unstable results.
            while (timer >= StaticTime.PhysicsFixedDeltaTime)
            {
                if(StaticTime.PhysicsFixedDeltaTime == 0f)
                {
                    break;
                }
                timer -= StaticTime.PhysicsFixedDeltaTime;
                Physics.Simulate(StaticTime.PhysicsFixedDeltaTime);
            }

            // Here you can access the transforms state right after the simulation, if needed
        }
    }

}