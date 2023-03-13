using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using UI.Base; 

namespace UI.Manager
{
    public class UIManager : MonoSingleton<UIManager>
    {
        private GameObject markerUI;
        private GameObject playerHudUI;

        private GameObject player;
        private TextKeySO textKeySO;
        private EntityPresenter playerHud;

        private IUIController screenUIController = null;

        private int width, height;

        public int Width => width;
        public int Height => height; 
        // private ScreenUI

        // 프로퍼티 
        public TextKeySO TextKeySO
        {
            get
            {
                textKeySO ??= AddressablesManager.Instance.GetResource<TextKeySO>("TextKeySO");
                return textKeySO; 
            }
        }
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
                        playerHud = player.GetComponentInChildren<EntityPresenter>();
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
            textKeySO ??= AddressablesManager.Instance.GetResource<TextKeySO>("TextKeySO");
            // 설정
            width = Screen.width;
            height = Screen.height; 
        }
    
        /// <summary>
        /// 재시작시 초기화 
        /// </summary>
        public void Init()
        {
            screenUIController = null; 
            player = null; 
        }
        public void ActiveHud(bool _isActive)
        {
            if (playerHud == null)
            {
                Debug.LogWarning("playerHud 없어요");
                return;
            }
            playerHud.SetActive(_isActive); 
        }

        /// <summary>
        ///  오브젝트에 UI 넣어주기 
        /// </summary>
        private void AddUIToObj()
        {
            Instantiate(markerUI, player.transform);
            Instantiate(playerHudUI, player.transform);
        }

        public void ActiveCursor(bool _isActive)
        {
            if (_isActive == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                return;
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }

}
