using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern; 

namespace UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        private GameObject markerUI;
        private GameObject playerHudUI;

        private GameObject player;

        // ������Ƽ 
        public GameObject Player
        {
            get
            {
                if (player is null)
                {
                    player ??= GameObject.FindWithTag("Player");
                    if(player is not null) // �� �� �����
                    {
                        AddUIToObj(); // UI �ʱ�ȭ 
                    }

                }
                return player; 
            }
        }
        public override void Awake()
        {
            base.Awake();
            // ��巹����� �޾ƿ��� 
            markerUI = AddressablesManager.Instance.GetResource<GameObject>("MarkerUI");
            playerHudUI = AddressablesManager.Instance.GetResource<GameObject>("PlayerHudUI");
        }

        /// <summary>
        ///  ������Ʈ�� UI �־��ֱ� 
        /// </summary>
        private void AddUIToObj()
        {
            Instantiate(markerUI, player.transform);
            Instantiate(playerHudUI, player.transform);
        }
    }

}
