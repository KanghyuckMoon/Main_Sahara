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

        // ������Ƽ 
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
                    if(player is not null) // �� �� �����
                    {
                        AddUIToObj(); // UI �ʱ�ȭ 
                        playerHud = player.GetComponentInChildren<EntityPresenter>();
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
            textKeySO ??= AddressablesManager.Instance.GetResource<TextKeySO>("TextKeySO");
            // ����
            width = Screen.width;
            height = Screen.height; 
        }
    
        /// <summary>
        /// ����۽� �ʱ�ȭ 
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
                Debug.LogWarning("playerHud �����");
                return;
            }
            playerHud.SetActive(_isActive); 
        }

        /// <summary>
        ///  ������Ʈ�� UI �־��ֱ� 
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
