using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
	[SerializeField]
	private Transform head;

	public void Update()
	{
		LookHeadToPlayer();
	}

	private void LookHeadToPlayer()
	{
		var target = PlayerObj.Player.transform;

		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion rotation = Quaternion.LookRotation(direction);

		// Convert the rotation to Euler angles
		Vector3 eulerRotation = rotation.eulerAngles;

		// Apply the Euler angles to the transform
		head.rotation = Quaternion.Euler(0f, eulerRotation.y, 0f);
	}
}
