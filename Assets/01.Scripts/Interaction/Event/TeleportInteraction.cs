using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
        public class TeleportInteraction : MonoBehaviour, IInteractionItem
        {
        
        
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
                [SerializeField] private TeleportSystem teleportSystem;
                [SerializeField] private bool isUp;
                
                public void Interaction()
                {
                        teleportSystem.Interaction(isUp);
                }
        }
}
