using UnityEngine;

namespace UpdateManager
{
	/// <summary>
	/// Moves the <see cref="GameObject"/> that it is attached to
	/// up and down with a random speed value
	/// </summary>
	public class ManagedMover : MonoBehaviour, IUpdateObj
	{

		float _speed;

		void Awake()
		{
			_speed = Random.Range(1.0f, 1.1f);
		}
		
		void OnEnable()
		{
			// Registers the script into the UpdateManager
			UpdateManager.Add(this);
		}

		public void UpdateManager_Update()
		{
		}

		void moveUpAndDown()
		{
			var currPos = transform.position;
			transform.position = new Vector3(currPos.x, Mathf.PingPong(Time.time * _speed, 10f), currPos.z);
		}

		void OnDisable()
		{
			// Unregisters the script from the UpdateManager
			UpdateManager.Remove(this);
		}

		void IUpdateObj.UpdateManager_Update()
		{
			moveUpAndDown();
		}

		void IUpdateObj.UpdateManager_FixedUpdate()
		{ 

		}

		void IUpdateObj.UpdateManager_LateUpdate()
		{

		}
	}
}
