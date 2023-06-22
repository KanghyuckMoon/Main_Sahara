using System.Collections;
using System.Collections.Generic;
using Inventory;
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
                                if (string.IsNullOrEmpty(needItem))
                                {
                                        return "O00000052"; 
                                }
                                else if (InventoryManager.Instance.ItemCheck(needItem, 1))
                                {
                                        return "O00000052";
                                }
                                return "O00000051";
                        }
                }

                [SerializeField] private string nameKey = "M00000010";
                [SerializeField] private TeleportSystem teleportSystem;
                [SerializeField] private bool isUp;
                [SerializeField] private string needItem;
                
                public void Interaction()
                {
                        if (string.IsNullOrEmpty(needItem))
                        {
                                teleportSystem.Interaction(isUp);
                                return;
                        }
                        else if(InventoryManager.Instance.ItemCheck(needItem, 1))
                        {
                                teleportSystem.Interaction(isUp);
                        }
                }
        }
}
