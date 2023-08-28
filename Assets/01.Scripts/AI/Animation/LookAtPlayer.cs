using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

public class LookAtPlayer : MonoBehaviour
{
	private enum InitVector
	{
		Forward, 
		Right, 
		Left, 
		Back,
		Up,
		Down,
	}

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
	private float correctionY;
	[SerializeField]
	private float correctionX;
	[SerializeField]
	private float correctionZ;


	[SerializeField, RangeAttribute(0f, 180f)]
	private float limitY = 20f;
	[SerializeField, RangeAttribute(0f, 180f)]
	private float limitX = 0f;
	[SerializeField, RangeAttribute(0f, 180f)]
	private float limitZ = 0f;

	[SerializeField]
	private float rotationSpeed = 1f;

	private Quaternion targetRotation;

	private AIModule aiModule;

	private Vector3 originForward;

	[SerializeField]
	private InitVector initVector;

	public float maxRotationAngle;
	public Vector3 desiredRotationEulerAngles = new Vector3(0f, 0f, 180f);

	private void Start()
	{
	}

	public void Update()
	{
		LookHeadToPlayer();
		//if (AIModule.AIModuleHostileState is AIModule.AIHostileState.Discovery)
		//{
		//	//LookHeadToPlayer();
		//}
	}

	private void LookHeadToPlayer()
	{        
		// Calculate the direction from the head to the player
		Vector3 targetDirection = (PlayerObj.Player.transform.position - transform.position).normalized;

		// Calculate the rotation that points the forward vector in the target direction
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

		Quaternion direction = Quaternion.LookRotation(GetDirection(initVector));

		// Limit the rotation to the specified angle
		Quaternion desiredRotation = Quaternion.Euler(desiredRotationEulerAngles);
		float angleDifference = Quaternion.Angle(direction, targetRotation);
		

		if (angleDifference > maxRotationAngle)
		{
			// Calculate the clamped rotation
			Quaternion clampedRotation = Quaternion.RotateTowards(direction, targetRotation, maxRotationAngle);

			// Apply the clamped rotation to your object
			head.rotation = Quaternion.Lerp(head.rotation, clampedRotation * desiredRotation, Time.deltaTime * rotationSpeed) ;
		}
		else
		{
			// If the angle difference is within the allowed range, directly set the rotation
			head.rotation = Quaternion.Lerp(head.rotation, targetRotation * desiredRotation, Time.deltaTime * rotationSpeed);
		}
	}

	private Vector3 GetDirection(InitVector initVector)
	{
		switch (initVector)
		{
			case InitVector.Forward:
				return transform.forward;
			case InitVector.Right:
				return transform.right;
			case InitVector.Left:
				return -transform.right;
			case InitVector.Back:
				return -transform.forward;
			case InitVector.Up:
				return transform.up;
			case InitVector.Down:
				return -transform.up;
		}
		return Vector3.zero;
	}
}
