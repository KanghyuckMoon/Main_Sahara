using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

public class LookAtPlayer : MonoBehaviour
{
	private AIModule AIModule
	{
		get
		{
			aiModule ??= mainModule.GetModuleComponent<AIModule>(ModuleType.Input);
			return aiModule;
		}
	}

	[SerializeField]
	private AbMainModule mainModule;

	[SerializeField]
	private Transform head;
	[SerializeField]
	private Transform body;

	[SerializeField]
	private float correctionY;
	[SerializeField]
	private float correctionX;
	[SerializeField]
	private float correctionZ;


	[SerializeField]
	private float limitY = 20f;
	[SerializeField]
	private float limitX = 0f;
	[SerializeField]
	private float limitZ = 0f;

	[SerializeField]
	private float rotationSpeed = 1f;

	private Quaternion targetRotation;

	private AIModule aiModule;

	public void Update()
	{
		if(AIModule.AIModuleHostileState is AIModule.AIHostileState.Discovery)
		{
			LookHeadToPlayer();
		}
	}

	private void LookHeadToPlayer()
	{// Calculate the direction to the player
        Vector3 targetDirection = PlayerObj.Player.transform.position - head.position;

        // Calculate the rotation to look at the player
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
		var localEuler = (Quaternion.Inverse(head.transform.parent.rotation) * targetRotation);

		var euler = localEuler.eulerAngles;

		if(euler.y < 90)
		{
			euler.y = 270 + limitY;
		}
		else
		{
			euler.y = Mathf.Clamp(euler.y, 270 - limitY, 270 + limitY);
		}

		//euler.x = euler.x > 180 ? euler.x - 360 : euler.x;
		euler.x = Mathf.Clamp(euler.x, -limitX, limitX);
		
		//euler.z = euler.z > 180 ? euler.z - 360 : euler.z;
		euler.z = Mathf.Clamp(euler.z, -limitZ, limitZ);

		euler.x += correctionX;
		euler.y += correctionY;
		euler.z += correctionZ;

		head.localEulerAngles = euler;
		//head.localEulerAngles = Vector3.Lerp(head.localEulerAngles, euler, Time.deltaTime * rotationSpeed);
	}
	//
}
