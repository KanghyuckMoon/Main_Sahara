using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Module;
using Effect;
using DG.Tweening;
using TimeManager;

namespace Interaction
{

	public class DemoEndSystem : MonoBehaviour, IInteractionItem
	{
		[SerializeField]
		private Transform movePosition;


		public bool Enabled
		{
			get
			{
				return true;
			}
			set
			{

			}
		}

		public string Name
		{
			get
			{
				return nameKey;
			}
		}

		public Vector3 PopUpPos
		{
			get
			{
				return transform.position + new Vector3(0, 1, 0);
			}
		}

		public string ActionName
		{
			get
			{
				return "O00000050";
			}
		}

		[SerializeField] private string nameKey = "M00000010";
		[SerializeField] private float power = 10f;
		[SerializeField] private string effectAddress;

		public void Interaction()
		{
			PlayerObj.Player.transform.SetParent(transform);
			var module = PlayerObj.Player.GetComponent<AbMainModule>();
			module.CharacterController.enabled = false;
			StaticTime.EntierTime = 0f;
			Jump();
		}

		private void Jump()
		{
			transform.DOMove(movePosition.position, 3f);
			Invoke("MoveDemoEndScene", 2f);
		}

		private void MoveDemoEndScene()
		{
			SceneManager.LoadScene("LoadingSceneInGame");
		}
	}
}