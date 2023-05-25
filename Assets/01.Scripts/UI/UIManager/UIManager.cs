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
        private GameObject uiCam; 
        
        private GameObject markerUI;
        private GameObject playerHudUI;

        private GameObject playerObj;
        private TextKeySO textKeySO;
        private EntityPresenter playerHud;

        // 기능들 
        private CursorModule cursorModule; 
        
        //private IUIController screenUIController = null;

        private int width, height;


        public TextKeySO TextKeySO
        {
            get
            {
                textKeySO ??= AddressablesManager.Instance.GetResource<TextKeySO>("TextKeySO");
                return textKeySO; 
            }
        }
        public GameObject PlayerObject
        {
            get
            {
                if (playerObj == null)
                {
                    playerObj =  PlayerObj.Player;
                    if(playerObj != null) // 한 번 실행됨
                    {
                        AddUIToObj(); // UI 초기화 
                        playerHud = playerObj.GetComponentInChildren<EntityPresenter>();
                    }

                }
                return playerObj; 
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

            cursorModule = new CursorModule();
        }
    
        /// <summary>
        /// 재시작시 초기화 
        /// </summary>
        public void Init()
        {
            //screenUIController = null; 
            playerObj = null; 
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
            Instantiate(markerUI, playerObj.transform);
            Instantiate(playerHudUI, playerObj.transform);
        }

        public void ActiveCursor(bool _isActive)
        {
            this.cursorModule.ActiveCursor(_isActive);
        }

        public void SetCursorImage(CursorImageType _type)
        {
            cursorModule.SetCursor(_type);
        }
    
    }

}
