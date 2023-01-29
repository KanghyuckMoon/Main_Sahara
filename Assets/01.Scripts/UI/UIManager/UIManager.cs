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

        // 프로퍼티 
        public GameObject Player
        {
            get
            {
                if (player is null)
                {
                    player ??= GameObject.FindWithTag("Player");
                    if(player is not null) // 한 번 실행됨
                    {
                        AddUIToObj(); // UI 초기화 
                    }

                }
                return player; 
            }
        }
        public override void Awake()
        {
            base.Awake();
            // 어드레서블로 받아오기 
            markerUI = AddressablesManager.Instance.GetResource<GameObject>("MarkerUI");
            playerHudUI = AddressablesManager.Instance.GetResource<GameObject>("PlayerHudUI");
        }

        /// <summary>
        ///  오브젝트에 UI 넣어주기 
        /// </summary>
        private void AddUIToObj()
        {
            Instantiate(markerUI, player.transform);
            Instantiate(playerHudUI, player.transform);
        }
    }

}
