using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern; 

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        private GameObject markerUI;
        private GameObject playerHudUI;

        private GameObject player;

        private bool isInit = false; 
        // 프로퍼티 
        public GameObject Player
        {
            get
            {
                player ??= GameObject.FindWithTag("Player");
                return player; 
            }
        }
        private void Awake()
        {   
            // 어드레서블로 받아오기 
            markerUI = AddressablesManager.Instance.GetResource<GameObject>("MarkerUI");
            playerHudUI = AddressablesManager.Instance.GetResource<GameObject>("PlayerHudUI"); 
        }
        void Start()
        {

        }

        void Update()
        {
            if (Player == null) return;

            if(isInit == false)
            { 
                AddUIToObj();
            }
        }

        /// <summary>
        ///  오브젝트에 UI 넣어주기 
        /// </summary>
        private void AddUIToObj()
        {
            isInit = true; 
            Instantiate(markerUI, player.transform);
            Instantiate(playerHudUI, player.transform);
        }
    }

}
