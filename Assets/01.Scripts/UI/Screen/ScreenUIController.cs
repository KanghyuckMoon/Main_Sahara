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
        // ������Ƽ 
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
                // �κ��丮 Ȱ��ȭ 
                inventoryPresenter.ActiveView();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                // �� Ȱ��ȭ
                mapPresenter.ActiveView();
            }
        }

        /// <summary>
        /// ��� ��ũ�� ��Ȱ��ȭ 
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
