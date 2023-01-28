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
        // ������Ƽ 
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
            // ��巹����� �޾ƿ��� 
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
        ///  ������Ʈ�� UI �־��ֱ� 
        /// </summary>
        private void AddUIToObj()
        {
            isInit = true; 
            Instantiate(markerUI, player.transform);
            Instantiate(playerHudUI, player.transform);
        }
    }

}
