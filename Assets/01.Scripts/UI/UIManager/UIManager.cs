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
        private TextKeySO textKeySO;
        private EntityPresenter playerHud; 
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

        ///private ScreenUI
        public override void Awake()
        {
            base.Awake();
            // ��巹����� �޾ƿ��� 
            markerUI = AddressablesManager.Instance.GetResource<GameObject>("MarkerUI");
            playerHudUI = AddressablesManager.Instance.GetResource<GameObject>("PlayerHudUI");
            textKeySO ??= AddressablesManager.Instance.GetResource<TextKeySO>("TextKeySO");
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

    }

}
