using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Effect;

namespace Interaction
{
    
    public class JumpInteraction : MonoBehaviour, IInteractionItem
    {
        [SerializeField]
        private Vector3 jumpPoint;
        
        private void Jump()
        {
            PlayerObj.Player.transform.position = jumpPoint;
            EffectManager.Instance.SetEffectDefault(effectAddress, jumpPoint, Quaternion.identity);
        }
        
        
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
			PlayerObj.Player.GetComponent<AbMainModule>().Jump(power);
			Jump();
		}
    }
}

