using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    using Module;

namespace Interaction
{
    public class CheckInteractionSoul : MonoBehaviour
    {
        private InteractionTalk interactionTalk;
        private ItemModule itemModule;

        private void Start()
        {
            itemModule = GameObject.Find("Player").GetComponent<AbMainModule>()
                .GetModuleComponent<ItemModule>(ModuleType.Item);
            interactionTalk = GetComponentInChildren<InteractionTalk>();
        }

        private void Update()
        {
            interactionTalk.gameObject.SetActive(itemModule.CheackSoul(AccessoriesItemType.UnlockInteraction));
        }
    }
}