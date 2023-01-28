using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    enum ScreenType
    {
        Inventory,
        Map,

    }

    public class ScreenUIController : MonoBehaviour
    {
        private InventoryPresenter inventoryPresenter;
        private MapPresenter mapPresenter;

        private Dictionary<ScreenType, IScreen> screenDic = new Dictionary<ScreenType, IScreen>();
        // 프로퍼티 
        public InventoryPresenter InventoryPresenter => inventoryPresenter;
        public MapPresenter MapPresenter => mapPresenter;

        private void Awake()
        {
            InitScreenPresenters();

            screenDic.Add(ScreenType.Inventory, inventoryPresenter);
            screenDic.Add(ScreenType.Map, mapPresenter);
        }

        private void Start()
        {
            EnabledAllScreens();
        }

        private void Update()
        {
            UIInput();
        }

        private void InitScreenPresenters()
        {
            inventoryPresenter = GetComponentInChildren<InventoryPresenter>();
            mapPresenter = GetComponentInChildren<MapPresenter>();
        }
        private void UIInput()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                // 인벤토리 활성화 
                inventoryPresenter.ActiveView();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                // 맵 활성화
                mapPresenter.ActiveView();
            }
        }

        /// <summary>
        /// 모든 스크린 비활성화 
        /// </summary>
        private void EnabledAllScreens()
        {
            foreach(var v in screenDic)
            {
                screenDic[v.Key].ActiveView(false); 
            }
        }
    }
}
